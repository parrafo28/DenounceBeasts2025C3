using DenounceBeasts.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.Domain.Entities
{
    public class ComplaintType : BaseEntity
    {

        [StringLength(75, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }
    }
}
