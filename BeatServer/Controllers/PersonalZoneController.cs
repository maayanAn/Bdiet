using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using BeatServer.Models;

namespace BeatServer.Controllers
{
    [RoutePrefix("api/PersonalZone")]
    public class PersonalZoneController : ApiController
    {
        enum Allergies
        {
            None,
            Gluten_allergy,
            Dairy_allergy,
            Nuts_allergy,
            Corn_allergy
        };

        enum Preferences
        {
            None,
            Vegan,
            Vegetarian,
            Rich_with_iron,
            Low_sugar,
            Low_Cholesterol,
            No_eggs,
            No_fish
        };

        public IHttpActionResult Options()
        {
            return Ok();
        }

        // GET: api/PersonalZone
        public IEnumerable<string> Get()
        {
            List<Allergies> generalAllergies = Enum.GetValues(typeof(Allergies)).Cast<Allergies>().ToList();
            List<Preferences> generalPreferences = Enum.GetValues(typeof(Preferences)).Cast<Preferences>().ToList();
            string allergies = string.Join(",", generalAllergies.ToArray());
            string preferences = string.Join(",", generalPreferences.ToArray());
            //return new string[] { string.Join(",", generalAllergies.ToArray()), string.Join(",", generalPreferences.ToArray()) };
            return new string[] { allergies, preferences };
        }


        //// not needed for users now
        ////// GET: api/Login/5
        //[ResponseType(typeof(User))]
        //public IHttpActionResult Get()
        //{
        //    return Ok(new User());
        //}

        // POST: api/PersonalZone
        [ResponseType(typeof(PersonalZone))]
        public IHttpActionResult Post([FromBody]PersonalZone value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonalZone pz = value;

            if (pz == null)
            {
                return BadRequest("Cannot go to menu, please try again");
            }

            return CreatedAtRoute("DefaultApi", new { id = pz.userId }, pz);
        }
    }
}