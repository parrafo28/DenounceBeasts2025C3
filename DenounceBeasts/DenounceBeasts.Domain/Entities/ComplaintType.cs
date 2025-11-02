using DenounceBeasts.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.Domain.Entities
{
    public class ComplaintType: BaseEntity
    { 
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
