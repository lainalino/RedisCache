using RedisCache.Entities;

namespace RedisCache.Repositories.Interfaces
{
    public interface IPessoaRepository : IRepositoryBase<Pessoa>
    {
        Task<List<Pessoa>> GetByName(string name);
    }
}
