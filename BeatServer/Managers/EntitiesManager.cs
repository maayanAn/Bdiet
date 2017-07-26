using BeatServer.Models;
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
        private static Random Rand;
        public static IList<Mealtype> MealTypesList = null;
        public static IList<Foodgroup> FoodGroupList = null;
        public static Dictionary<Mealtype, IList<Food>> FoodsByMeal = new Dictionary<Mealtype, IList<Food>>();

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
            Rand = new Random();
        }

        #region login

        public IEnumerable<User> GetUsers()
        {
            using (var session = NHibernateManager.OpenSession())
            {
                IList<User> users = session.CreateCriteria<User>().List<User>();

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
                        // try to save the new user
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
            User ret = GetUser(details.UserId);

            using (var session = NHibernateManager.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    ret.Allergies = ConvertOptionNameToId(details.userAllergies, Globals.allergyArray);
                    ret.Preferences = ConvertOptionNameToId(details.userPreferences, Globals.preferenceArray);

                    // try to update the user in the DB
                    try
                    {                        
                        session.Update(ret);                        
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

        public string ConvertOptionNameToId(List<string> userItems, Dictionary<string, int> generalArray)
        {
            string stringIds = "";

            if (userItems[0].Equals("None"))
            {
                stringIds = null;
            }
            else
            {
                foreach (var item in userItems)
                {
                    stringIds += generalArray[item] + ",";
                }

                if (string.Empty != stringIds)
                {
                    char[] MyChar = { ',' };
                    stringIds = stringIds.TrimEnd(MyChar);
                }
            }
            return stringIds;
        }

        public void SaveBloodResultsLacks(List<int> lacks, int UserId)
        {
            using (var session = NHibernateManager.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        User user = GetUser(UserId);

                        if (lacks != null)
                            user.NutrientLacks = string.Join(",", lacks.ToArray());
                        else
                            user.NutrientLacks = null;

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

        // Get the requested num of foods of the specific meal type randomly
        public List<Food> GetFoods(int NumOfFoods, MealTypes mainType)
        {
            using (var session = NHibernateManager.OpenSession())
            {
                List<Food> result = new List<Food>();

                // get the meal type list (only happens once)
                if (MealTypesList == null)
                {
                    MealTypesList = session.CreateCriteria<Mealtype>().List<Mealtype>();
                }

                Mealtype currMealType = MealTypesList.Where(x => x.Id == (int)mainType).First();

                IList<Food> foodList;

                // get the meal type food items (only once per meal type)
                if (!FoodsByMeal.ContainsKey(currMealType))
                {
                    if (mainType == MealTypes.Snack)
                    {
                        // geting only the items that are snacks
                        foodList = session.CreateCriteria<Food>()
                        .Add(Expression.Eq("MealType", currMealType))
                        .List<Food>();
                    }
                    else
                    {
                        // in all other meal types we want to get the meal type food items plus the everything category
                        foodList = session.CreateCriteria<Food>()
                        .Add(Expression.Or(Expression.Eq("MealType", currMealType),
                        Expression.Eq("MealType", MealTypesList.Where(x => x.Id == (int)MealTypes.Everything).First())))
                        .List<Food>();
                    }

                    // save the food items localy
                    FoodsByMeal[currMealType] = foodList;
                }
                // if the items where already pulled from the DB
                else
                {
                    foodList = FoodsByMeal[currMealType];
                }


                if (foodList.Count > 0)
                {
                    // randomly choose the requesed number of foods from the group
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