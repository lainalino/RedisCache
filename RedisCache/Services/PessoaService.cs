using RedisCache.Entities;
using RedisCache.ViewModels;
using RedisCache.Services.Interfaces;
using RedisCache.Repositories.Interfaces;

namespace RedisCache.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }
        public async Task<Pessoa> Add(PessoaViewModel pessoaViewModel)
        {
            var pessoa = new Pessoa(
               pessoaViewModel.Name
           );

            return await _pessoaRepository.Add(pessoa);
        }

        public async Task<List<Pessoa>> GetByName(PessoaViewModel pessoaViewModel)
        {
            return await _pessoaRepository.GetByName(pessoaViewModel.Name);
        }

        public async Task<Pessoa> GetById(int id)
        {
            return await _pessoaRepository.GetById(id);
        }

    }
}
