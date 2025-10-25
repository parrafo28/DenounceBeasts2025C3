using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.API.Models.DTOs
{
    public class MunicipalityDto
    {
        public int Id { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public string Name { get; set; } = null!;
    }
}
