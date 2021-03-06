﻿using BeatServer.Managers;
using BeatServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace BeatServer.Genetic_algorithm
{
    public class Population
    {
        public Menu[] Menues { get; set; }
        private int NumOfChildren = 2;
        private Random rand;

        public Population(bool Random= true)
        {
            Menues = new Menu[Globals.PopulationSize];
            rand = new System.Random();

            if (Random)
            {
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
        }
        
        public Population GetNextGeneration()
        {
            Population NextGeneration = new Population(false);
            this.Menues = this.Menues.OrderByDescending(x => x.Score).ToArray();

            for (int i = 0; i < Globals.PopulationSize; i += NumOfChildren)
            {
                // get 2 random menues and create 2 new menues from them
                Menu[] Children = GetChildren(GetRandomMenuId(), GetRandomMenuId());
                NextGeneration.Menues[i] = Children[0];
                NextGeneration.Menues[i + 1] = Children[1];
            }

            return NextGeneration;
        }

        private Menu GetRandomMenuId()
        {
            int ScoresSum = Menues.Sum(x => x.Score);

            int randValue = rand.Next(1, ScoresSum);

            int Jump = 0;
            Menu ChosenMenu = null;

            foreach (Menu CurrMenu in Menues)
            {
                Jump += CurrMenu.Score;

                if (Jump > randValue)
                {
                    ChosenMenu =  CurrMenu;
                    break;
                }
            }

            return ChosenMenu;

        }

        private Menu[] GetChildren(Menu Mother, Menu Father)
        {
            Menu[] Children = new Menu[NumOfChildren];
            List<Meal> FirstChildMealList = new List<Meal>();
            List<Meal> SecondChildMealList = new List<Meal>();

            // go over the mother and father meals and combine them to create children meals
            for (int i = 0; i < Mother.MealList.Count; i++)
            {
                List<Food> FirstMealFoods = new List<Food>();
                List<Food> SecondMealFoods = new List<Food>();

                // put the mother and father foods in the children meal switching order each time
                for (int j = 0; j < Mother.MealList[i].FoodsList.Count; j++)
                {
                    if (j % 2 == 0)
                    {
                        FirstMealFoods.Add(Mother.MealList[i].FoodsList[j]);
                        SecondMealFoods.Add(Father.MealList[i].FoodsList[j]);
                    }
                    else
                    {
                        FirstMealFoods.Add(Father.MealList[i].FoodsList[j]);
                        SecondMealFoods.Add(Mother.MealList[i].FoodsList[j]);
                    }
                }

                FirstChildMealList.Add(new Meal(Mother.MealList[i].FoodsList.Count, Mother.MealList[i].Type, FirstMealFoods));
                SecondChildMealList.Add(new Meal(Mother.MealList[i].FoodsList.Count, Mother.MealList[i].Type, SecondMealFoods));

            }

            Children[0] = new Menu(FirstChildMealList[0], FirstChildMealList[1], FirstChildMealList[2], FirstChildMealList[3], FirstChildMealList[4]);
            Children[1] = new Menu(SecondChildMealList[0], SecondChildMealList[1], SecondChildMealList[2], SecondChildMealList[3], SecondChildMealList[4]);

            //MutateByProbability(ref Children[0]);
            //MutateByProbability(ref Children[1]);

            return Children;
        }

        public void MutateByProbability(ref Menu CurrMenu)
        {


            if (rand.Next(1,100) <= Globals.MutationProbability)
            {
                // Get 1 new random food replacement for each meal
                foreach (Meal CurrMeal in CurrMenu.MealList)
                {
                    int EaraseIndex;
                    
                    if (CurrMeal.Type == MealTypes.Snack)
                    {
                        EaraseIndex = rand.Next(1, Globals.SmallMealFoodCount) - 1;
                    }
                    else
                    {
                        EaraseIndex = rand.Next(1, Globals.MainMealFoodCount) - 1;
                    }
                    
                    CurrMeal.FoodsList[EaraseIndex] = EntitiesManager.getInstance().GetFoods(1, CurrMeal.Type).First();
                }
            }
        }
    }
}