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
                FoodsList = EntitiesManager.getInstance().GetFoods(NumOfFoods, MainType);
            }
            else
            {
                FoodsList = List;
            }
            
            Type = MainType;
        }

        public Meal(MealTypes MainType, int[] foodGroupId)
        {
            Food currFood;
            FoodsList = new List<Food>();
            Type = MainType;

            for (int i = 0; i < foodGroupId.Length; i++)
            {
                currFood = EntitiesManager.getInstance().GetFoodByGroup(MainType, foodGroupId[i]);
                FoodsList.Add(currFood);
            }
        }

        public override string ToString()
        {
            string result = "";
            FoodsList.ForEach(x => result += string.Format("{0} of {1}, ", x.Amount, x.Name));
            result.TrimEnd(new char[] { ',' });

            return result;
        }
    }
}