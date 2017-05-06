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

        // GET: api/Users
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Users/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult Post([FromBody]User value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User u = EntitiesManager.getInstance().Register(value);

            if (u == null)
            {
                return BadRequest("Email already in use");
            }

            return CreatedAtRoute("Default", new { id = u.user_id }, u);
        }

        //// PUT: api/Users/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Users/5
        //public void Delete(int id)
        //{
        //}
    }
}
