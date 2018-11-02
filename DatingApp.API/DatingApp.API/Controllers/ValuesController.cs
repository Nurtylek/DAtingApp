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
using DatingApp.API.Models;

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
        [HttpGet("{id}", Name = "GetValue")]
        public async Task<IActionResult> GetValueAsync(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);

            if (value == null)
                return NotFound();

            var valueToReturn = _mapper.Map<ValueForReturnDto>(value);
            return Ok(valueToReturn);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Value value)
        {
            if (value == null) {
                return BadRequest();
            }
            _context.Values.Add(value);
            _context.SaveChanges();
            // return CreatedAtRoute("GetValue", new { id = value.Id}, value);
            return StatusCode(201);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Value value)
        {
            if (value == null || value.Id != id)
                return BadRequest();      

            var values = _context.Values.FirstOrDefault(x => x.Id == id);
            
            if (values == null)
                return NotFound();

            values.Name = value.Name;
            values.Surname = value.Surname;
            values.Age = value.Age;
            values.Id = value.Id;
            values.Hobby = value.Hobby;

            _context.Values.Update(values);
            _context.SaveChanges();
            return NoContent();              
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value =  _context.Values.FirstOrDefault(x => x.Id == id);
            if (value == null)
                return NotFound();

            _context.Values.Remove(value);
            _context.SaveChanges();
            return Ok(value);
        }
    }
}
