using CustomsExternal.Data;
using CustomsExternal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomsExternal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeclarationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DeclarationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<DeclarationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DeclarationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DeclarationController>
        [HttpPost]
        public ActionResult Post(Declaration declaration)
        {
            _context.Declarations.Add(declaration);
            _context.SaveChanges();
            return Ok();
        }

        // PUT api/<DeclarationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DeclarationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
