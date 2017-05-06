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
        // for testing DB conenction
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            EntitiesManager.getInstance().GetUsers();
            return new string[] { "value1", "value2" };
        }

        //[Route("Register")]
        //public IHttpActionResult RegisterOptions()
        //{
        //    return Ok();
        //}

        public IHttpActionResult Options()
        {
            return Ok();
        }
        // not needed for users now
        // GET: api/Login/5
        //[ResponseType(typeof(User))]
        //public IHttpActionResult Get(int id)
        //{
        //    return Ok(new User());
        //}

        // POST: api/Login
        [ResponseType(typeof(User))]
        public IHttpActionResult Post([FromBody]LoginUser value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User u = EntitiesManager.getInstance().Login(value);

            if (u == null)
            {
                return BadRequest("Invalid email or password");
            }

            return CreatedAtRoute("DefaultApi", new { id = u.user_id }, u);
        }

        //// POST: api/Login/Register
        //[ResponseType(typeof(User))]
        //[Route("Register")]
        //public IHttpActionResult Register([FromBody]User value)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    User u = EntitiesManager.getInstance().Register(value);

        //    if (u == null)
        //    {
        //        return BadRequest("Email already in use");
        //    }

        //    return CreatedAtRoute("Default", new { id = u.user_id }, u);
        //}

        // not needed for users now
        //// PUT: api/Login/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Login/5
        //public void Delete(int id)
        //{
        //}
    }
}
