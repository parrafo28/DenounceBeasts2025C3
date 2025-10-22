using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.API.Models.Entities
{
    public class Municipality
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string PostalCode { get; set; } = string.Empty;

        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        public List<Sector> Sectors { get; set; }
    }
}
