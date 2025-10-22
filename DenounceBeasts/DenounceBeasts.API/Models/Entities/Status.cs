using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.API.Models.Entities
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        //[MaxLength(100)]
        //[MinLength(1)]
        [StringLength(50)]
        public string Name { get; set; }    = string.Empty;
    }
}
