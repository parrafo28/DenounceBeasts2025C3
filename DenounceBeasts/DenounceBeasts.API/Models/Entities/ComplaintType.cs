using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.API.Models.Entities
{
    public class ComplaintType
    {
        [Key]
        public int Id { get; set; }

        [StringLength(75, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(300)] 
        public string Description { get; set; }
    }
}
