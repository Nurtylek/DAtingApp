using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly DataContext _context;
         private readonly IMapper _mapper;
        public ValuesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.Values.ToArrayAsync();

            var valuesToReturn = _mapper.Map<IEnumerable<ValueForReturnDto>>(values);
            return Ok(valuesToReturn);

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValueAsync(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            var valueToReturn = _mapper.Map<ValueForReturnDto>(value);
            return Ok(valueToReturn);
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
