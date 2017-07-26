using BeatServer.Genetic_algorithm;
using BeatServer.Managers;
using BeatServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BeatServer.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        //// for testing DB conenction
        //// GET: api/Login
        //public IEnumerable<string> Get()
        //{
            
        //    IEnumerable<User> lat = EntitiesManager.getInstance().GetUsers();        

        //    GeneticAlgorithmGenerator GA = new GeneticAlgorithmGenerator(lat.First());
        //    Menu m = GA.RunAlgorithm();

        //    return new string[] { "value1", "value2" };
        //}

        // Enable all the options in config file
        public IHttpActionResult Options()
        {
            return Ok();
        }

        // POST: api/Login
        [ResponseType(typeof(User))]
        public IHttpActionResult Post([FromBody]LoginUser value)
        {
            // Validate the request format
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Valid the user - if exist in the db
            User loggedUser = EntitiesManager.getInstance().Login(value);

            if (loggedUser == null)
            {
                return BadRequest("Invalid email or password");
            }

            return CreatedAtRoute("DefaultApi", new { id = loggedUser.UserId }, loggedUser);
        }
    }
}
