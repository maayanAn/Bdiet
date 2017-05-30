using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeatServer
{
    public enum MealTypes
    {
        Everything = 1,
        Noon,
        MorningOrEvening,
        Snack
    }

    public enum FoodGroups
    {
        Vegan_protein = 1,
        Carbohydrates,
        Legumes,
        Cereals,
        Cheese,
        Meat,
        Fish,
        Vegetables,
        Fruits,
        Eggs,
        Fats
    }
    public static class Globals
    {
        public static int MainMealFoodCount = 3;
        public static int SmallMealFoodCount = 2;
        public static int PopulationSize = 200;
        public static int NumOfGenerations = 200;
        public static int MutationProbability = 5;
    }
}