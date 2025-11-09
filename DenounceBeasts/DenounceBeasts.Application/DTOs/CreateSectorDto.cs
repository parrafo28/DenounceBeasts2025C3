namespace DenounceBeasts.Application.DTOs
{
    public class CreateSectorDto
    { 
        public bool IsActive { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public string Name { get; set; } = null!;
        public int MunicipalityId { get; set; }
    }
}
