using DenounceBeasts.Domain.Core;

namespace DenounceBeasts.Domain.Entities
{
    public class Sector : BaseEntity
    {
        //[Key]
        //public int Id { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }
    }
}
