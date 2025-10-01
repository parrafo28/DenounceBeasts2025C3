using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.API.Models.Entities
{
    public class Municipality
    {
       [Key]
        public int Id { get; set; }

        public string PostalCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        List<Sector> Sectors { get; set; } = new List<Sector>();
    }
}
