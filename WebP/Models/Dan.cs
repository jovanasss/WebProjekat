using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Dan")]
    public class Dan
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Naziv { get; set; }  
        
        [JsonIgnore]
        public virtual List<Spoj> RadniciPoslovi { get; set; }
    }
}