using System.ComponentModel.DataAnnotations;

namespace RedisCache.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
