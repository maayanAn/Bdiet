using BeatServer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeatServer.Models
{
    public class Meal
    {
        public List<Food> FoodsList { get; set; }
        public MealTypes Type { get; set; }

        public Meal(int NumOfFoods, MealTypes MainType, List<Food> List = null)
        {
            if (List ==  null)
            {
                // get random food items
                FoodsList = EntitiesManager.getInstance().GetFoods(NumOfFoods, MainType);
            }
            else
            {
                FoodsList = List;
            }
            
            Type = MainType;
        }

        public override string ToString()
        {
            string result = "";
            FoodsList.ForEach(x => result += string.Format("{0} of {1}, ", x.Amount, x.Name));
            result = result.TrimEnd(new char[] { ',',' ' });

            return result;
        }
    }
}