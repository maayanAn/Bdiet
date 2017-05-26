using BeatServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeatServer.Genetic_algorithm
{
    public class Population
    {
        public Menu[] Menues { get; set; }

        public Population()
        {
            Menues = new Menu[Globals.PopulationSize];

            for (int i = 0; i < Globals.PopulationSize; i++)
            {
                Meal Breakfast = new Meal(Globals.MainMealFoodCount, MealTypes.MorningOrEvening);
                Meal MidMorning = new Meal(Globals.SmallMealFoodCount, MealTypes.Snack);
                Meal Lunch = new Meal(Globals.MainMealFoodCount, MealTypes.Noon);
                Meal Afternoon = new Meal(Globals.SmallMealFoodCount, MealTypes.Snack);
                Meal Dinner = new Meal(Globals.MainMealFoodCount, MealTypes.MorningOrEvening);

                Menu CurrMenu = new Menu(Breakfast, MidMorning, Lunch, Afternoon, Dinner);
                Menues[i] = CurrMenu;
            }
        }

        public Population GetNextGeneration()
        {
            return this;
        }
    }
}