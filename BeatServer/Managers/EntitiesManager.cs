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

                            transaction.Rollback();
                        }
                    }
                }
            }

            return ret;
        }

        #endregion
    }
}