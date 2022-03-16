using RedisCache.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RedisCache.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using RedisCache.Entities;
using Newtonsoft.Json;

namespace RedisCache.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/v1/[controller]")]
    public class PessoasController : Controller
    {
        private readonly IPessoaService _service;
        private readonly IDistributedCache _cache;

        public PessoasController(IPessoaService service, IDistributedCache cache)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _cache = cache;
        }

        [HttpPost]
        public async Task<ActionResult> Post(PessoaViewModel pessoaViewModel)
        {
            await _service.Add(pessoaViewModel);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PessoaViewModel pessoaViewModel)
        {
            try
            {
                List<Pessoa> lPessoas = new();

                var cachedCategory = _cache.GetString("pessoaList");
                if (!string.IsNullOrEmpty(cachedCategory))
                {
                    lPessoas = JsonConvert.DeserializeObject<List<Pessoa>>(cachedCategory);
                }
                else
                {
                    lPessoas = await _service.GetByName(pessoaViewModel);

                    DistributedCacheEntryOptions options = new();
                    options.SetAbsoluteExpiration(new TimeSpan(0, 0, 30));

                    _cache.SetString("pessoaList", JsonConvert.SerializeObject(lPessoas), options);
                }
                return Ok(lPessoas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
