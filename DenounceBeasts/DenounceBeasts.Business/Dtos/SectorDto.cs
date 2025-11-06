namespace DenounceBeasts.Business.Dtos
{
    public class SectorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty; 
        public int MunicipalityId { get; set; }
        public string? MunicipalityName { get;  set; } 
    }
}
