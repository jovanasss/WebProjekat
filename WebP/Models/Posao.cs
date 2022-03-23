using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Posao")]
    public class Posao
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Naziv { get; set; }

        [Range(1, 4)]  //4 Nedelje ili mesec dana
        public int Nedelja { get; set; } 

        [JsonIgnore]
        public virtual List<Spoj> PosaoRadnik { get; set; }
    }
}