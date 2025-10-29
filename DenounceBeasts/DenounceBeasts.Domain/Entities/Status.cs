using DenounceBeasts.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.Domain.Entities
{
    public class Status : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
