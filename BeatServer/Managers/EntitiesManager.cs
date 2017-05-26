﻿using BeatServer.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeatServer.Managers
{
    public class EntitiesManager
    {
        private static EntitiesManager m_instance;
        public static Dictionary<string, int> allergyArray;
        public static Dictionary<string, int> preferenceArray;

        public static EntitiesManager getInstance()
        {
            if (m_instance == null)
            {
                m_instance = new EntitiesManager();
            }

            return m_instance;
        }

        private EntitiesManager()
        {
        }

        #region login


        public IEnumerable<User> GetUsers()
        {
            using (var session = NHibernateManager.OpenSession())
            {
                IList<User> users = session.CreateCriteria<User>().List<User>();
                IList<int> l = users[0].AllergiesIdList;
                l.Add(1);
                users[0].AllergiesIdList = l;

                IList<Preference> preferences = session.CreateCriteria<Preference>().List<Preference>();
                IList<Allergy> allergies = session.CreateCriteria<Allergy>().List<Allergy>();
                IList<Mealtype> mealtypes = session.CreateCriteria<Mealtype>().List<Mealtype>();
                IList<Foodgroup> foodgroup = session.CreateCriteria<Foodgroup>().List<Foodgroup>();
                IList<Menu> menues = session.CreateCriteria<Menu>().List<Menu>();
                IList<Nutrient> nutrients = session.CreateCriteria<Nutrient>().List<Nutrient>();
                IList<Food> food = session.CreateCriteria<Food>().List<Food>();
                return users;
            }
        }

        public User Login(LoginUser details)
        {
            User ret = null;

            using (var session = NHibernateManager.OpenSession())
            {
                IList<User> lst = session.CreateCriteria<User>()
                    .Add(Expression.And(Expression.Eq("Email", details.email), Expression.Eq("Password", details.password)))
                    .List<User>();

                if (lst.Count > 0)
                {
                    ret = lst.First();
                }
            }

            return ret;
        }

        public User Register(User details)
        {
            User ret = null;

            using (var session = NHibernateManager.OpenSession())
            {
                IList<User> lst = session.CreateCriteria<User>()
                    .Add(Expression.Eq("Email", details.Email))
                    .List<User>();

                // if the email isn't in use save the new user
                if (lst.Count == 0)
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        try
                        {
                            ret = details;
                            session.Save(ret);
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            List<Exception> list = new List<Exception>();
                            list.Add(e);
                            transaction.Rollback();
                        }
                    }
                }
            }

            return ret;
        }

        public User GetUser(int id)
        {
            User ret = null;

            using (var session = NHibernateManager.OpenSession())
            {
                IList<User> lst = session.CreateCriteria<User>()
                    .Add(Expression.Eq("UserId", id))
                    .List<User>();

                if (lst.Count == 1)
                {
                    ret = lst.First();
                }
            }
            return ret;
        }
        #endregion

        #region PersonalZone
        public IList<Allergy> GetAllergies()
        {
            using (var session = NHibernateManager.OpenSession())
            {
                IList<Allergy> allergies = session.CreateCriteria<Allergy>().List<Allergy>();
                return allergies;
            }
        }

        public IList<Preference> GetPreferences()
        {
            using (var session = NHibernateManager.OpenSession())
            {
                IList<Preference> preferences = session.CreateCriteria<Preference>().List<Preference>();
                return preferences;
            }
        }


        public User UpdateUsersZone(PersonalZone details)
        {
            User ret = GetUser(details.userId);

            using (var session = NHibernateManager.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    ret.Allergies = ConvertOptionNameToId(details.userAllergies, allergyArray);
                    ret.Preferences = ConvertOptionNameToId(details.userPreferences, preferenceArray);
                    try
                    {                        
                        session.Update(ret);
                        //session.Update("Preferences", ret.Preferences, details.userId);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return ret;
        }


        public void ListToItemArray(PersonalZoneLists lists)
        {
            allergyArray = new Dictionary<string, int>();
            allergyArray["None"] = 0;
            foreach (var item in lists.allergiesList)
            {
                allergyArray[item.Name] = item.Id;
            }

            preferenceArray = new Dictionary<string, int>();
            preferenceArray["None"] = 0;
            foreach (var item in lists.preferencesList)
            {
                preferenceArray[item.Name] = item.Id;
            }
        }

        public string ConvertOptionNameToId(List<string> userItems, Dictionary<string, int> generalArray)
        {
            string stringIds = "";
            foreach (var item in userItems)
            {
                stringIds += generalArray[item] + ",";
            }

            if (string.Empty != stringIds)
            {
                char[] MyChar = { ',' };
                stringIds = stringIds.TrimEnd(MyChar);
            }
            return stringIds;
        }

        public void SaveBloodResultsLacks(List<int> lacks, int userId)
        {
            using (var session = NHibernateManager.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        User user = GetUser(userId);
                        user.NutrientLacksList = lacks;
                        session.Update(user);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        List<Exception> list = new List<Exception>();
                        list.Add(e);
                        transaction.Rollback();
                    }
                }
                
            }
        }

        public int GetNutrientIdByName(string name)
        {
            int ret = -1;

            using (var session = NHibernateManager.OpenSession())
            {
                IList<Nutrient> lst = session.CreateCriteria<Nutrient>()
                    .Add(Expression.Eq("Name", name))
                    .List<Nutrient>();

                if (lst.Count == 1)
                {
                    ret = lst.First().Id;
                }
            }
            return ret;
        }
        #endregion

        #region Meal generator

        public List<Food> GetFoods(int NumOfFoods, MealTypes mainType)
        {
            using (var session = NHibernateManager.OpenSession())
            {
                List<Food> result = new List<Food>();

                IList<Food> foodList = session.CreateCriteria<Food>()
                    .Add(Expression.Or(Expression.Eq("MealType", mainType), Expression.Eq("MealType", MealTypes.Everything)))
                    .List<Food>();

                if (foodList.Count > 0)
                {
                    Random Rand = new Random();

                    for (int i = 0; i < NumOfFoods; i++)
                    {
                        int ChosenIndex = Rand.Next(0, foodList.Count - 1);

                        result.Add(foodList[ChosenIndex]);
                    }
                }

                return result;
            }

            
        }

        #endregion
    }
}