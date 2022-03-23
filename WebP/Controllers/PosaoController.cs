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
    public class PosaoController : ControllerBase
    {
        public AgencijaContext Context { get; set; }

        public PosaoController(AgencijaContext context)
        {
            Context = context;
        }

        [Route("PreuzmiPoslove")]         // vraca ID i naziv poslova
        [HttpGet]
        public async Task<ActionResult> Preuzmi()
        {
            try
            {
                return Ok(await Context.Poslovi.Select(p => new {p.ID, p.Naziv}).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}