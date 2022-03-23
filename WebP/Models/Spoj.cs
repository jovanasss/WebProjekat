using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Spoj")]
    public class Spoj
    {
        [Key]
        public int ID { get; set; }

        [Range(200, 150000)]   //RSD
        public int Honorar { get; set; }

        //veze
        public virtual Dan Dan { get; set; } 

        [JsonIgnore] 
        public virtual Radnik Radnik { get; set; }

        public virtual Posao Posao { get; set; }
    }
}