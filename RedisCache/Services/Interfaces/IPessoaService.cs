using RedisCache.Entities;
using RedisCache.ViewModels;
namespace RedisCache.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<Pessoa> Add(PessoaViewModel brandViewModel);
        Task<List<Pessoa>> GetByName(PessoaViewModel pessoaViewModel);
    }
}
