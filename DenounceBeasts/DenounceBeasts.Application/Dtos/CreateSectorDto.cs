namespace DenounceBeasts.Application.Dtos
{
    public class CreateSectorDto
    {
        public string PostalCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int MunicipalityId { get; set; }
    }
}
