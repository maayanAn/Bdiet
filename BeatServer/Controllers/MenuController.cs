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
            if (id == 0)
            {
                return null;
            }

            GeneticAlgorithmGenerator GA = new GeneticAlgorithmGenerator(EntitiesManager.getInstance().GetUser(id));
            Menu CurrMenu = GA.RunAlgorithm();

            return CurrMenu;
        }

    
    }
}