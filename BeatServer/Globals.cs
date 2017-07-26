using BeatServer.Models;
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
        public static Dictionary<string, int> allergyArray;
        public static Dictionary<string, int> preferenceArray;
        public static PersonalZoneLists pList;


        public static void ListToItemArray(PersonalZoneLists lists)
        {
            allergyArray = new Dictionary<string, int>();
            foreach (var item in lists.allergiesList)
            {
                allergyArray[item.Name] = item.Id;
            }

            preferenceArray = new Dictionary<string, int>();
            foreach (var item in lists.preferencesList)
            {
                preferenceArray[item.Name] = item.Id;
            }
        }

        public static List<int> CommaSeparatedStringToIntList(string list)
        {
            return list.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        }
    }
}