using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Radnik")]
    public class Radnik
    {
        [Key]
        public int ID { get; set; }

        [Required]                  // Required onemogucava da se u bazu upise null
        [Range(1000, 100000000)]   // ovo za validaciju
        public int JMBG { get; set; }

        [Required]
        [MaxLength(50)]
        public string Ime { get; set; }

        [Required]
        [MaxLength(50)]
        public string Prezime { get; set; }

        //veze
        public virtual List<Spoj> RadnikPosao { get; set; }

        //radnik moze da radi samo u jednoj agenciji
        public int AgencijaID { get; set; }
        public Agencija Agencija { get; set; }


    }
}