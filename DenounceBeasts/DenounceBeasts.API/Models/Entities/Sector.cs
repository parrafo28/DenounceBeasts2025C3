using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.API.Models.Entities
{
    public class Sector
    {
        [Key]
        public int Id { get; set; } 

        [StringLength(50, MinimumLength = 2)]
        public string PostalCode { get; set; } = string.Empty;

        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        public int MunicipalityId { get; set; } 

        public virtual Municipality? Municipality { get; set; }

    }
}
