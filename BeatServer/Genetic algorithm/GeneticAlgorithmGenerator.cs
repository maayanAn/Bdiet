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

            // running according to the number of generations requiered
            for (int j = 1; j < Globals.NumOfGenerations; j++)
            {
                // calculating the fitness of each menu
                for (int i = 0; i < population.Menues.Length; i++)
                {
                    SetCalcFitness(ref population.Menues[i]);

                }

                // save the best menu of the generation
                Menu IterationBestMenu = population.Menues.Where(x => x.Score == population.Menues.Max(y => y.Score)).First();

                if (CurrBestMenu == null || IterationBestMenu.Score > CurrBestMenu.Score)
                {
                    CurrBestMenu = IterationBestMenu;
                }

                // start the next generation (every time except the last one)
                if (j != Globals.NumOfGenerations-1)
                {
                    population = population.GetNextGeneration();
                }
            }

            CurrBestMenu.User = User;

            return CurrBestMenu;
        }

        private int SetCalcFitness(ref Menu menu)
        {
            int Score = 0;
            List<Food> foodList = new List<Food>();

            // food group check
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

                            if (FoodGroupsInMeal.Sum() == 3)
                            {
                                Score += (FoodGroupsInMeal.Sum() * 3);
                            }
                            else
                            {
                                Score += FoodGroupsInMeal.Sum();
                            }
                            
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
                                    FoodItem.FoodGroup.Id == (int)FoodGroups.Fish ||
                                    FoodItem.FoodGroup.Id == (int)FoodGroups.Vegan_protein)
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

                            if (FoodGroupsInMeal.Sum() == 3)
                            {
                                Score += (FoodGroupsInMeal.Sum() * 3);
                            }
                            else
                            {
                                Score += FoodGroupsInMeal.Sum();
                            }
                            break;
                        }
                    case MealTypes.Snack:
                        {
                            int[] FoodGroupsInMeal = new int[5];

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
                                // carbs
                                if (FoodItem.FoodGroup.Id == (int)FoodGroups.Carbohydrates)
                                {
                                    FoodGroupsInMeal[2] = 1;
                                }
                                // cheese
                                if (FoodItem.FoodGroup.Id == (int)FoodGroups.Cheese)
                                {
                                    FoodGroupsInMeal[3] = 1;
                                }
                                // cereal
                                if (FoodItem.FoodGroup.Id == (int)FoodGroups.Cereals)
                                {
                                    FoodGroupsInMeal[4] = 1;
                                }
                            }

                            if (FoodGroupsInMeal.Sum() == 2)
                            {
                                Score += (FoodGroupsInMeal.Sum() * 3);
                            }
                            else
                            {
                                Score += FoodGroupsInMeal.Sum();
                            }
                            break;
                        }
                    default:
                        break;
                }
                
                foodList.AddRange(currMeal.FoodsList);
            }

            foodList = foodList.Distinct().ToList();

            // versatility check
            Score += foodList.Count * 2;

            foreach (Food foodItem in foodList)
            {
                // nutrient lack check
                foreach (int lack in User.NutrientLacksList)
                {
                    if (foodItem.NutrientsIdList.Contains(lack))
                    {
                        Score++;
                    }
                }

                // cholesterol check
                if (User.NutrientLacksList.Contains(9) && !foodItem.NutrientsIdList.Contains(9))
                {
                    Score += 2;
                }

                // allergies check
                foreach (int allergy in User.AllergiesIdList)
                {
                    if (foodItem.AllergessIdList.Contains(allergy))
                    {
                        Score = 0;
                    }
                }

                // prefernces check
                bool isPreferenceFound = true;
                
                foreach (int preference in User.PreferencesIdList)
                {
                    if (!foodItem.PreferencesIdList.Contains(preference))
                    {
                        isPreferenceFound = false;
                    }
                }

                if (!isPreferenceFound)
                {
                    Score = 0;
                }

            }

            
            if (Score <= 0)
            {
                Score = 1;
            }
            
            menu.Score = Score;

            return Score;
        }
    }
}