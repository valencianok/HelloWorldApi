using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldApi.Models;
using HelloWorldApi.Movies;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorldApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // GET: api/<MoviesController>
        [HttpGet]
        [Description("Gets the list of all movies")]
        [ProducesResponseType(typeof(List<Movie>), 200)]
        public async Task<IActionResult> GetAsync()
        {
            using (var ct = new MoviesDataController())
            {
                return Ok(await ct.GetMoviesAsync());
            }
        }

        // GET api/<MoviesController>/{guid}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Movie), 200)]
        public async Task<IActionResult> Get(string id)
        {
            using (var ct = new MoviesDataController())
            {
                Guid guid;
                if (!Guid.TryParse(id, out guid)) // validates id is a guid
                    return StatusCode((int)System.Net.HttpStatusCode.ExpectationFailed);

                return Ok(await ct.GetMovieAsync(guid));
            }
        }

        // POST api/<MoviesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie value)
        {
            using (var ct = new MoviesDataController())
            {
                Guid? id = await ct.CreateMovieAsync(value);
                if (id.HasValue)
                    return Ok(new { id = id.ToString() });
                else
                    return Conflict();
            }
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Movie value)
        {
            using (var ct = new MoviesDataController())
            {
                Guid guid;
                if (!Guid.TryParse(id, out guid)) // validates id is a guid
                    return StatusCode((int)System.Net.HttpStatusCode.ExpectationFailed);

                bool ok = await ct.UpdateMovieAsync(guid, value);
                if (ok)
                    return Ok();
                else
                    return Conflict();
            }
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using (var ct = new MoviesDataController())
            {
                Guid guid;
                if (!Guid.TryParse(id, out guid)) // validates id is a guid
                    return StatusCode((int)System.Net.HttpStatusCode.ExpectationFailed);

                bool ok = await ct.DeleteMovieAsync(Guid.Parse(id));
                if (ok)
                    return Ok();
                else
                    return Conflict();
            }
        }
    }
}
