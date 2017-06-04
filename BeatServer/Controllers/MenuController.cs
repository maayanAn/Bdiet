using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BeatServer.Models;


namespace BeatServer.Controllers
{
    [RoutePrefix("api/Menu")]
    public class MenuController : ApiController
    {

        //public Menu getMenu()
        //{
        //    Meal Breakfast = new Meal(MealTypes.MorningOrEvening, new int[] { (int)FoodGroups.Cheese,
        //                                                                      (int)FoodGroups.Carbohydrates,
        //                                                                      (int)FoodGroups.Vegetables});
        //    Meal MidMorning = new Meal(MealTypes.Snack, new int[] { (int)FoodGroups.Fats,
        //                                                            (int)FoodGroups.Fruits });
        //    Meal Lunch = new Meal(MealTypes.Noon, new int[] { (int)FoodGroups.Meat,
        //                                                      (int)FoodGroups.Legumes,
        //                                                      (int)FoodGroups.Vegetables });
        //    Meal Afternoon = new Meal(MealTypes.Snack, new int[] { (int)FoodGroups.Fats,
        //                                                            (int)FoodGroups.Fruits });
        //    Meal Dinner = new Meal(MealTypes.MorningOrEvening, new int[] { (int)FoodGroups.Cheese,
        //                                                                      (int)FoodGroups.Carbohydrates,
        //                                                                      (int)FoodGroups.Vegetables});

        //    Menu CurrMenu = new Menu(Breakfast, MidMorning, Lunch, Afternoon, Dinner);

        //    return CurrMenu;
        //}

        // GET api/<controller>
        public Menu Get()
        {
            Meal Breakfast = new Meal(MealTypes.MorningOrEvening, new int[] { (int)FoodGroups.Cheese,
                                                                              (int)FoodGroups.Carbohydrates,
                                                                              (int)FoodGroups.Vegetables});
            Meal MidMorning = new Meal(MealTypes.Snack, new int[] { (int)FoodGroups.Fats,
                                                                    (int)FoodGroups.Fruits });
            Meal Lunch = new Meal(MealTypes.Noon, new int[] { (int)FoodGroups.Meat,
                                                              (int)FoodGroups.Legumes,
                                                              (int)FoodGroups.Vegetables });
            Meal Afternoon = new Meal(MealTypes.Snack, new int[] { (int)FoodGroups.Fats,
                                                                    (int)FoodGroups.Fruits });
            Meal Dinner = new Meal(MealTypes.MorningOrEvening, new int[] { (int)FoodGroups.Cheese,
                                                                              (int)FoodGroups.Carbohydrates,
                                                                              (int)FoodGroups.Vegetables});

            Menu CurrMenu = new Menu(Breakfast, MidMorning, Lunch, Afternoon, Dinner);

            return CurrMenu;
        }

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}