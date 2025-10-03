namespace DenounceBeasts.API.Models.Entities
{
    public class Sector
    {

        public int Id { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
