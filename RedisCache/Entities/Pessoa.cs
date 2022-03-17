using System.ComponentModel.DataAnnotations;

namespace RedisCache.Entities
{
    public class Pessoa : Entity
    {
        public string Name { get; set; }

        public Pessoa()
        {

        }
        public Pessoa(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome da pessoa é necessário");
            Name = name;
        }
    }
}
