using DenounceBeasts.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.Domain.Entities
{
    public class Municipality : BaseEntity
    { 

        [StringLength(50, MinimumLength = 2)]
        public string PostalCode { get; set; } = string.Empty;

        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        public List<Sector> Sectors { get; set; }
    }
}
