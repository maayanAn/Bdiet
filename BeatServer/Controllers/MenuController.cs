using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BeatServer.Models;
using BeatServer.Genetic_algorithm;
using BeatServer.Managers;

namespace BeatServer.Controllers
{
    [RoutePrefix("api/Menu")]
    public class MenuController : ApiController
    {
        // GET api/<controller>
        public Menu Get([FromUri] int id)
        {
            // check that we got a real user id
            if (id == 0)
            {
                return null;
            }

            // initialize the GA generator
            GeneticAlgorithmGenerator GA = new GeneticAlgorithmGenerator(EntitiesManager.getInstance().GetUser(id));

            // create the menu
            Menu CurrMenu = GA.RunAlgorithm();

            return CurrMenu;
        }

    
    }
}