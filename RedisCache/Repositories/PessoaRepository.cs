using Microsoft.EntityFrameworkCore;
using RedisCache.Entities;
using RedisCache.Migrations;
using RedisCache.Repositories.Interfaces;

namespace RedisCache.Repositories
{
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(ApplicationDbContext context) : base(context) { 
        }

        public async Task<List<Pessoa>> GetByName(string name)
        {
            return await Db.Pessoa.Where(p => p.Name.Contains(name)).ToListAsync();
        }

    }
}
