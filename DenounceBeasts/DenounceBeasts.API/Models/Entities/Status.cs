using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.API.Models.Entities
{
    public class Status
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 1)]
       // [StringLength(100)]
        //[MinLength(1)]
        //[MaxLength(100)] 
        public string Name { get; set; }
    }
}
