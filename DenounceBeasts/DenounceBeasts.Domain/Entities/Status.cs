using DenounceBeasts.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.Domain.Entities
{
    public class Status: BaseEntity
    { 

        [StringLength(100, MinimumLength = 1)]

        public string Name { get; set; }
    }
}
