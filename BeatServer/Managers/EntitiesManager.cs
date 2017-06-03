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
        //public static Dictionary<string, int> allergyArray;
        //public static Dictionary<string, int> preferenceArray;
        public static IList<Mealtype> MealTypesList = null;
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
                //IList<int> l = users[0].AllergiesIdList;
                //l.Add(1);
                //users[0].AllergiesIdList = l;

                //IList<Preference> preferences = session.CreateCriteria<Preference>().List<Preference>();
                //IList<Allergy> allergies = session.CreateCriteria<Allergy>().List<Allergy>();
                //IList<Mealtype> mealtypes = session.CreateCriteria<Mealtype>().List<Mealtype>();
                //IList<Foodgroup> foodgroup = session.CreateCriteria<Foodgroup>().List<Foodgroup>();
                //IList<Menu> menues = session.CreateCriteria<Menu>().List<Menu>();
                //IList<Nutrient> nutrients = session.CreateCriteria<Nutrient>().List<Nutrient>();
                //IList<Food> food = session.CreateCriteria<Food>().List<Food>();
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
                    ret.Allergies = ConvertOptionNameToId(details.userAllergies, Globals.allergyArray);
                    ret.Preferences = ConvertOptionNameToId(details.userPreferences, Globals.preferenceArray);
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


        //public void ListToItemArray(PersonalZoneLists lists)
        //{
        //    Globals.allergyArray =new Dictionary<string, int>();     
        //    foreach (var item in lists.allergiesList)
        //    {
        //        Globals.allergyArray[item.Name] = item.Id;
        //    }

        //    Globals.preferenceArray = new Dictionary<string, int>();
        //    foreach (var item in lists.preferencesList)
        //    {
        //        Globals.preferenceArray[item.Name] = item.Id;
        //    }
        //}

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

        public void SaveBloodResultsLacks(List<int> lacks, int userId)
        {
            using (var session = NHibernateManager.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        User user = GetUser(userId);
                        //user.NutrientLacksList = lacks;
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

        public List<Food> GetFoods(int NumOfFoods, MealTypes mainType)
        {
            using (var session = NHibernateManager.OpenSession())
            {
                List<Food> result = new List<Food>();

                if (MealTypesList == null)
                {
                    MealTypesList = session.CreateCriteria<Mealtype>().List<Mealtype>();
                }

                Mealtype currMealType = MealTypesList.Where(x => x.Id == (int)mainType).First();
                IList<Food> foodList;

                if (!FoodsByMeal.ContainsKey(currMealType))
                {

                    if (mainType == MealTypes.Snack)
                    {

                        foodList = session.CreateCriteria<Food>()
                        .Add(Expression.Eq("MealType", currMealType))
                        .List<Food>();
                    }
                    else
                    {
                        foodList = session.CreateCriteria<Food>()
                        .Add(Expression.Or(Expression.Eq("MealType", currMealType),
                        Expression.Eq("MealType", MealTypesList.Where(x => x.Id == (int)MealTypes.Everything).First())))
                        .List<Food>();
                    }

                FoodsByMeal[currMealType] = foodList;
            }
                else
                {
                foodList = FoodsByMeal[currMealType];
            }


            if (foodList.Count > 0)
                {

                    for (int i = 0; i < NumOfFoods; i++)
                    {
                        int ChosenIndex = Rand.Next(0, foodList.Count - 1);

                        result.Add(foodList[ChosenIndex]);
                    }
                }

                return result;
            }   
        }

        public Food GetFoodByGroup(MealTypes mainType, int foodGroup)
        {
            using (var session = NHibernateManager.OpenSession())
            {
                IList<Food> foodList = session.CreateCriteria<Food>()
                .Add(Expression.Or(Expression.Eq("MealType", mainType), Expression.Eq("MealType", MealTypes.Everything)))
                .Add(Expression.Eq("FoodGroup", foodGroup))
                .List<Food>();

                Random Rand = new Random();

                int ChosenIndex = Rand.Next(0, foodList.Count - 1);

                return foodList[ChosenIndex];
                
            }
        }

        #endregion
    }
}