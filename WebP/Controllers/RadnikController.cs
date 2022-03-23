using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace WebP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RadnikController : ControllerBase
    {
        public AgencijaContext Context { get; set; }

        public RadnikController(AgencijaContext context)
        {
            Context = context;
        }

        //Get je za prikaz
        [Route("Radnici")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi([FromQuery] int[] danIDs)
        {
            var radnici = Context.Radnici
                .Include(p => p.RadnikPosao)
                .ThenInclude(p => p.Dan)
                .Include(p => p.RadnikPosao)
                .ThenInclude(p => p.Posao);

            var radnik = await radnici.ToListAsync();



            return Ok  // stvari koje nam trebaju
            (
                radnik.Select(p =>
                new
                {
                    JMBG = p.JMBG,
                    Ime = p.Ime,
                    Prezime = p.Prezime,
                    Poslovi = p.RadnikPosao
                        .Where(q => danIDs.Contains(q.Dan.ID))
                        .Select(q =>
                        new
                        {
                            Posao = q.Posao.Naziv,
                            NedeljaPosla = q.Posao.Nedelja,
                            Dan = q.Dan.Naziv,
                            Honorar = q.Honorar
                        })
                }).ToList()
            );
        }

        [Route("RadniciPretraga/{dani}/{posaoID}")]
        [HttpGet]
        public async Task<ActionResult> RadniciPretraga(string dani, int posaoID)
        {

            //sada rasturamo string 
            var danIds = dani.Split('a')   // povezala sam ih sa promenljivom a 
            .Where(x=> int.TryParse(x, out _))
            .Select(int.Parse)
            .ToList(); //imamo niz integera u danIds

            var radnicipoposlu = Context.RadniciPoslovi
                .Include(p => p.Radnik)
                .Include(p => p.Dan)
                .Include(p => p.Posao)
                .Where(p=>p.Posao.ID==posaoID
                && danIds.Contains(p.Dan.ID));
            var radnik = await radnicipoposlu.ToListAsync();



            return Ok  //vraca samo stvari koje nam trebaju
            (
                radnik.Select(p =>
                new
                {
                    JMBG = p.Radnik.JMBG,
                    Ime = p.Radnik.Ime,
                    Prezime = p.Radnik.Prezime,
                    Posao = p.Posao.Naziv,
                    Dan = p.Dan.Naziv,
                    Honorar = p.Honorar
                }).ToList()
            );
        }


    [Route("RadniciPretragaAgencija/{dani}/{posaoID}/{agencijaID}")]
        [HttpGet]
        public async Task<ActionResult> RadniciPretragaAgencija(string dani, int posaoID, int agencijaID)
        {

            //sada rasturamo string 
            var danIds = dani.Split('a')   // povezao sam ih sa promenljivom a pa zato
            .Where(x=> int.TryParse(x, out _))
            .Select(int.Parse)
            .ToList(); // sada imamo niz integera u danIds

            var radnicipoposlu = Context.RadniciPoslovi
                .Include(p => p.Radnik)
                .Include(p => p.Dan)
                .Include(p => p.Posao)
                .Where(p=>p.Posao.ID==posaoID 
                && p.Radnik.AgencijaID == agencijaID  //radnik koji pripada odredjenoj agenciji
                && danIds.Contains(p.Dan.ID));
            var radnik = await radnicipoposlu.ToListAsync();



            return Ok  //vraca samo stvari koje nam trebaju
            (
                radnik.Select(p =>
                new
                {
                    JMBG = p.Radnik.JMBG,
                    Ime = p.Radnik.Ime,
                    Prezime = p.Radnik.Prezime,
                    Posao = p.Posao.Naziv,
                    Dan = p.Dan.Naziv,
                    Honorar = p.Honorar
                }).ToList()
            );
        }



        //Post je za dodavanje
        [Route("DodatiRadnika")]
        [HttpPost]
        public async Task<ActionResult> DodajRadnika([FromBody] Radnik radnik) //frombody salje kompletan atribut
        {
            if (radnik.JMBG < 1000 || radnik.JMBG > 100000000)
            {
                return BadRequest("Pogresno unet JMBG!");
            }

            if( string.IsNullOrWhiteSpace(radnik.Ime) ||  radnik.Ime.Length > 50)  //provera imena
            {
                return BadRequest("Pogresno uneto ime!");
            }

            if( string.IsNullOrWhiteSpace(radnik.Prezime) ||  radnik.Prezime.Length > 50)     //provera prezimena
            {
                return BadRequest("Pogresno uneto prezime!");
            }

            try
            {
                Context.Radnici.Add(radnik);
                await Context.SaveChangesAsync();   // radice u pozadinskoj niti ,a po zavsetku se vraca na glavnu nit i nastavlja
                return Ok($"Radnik ciji je ID: {radnik.ID} je uspesno dodat!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodavanjeRadnika/{jmbg}/{ime}/{prezime}/{agencijaID}")]
        [HttpPost]
        public async Task<ActionResult> DodavanjeRadnika(int jmbg, string ime, string prezime, int agencijaID) //frombody salje kompletan atribut
        {
            var m = Context.Radnici.Where(m => m.JMBG == jmbg).FirstOrDefault();
            if(m==null)
            {
                if (jmbg < 1000 || jmbg > 100000000)
                {
                    return BadRequest("Pogresno unet JMBG!");
                }

                if( string.IsNullOrWhiteSpace(ime) ||  ime.Length > 50 || ime.Any(Char.IsDigit))  //provera imena
                {
                    return BadRequest("Pogresno uneto ime!");
                }

                if( string.IsNullOrWhiteSpace(prezime) ||  prezime.Length > 50 || prezime.Any(Char.IsDigit))     //provera prezimena
                {
                    return BadRequest("Pogresno uneto prezime!");
                }

                try
                {
                    Radnik radnik = new Radnik
                    {
                        JMBG = jmbg,
                        Ime = ime,
                        Prezime = prezime,
                        AgencijaID = agencijaID

                    };

                    Context.Radnici.Add(radnik);
                    await Context.SaveChangesAsync();
                    return Ok("Uspesno dodat novi radnik!");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return BadRequest("Radnik je vec dodat!");
            }
        }

        //azuriranje
        [Route("PromenitiRadnika/{jmbg}/{ime}/{prezime}/{agencijaID}")]
        [HttpPut]
        public async Task<ActionResult> Promeni(int jmbg, string ime, string prezime, int agencijaID)
        {
            if (jmbg < 1000 || jmbg > 100000000)
            {
                return BadRequest("Pogresno unet JMBG!");
            }

            if( string.IsNullOrWhiteSpace(ime) ||  ime.Length > 50)  //provera imena
            {
                return BadRequest("Pogresno uneto ime!");
            }

            if( string.IsNullOrWhiteSpace(prezime) ||  prezime.Length > 50)     //provera prezimena
            {
                return BadRequest("Pogresno uneto prezime!");
            }

            try
            {

                var radnik = Context.Radnici.Where(p => p.JMBG == jmbg && p.AgencijaID == agencijaID).FirstOrDefault(); 

                if (radnik != null) 
                {
                    radnik.Ime = ime;
                    radnik.Prezime = prezime;
                        
                    await Context.SaveChangesAsync();
                    return Ok($"Radnik ciji je ID: {radnik.ID} je uspesno azuriran");
                }
                else
                {
                    return BadRequest("Radnik nije pronadjen!");
                }
      
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    
        [Route("PromenaFromBody")] 
        [HttpPut]
        public async Task<ActionResult> PromeniBody([FromBody] Radnik radnik)
        {
            if (radnik.ID <= 0)
            {
                return BadRequest("Pogresno unet ID!");
            }

            if (radnik.JMBG < 1000 || radnik.JMBG > 100000000)
            {
                return BadRequest("Pogresno unet JMBG!");
            }

            if( string.IsNullOrWhiteSpace(radnik.Ime) ||  radnik.Ime.Length > 50)  //provera imena
            {
                return BadRequest("Pogresno uneto ime!");
            }

            if( string.IsNullOrWhiteSpace(radnik.Prezime) ||  radnik.Prezime.Length > 50)     //provera prezimena
            {
                return BadRequest("Pogresno uneto prezime!");
            }

            try
            {
            
                Context.Radnici.Update(radnik);  // ovo je drugi nacin da se azurira radnik proslednjivanjem njegovog ID

                await Context.SaveChangesAsync();
                return Ok($"Radnik ciji je ID: {radnik.ID} je uspesno azuriran");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisatiRadnika/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogresno unet ID!");
            }

            try
            {
                var radnik = await Context.Radnici.FindAsync(id);
                int jmbg = radnik.JMBG; 
                Context.Radnici.Remove(radnik);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno obrisan rad ik ciji je JMBG: {jmbg}");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }


        [Route("ObrisiRadnika/{jmbg}/{agencijaID}")]
        [HttpDelete]
        public async Task<ActionResult> Obrisi(int jmbg, int agencijaID)
        {

            if (jmbg < 1000 || jmbg > 100000000)
            {
                return BadRequest("Pogresno unet JMBG!");
            }

            var r = Context.Radnici.Where(r => r.JMBG == jmbg && r.AgencijaID==agencijaID).FirstOrDefault();

            if(r == null)
            {
                return BadRequest("Rezervacija ne postoji!");
            }

            else
            {
            try
            {
                var radnik = await Context.Radnici.Where(r => r.JMBG == jmbg && r.AgencijaID==agencijaID).FirstOrDefaultAsync();
                foreach(var rs in Context.RadniciPoslovi.Where(p => p.Radnik.JMBG == jmbg && p.Radnik.AgencijaID==agencijaID))
                {
                    Context.RadniciPoslovi.Remove(rs);
                }

                Context.Radnici.Remove(radnik);
                await Context.SaveChangesAsync();
                return Ok("Radnik je uspesno izbrisan");
            }
            catch (Exception e)
            {
                return BadRequest("Doslo je do greske:" + e.Message);
            }
            }
        }
            /*if (jmbg < 1000 || jmbg > 100000000)
            {
                return BadRequest("Pogresno unet JMBG!");
            }

            var r = Context.Radnici.Where(r => r.JMBG == jmbg && r.AgencijaID==agencijaID).FirstOrDefault();

            if(r == null)
            {
                return BadRequest("Ne postoji!");
            }
            else
            {
            try         
            {

                var radnik = Context.Radnici.Include(a=> a.Agencija)
                                            .Include(b=>b.RadnikPosao)
                    .Where(p => p.JMBG == jmbg && p.AgencijaID == agencijaID ).FirstOrDefault();


                if(radnik !=null)
                {
                    int pamtijmbg = radnik.JMBG; 
                    Context.Remove(radnik);
                    await Context.SaveChangesAsync();
                    return Ok($"Uspesno obrisan radnik ciji je JMBG: {pamtijmbg}");
                }
                else
                {
                    return BadRequest("radnik sa zadatim jmbg nije pronadjen!");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);     
            }
            }
        }*/
    }
}