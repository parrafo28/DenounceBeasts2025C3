using System.ComponentModel.DataAnnotations.Schema;

namespace DenounceBeasts.API.Models.Entities
{
   // [Table("MUNICIPALITY")]
    public class Municipality
    {
        public int Id { get; set; }
        public string PostalCode { get; set; } = string.Empty;
      //  [Column("MunicipalityName")]
        public string Name { get; set; } = string.Empty;
      //  public bool IsActive { get; set; }
       public List<Sector> Sectors { get; set; }= new List<Sector>();

        //public Municipality()
        //{
        //    Sectors = new List<Sector>();
        //}
    }
}
