using DenounceBeasts.Domain.Core;

namespace DenounceBeasts.Domain.Entities
{
    public class Municipality : BaseEntity
    {
        public string PostalCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<Sector> Sectors { get; set; } = new List<Sector>();
    }
}
