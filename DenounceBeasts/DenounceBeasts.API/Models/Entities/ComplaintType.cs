using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.API.Models.Entities
{
    public class ComplaintType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
