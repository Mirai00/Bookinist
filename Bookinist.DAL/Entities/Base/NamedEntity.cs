using System.ComponentModel.DataAnnotations;

namespace Bookinist.DAL.Entities
{
    public abstract class NamedEntity : EntityBase
    {
        [Required]
        public string Name { get; set; }
    }
}