using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.API.Models.Entities
{
    public class Sector
    {
        [Key]
        public int Id { get; set; } 
        public string PostalCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int MunicipalityId { get; set; } 
        public virtual Municipality? Municipality { get; set; }

    }
}
