namespace DenounceBeasts.API.Models.DTOs
{
    public class SectorDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public string Name { get; set; } = null!;
        public int MunicipalityId { get; set; }
        public string? MunicipalityName { get;  set; }
    }
}
