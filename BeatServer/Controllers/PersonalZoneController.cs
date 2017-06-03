using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using BeatServer.Models;
using BeatServer.Managers;
using System.Collections;

namespace BeatServer.Controllers
{
    [RoutePrefix("api/PersonalZone")]
    public class PersonalZoneController : ApiController
    {
        public IHttpActionResult Options()
        {
            return Ok();
        }

        // GET: api/PersonalZone
        public PersonalZoneLists Get()
        {
            IList<Allergy> listAllergies = EntitiesManager.getInstance().GetAllergies();
            IList<Preference> listPreferences = EntitiesManager.getInstance().GetPreferences();

            Globals.pList = new PersonalZoneLists();
            Globals.pList.allergiesList = new List<Allergy>(listAllergies);
            Globals.pList.preferencesList = new List<Preference>(listPreferences);            

            return Globals.pList;
        }

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
            Globals.ListToItemArray(Globals.pList);
            EntitiesManager.getInstance().UpdateUsersZone(value);

            return CreatedAtRoute("DefaultApi", new { id = pz.userId }, pz);
        }
    }
}