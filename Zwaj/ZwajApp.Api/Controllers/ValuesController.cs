using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZwajApp.Api.Data;
namespace ZwajApp.Api.Controllers
{  
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;

        }
        // GET api/values
       //[AllowAnonymous]
        [HttpGet]
        public async Task< IActionResult> GetValues()
        {
            var Values=await _context.Values.ToListAsync();
            return Ok("ali");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task< IActionResult> GetValue(int id)
        {   
            var Value =await _context.Values.FirstOrDefaultAsync(x=>x.id==id);
            return Ok(Value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
