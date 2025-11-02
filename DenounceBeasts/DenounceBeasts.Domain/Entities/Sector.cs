using DenounceBeasts.Domain.Core;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DenounceBeasts.Domain.Entities
{
    public class Sector: BaseEntity
    { 
        public string PostalCode { get; set; } = string.Empty;
        public string Name { get; set; } = null!;
        public int MunicipalityId { get; set; }
        
        public virtual Municipality Municipality { get; set; }  
    }
}
