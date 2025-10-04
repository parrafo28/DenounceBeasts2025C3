using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.API.Models.Entities
{
    // [Table("MUNICIPA_LITIES")]
    public class Municipality
    {
        [Key]
        //  public int MunicipalityId { get; set; }
        public int Id { get; set; }
        //public bool IsActive { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        //[Column("MUNI_CIPALITY_NAME")]
        public string Name { get; set; } = null!;
        public virtual List<Sector> Sectors { get; set; }
    }
}
