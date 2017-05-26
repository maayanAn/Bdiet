using BeatServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeatServer.Genetic_algorithm
{
    public class GeneticAlgorithmGenerator
    {
        public User User { get; set; }
        private Menu CurrBestMenu { get; set; }

        public GeneticAlgorithmGenerator(User user)
        {
            User = user;
        }
        
        public Menu RunAlgorithm()
        {
            Population population = new Population();

            for (int j = 1; j < Globals.NumOfGenerations; j++)
            {

                for (int i = 0; i < population.Menues.Length; i++)
                {
                    SetCalcFitness(ref population.Menues[i]);

                }

                CurrBestMenu = population.Menues.Where(x => x.Score == population.Menues.Max(y => y.Score)).First();

                if (j != Globals.NumOfGenerations-1)
                {
                    population = population.GetNextGeneration();
                }
                
            }

            return CurrBestMenu;
            
        }

        private int SetCalcFitness(ref Menu menu)
        {
            int Score = 0;
            List<Food> foodList = new List<Food>();

            // בדיקת אבות המזון
            foreach (Meal currMeal in menu.MealList)
            {

                switch (currMeal.Type)
                {
                    case MealTypes.Everything:
                        break;
                    case MealTypes.Noon:
                        {
                            int[] FoodGroupsInMeal = new int[3];

                            foreach (Food FoodItem in currMeal.FoodsList)
                            {
                                // protein
                                if (FoodItem.FoodGroup.Id == (int)FoodGroups.Meat ||
                                    FoodItem.FoodGroup.Id == (int)FoodGroups.Vegan_protein ||
                                    FoodItem.FoodGroup.Id == (int)FoodGroups.Fish)
                                {
                                    FoodGroupsInMeal[0] = 1;
                                }
                                // carbohydrate
                                if (FoodItem.FoodGroup.Id == (int)FoodGroups.Carbohydrates ||
                                    FoodItem.FoodGroup.Id == (int)FoodGroups.Legumes)
                                {
                                    FoodGroupsInMeal[1] = 1;
                                }

                                // vegetble
                                if (FoodItem.FoodGroup.Id == (int)FoodGroups.Vegetables)
                                {
                                    FoodGroupsInMeal[2] = 1;
                                }
                            }

                            Score += FoodGroupsInMeal.Sum();
                            break;
                        }
                    case MealTypes.MorningOrEvening:
                        {
                            int[] FoodGroupsInMeal = new int[3];

                            foreach (Food FoodItem in currMeal.FoodsList)
                            {
                                // protein
                                if (FoodItem.FoodGroup.Id == (int)FoodGroups.Cheese || 
                                    FoodItem.FoodGroup.Id == (int)FoodGroups.Eggs || 
                                    FoodItem.FoodGroup.Id == (int)FoodGroups.Fish)
                                {
                                    FoodGroupsInMeal[0] = 1;
                                }
                                // carbohydrate
                                if (FoodItem.FoodGroup.Id == (int)FoodGroups.Carbohydrates || 
                                    FoodItem.FoodGroup.Id == (int)FoodGroups.Cereals)
                                {
                                    FoodGroupsInMeal[1] = 1;
                                }

                                // vegetble
                                if (FoodItem.FoodGroup.Id == (int)FoodGroups.Vegetables)
                                {
                                    FoodGroupsInMeal[2] = 1;
                                }
                            }

                            Score += FoodGroupsInMeal.Sum();
                            break;
                        }
                    case MealTypes.Snack:
                        {
                            int[] FoodGroupsInMeal = new int[2];

                            foreach (Food FoodItem in currMeal.FoodsList)
                            {
                                // fats
                                if (FoodItem.FoodGroup.Id == (int)FoodGroups.Fats)
                                {
                                    FoodGroupsInMeal[0] = 1;
                                }
                                // fruits
                                if (FoodItem.FoodGroup.Id == (int)FoodGroups.Fruits)
                                {
                                    FoodGroupsInMeal[1] = 1;
                                }
                            }

                            Score += FoodGroupsInMeal.Sum();
                            break;
                        }
                    default:
                        break;
                }

                // create food list
                foodList.AddRange(currMeal.FoodsList);
            }

            foreach (Food foodItem in foodList)
            {
                // בדיקת חוסרים
                foreach (int lack in User.NutrientLacksList)
                {
                    if (foodItem.NutrientsIdList.Contains(lack))
                    {
                        Score++;
                    }
                }


                // בדיקת אלרגיות

                bool isAllergyFound = false;

                foreach (int allergy in User.AllergiesIdList)
                {
                    if (foodItem.AllergessIdList.Contains(allergy))
                    {
                        isAllergyFound = true;
                    }
                }

                if (!isAllergyFound)
                {
                    Score += 3;
                }

                // בדיקת העדפות
                bool isPreferenceFound = false;

                foreach (int preference in User.PreferencesIdList)
                {
                    if (foodItem.PreferencesIdList.Contains(preference))
                    {
                        isPreferenceFound = true;
                    }
                }

                if (isPreferenceFound)
                {
                    Score += 2;
                }

                // בדיקת כולסטרול
                if (User.NutrientLacksList.Contains(9) && !foodItem.NutrientsIdList.Contains(9))
                {
                    Score += 2;
                }
                
            }

            // בדיקת גיוון
            Score += foodList.Count;

            menu.Score = Score;

            return Score;
        }
    }
}