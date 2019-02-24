using System.Collections.Generic;
using System.Linq;
using Heroes.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        // GET api/heroes
        [HttpGet]
        public ActionResult<IEnumerable<Hero>> Get()
        {
            return Heroes;
        }

        // GET api/heroes/5
        [HttpGet("{id}")]
        public ActionResult<Hero> Get(int id)
        {
            return Heroes.SingleOrDefault(x => x.Id == id);
        }

        private static readonly Hero[] Heroes = new Hero[]
        {
            new Hero { Id = 11, Name = "Mr. Nice" },
            new Hero { Id = 12, Name = "Narco" },
            new Hero { Id = 13, Name = "Bombasto" },
            new Hero { Id = 14, Name = "Celeritas" },
            new Hero { Id = 15, Name = "Magneta" },
            new Hero { Id = 16, Name = "RubberMan" },
            new Hero { Id = 17, Name = "Dynama" },
            new Hero { Id = 18, Name = "Dr IQ" },
            new Hero { Id = 19, Name = "Magma" },
            new Hero { Id = 20, Name = "Tornado" }
        };
    }
}
