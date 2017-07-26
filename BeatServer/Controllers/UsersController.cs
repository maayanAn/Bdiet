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
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        public IHttpActionResult Options()
        {
            return Ok();
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult Post([FromBody]User value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // register the new user in the DB
            User registeredUser = EntitiesManager.getInstance().Register(value);

            if (registeredUser == null)
            {
                return BadRequest("Email already in use");
            }

            return CreatedAtRoute("DefaultApi", new { id = registeredUser.UserId }, registeredUser);
        }
        
    }
}
