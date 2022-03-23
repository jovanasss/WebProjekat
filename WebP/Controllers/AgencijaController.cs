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
    public class AgencijaController : ControllerBase
    {
        public AgencijaContext Context { get; set; }

        public AgencijaController(AgencijaContext context)
        {
            Context = context;
        }

        [Route("PreuzmiAgencije")]
        [HttpGet]
        public async Task<ActionResult> Agencije()
        {
            try
            {
                return Ok(await Context.Agencije.Select(p =>
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

       
    }
}