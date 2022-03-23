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
    public class DanController : ControllerBase
    {
        public AgencijaContext Context { get; set; }

        public DanController(AgencijaContext context)
        {
            Context = context;
        }

        [Route("DaniUNedelji")]
        [HttpGet]
        public async Task<ActionResult> Dani()
        {
            try
            {
                return Ok(await Context.Dani.Select(p =>
                new
                {
                    ID = p.ID,
                    Naziv = p.Naziv
                }).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //upis honorara za taj dan kada je radnik odradio posao
        [Route("DodajOdradjeniPosao/{jmbg}/{idPosla}/{idDana}/{honorar}")]      
        [HttpPost]
        public async Task<ActionResult> DodajPosao(int jmbg, int idPosla, int idDana, int honorar)
        {
            
            if (jmbg < 1000 || jmbg > 100000000)
            {
                return BadRequest("Pogresno unet JMBG!");
            }

            if (idPosla <= 0)
            {
                return BadRequest("Pogresan ID!");
            }

            if (idDana <= 0)
            {
                return BadRequest("Pogresan ID!");
            }

            if (honorar < 200 || honorar > 150000)
            {
                return BadRequest("Pogresno unet honorar!");
            }

            try
            {
                var radnik = await Context.Radnici.Where(p => p.JMBG == jmbg).FirstOrDefaultAsync();   //potreban je radnik kome upisujemo honorar
                var posao = await Context.Poslovi.Where(p => p.ID == idPosla).FirstOrDefaultAsync();    //potreban je posao koji je zavrsen
                var dan = await Context.Dani.Where(p => p.ID == idDana).FirstOrDefaultAsync();         //potreban je dan kada je radnik obavio ovaj posao
                //var dan = await Context.Dani.FindAsync(idDana);   

                Spoj s = new Spoj
                {
                    Radnik = radnik,  
                    Posao = posao,  
                    Dan = dan,      
                    Honorar = honorar   //honorar je onaj koji je prosledjen kroz parametar
                };

                //tabelu spoj dodajemo radnika sa honorarom
                Context.RadniciPoslovi.Add(s);
                await Context.SaveChangesAsync();

                var podaciORadniku = await Context.RadniciPoslovi
                        .Include(p => p.Radnik)
                        .Include(p => p.Posao)
                        .Include(p => p.Dan)
                        .Where(p => p.Radnik.JMBG == jmbg)
                        .Select(p =>
                        new
                        {
                            JMBG = p.Radnik.JMBG,
                            Ime = p.Radnik.Ime,
                            Prezime = p.Radnik.Prezime,
                            Posao = p.Posao.Naziv,
                            Dan = p.Dan.Naziv,
                            Honorar = p.Honorar
                        }).ToListAsync();
                return Ok(podaciORadniku); //ovako vracam ove podatke iz novog anonimnog objekta koji sadrzi informacije koje nam trebaju i koji je kreiran sa ovim new
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}