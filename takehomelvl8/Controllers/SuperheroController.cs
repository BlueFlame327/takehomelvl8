using Microsoft.AspNetCore.Mvc;

namespace takehomelvl8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuperheroController(ILogger<SuperheroController> logger) : ControllerBase
    {
        private static readonly Dictionary<string, string> SuperheroPowers = new()
        {
            { "Spider-Man", "Wall-crawling, spider-sense, super agility" },
            { "Iron Man", "Powered armor suit, genius intellect" },
            { "Captain America", "Super strength, enhanced agility, indestructible shield" },
            { "Thor", "God of Thunder, super strength, control over lightning" },
            { "Hulk", "Super strength, invulnerability, regeneration" },
            { "Black Widow", "Expert martial artist, espionage skills" },
            { "Doctor Strange", "Master of the mystic arts, reality manipulation" },
            { "Black Panther", "Enhanced strength, agility, and senses, vibranium suit" },
            { "Captain Marvel", "Super strength, flight, energy projection" },
            { "Scarlet Witch", "Reality manipulation, telekinesis, energy projection" }
        };

        private readonly ILogger<SuperheroController> _logger = logger;

        [HttpGet(Name = "GetPower")]
        public string Get(string superhero)
        {
            if (SuperheroPowers.TryGetValue(superhero, out string? power))
            {
                return power;
            }
            return "Super hero not found";
        }

        [HttpGet("all", Name = "GetAll")]
        public Dictionary<string, string> GetAll()
        {
            return SuperheroPowers;
        }

        [HttpPost(Name = "AddSuperhero")]
        public IActionResult Add(string superhero, string power)
        {
            if (SuperheroPowers.ContainsKey(superhero))
            {
                return Conflict("Superhero already exists");
            }
            SuperheroPowers[superhero] = power;
            return CreatedAtAction(nameof(Get), new { superhero }, power);
        }

        [HttpDelete(Name = "DeleteSuperhero")]
        public IActionResult Delete(string superhero)
        {
            if (!SuperheroPowers.ContainsKey(superhero))
            {
                return NotFound("Superhero not found");
            }
            SuperheroPowers.Remove(superhero);
            return NoContent();
        }
    }
}
