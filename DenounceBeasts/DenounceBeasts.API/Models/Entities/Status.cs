using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.API.Models.Entities
{
    public class Status
    {
        public int Id { get; set; }

        [StringLength(100)]
        //[MaxLength(100)]
        //[MinLength(3)]
        //[EmailAddress] 
        public string Name { get; set; } = string.Empty;
    }
}
