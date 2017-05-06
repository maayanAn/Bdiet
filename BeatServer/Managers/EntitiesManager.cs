using BeatServer.Models;
using NHibernate;
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
            UsersTemp = new List<User>();
            User u = new User();
            u.user_id = 1;
            u.name = "Yossi";
            u.password = "123";
            u.email = "yossi@gmail.com";
            UsersTemp.Add(u);
        }

        #region login

        private List<User> UsersTemp;
        
        public IEnumerable<User> GetUsers()
        {
            using (var session = NHibernateManager.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<User>();
                return criteria.List<User>();
            }
        }

        public User Login(LoginUser details)
        {
            User ret = null;

            foreach (User item in UsersTemp)
            {
                if (item.email == details.email && item.password == details.password)
                {
                    ret = item;
                }
            }

            return ret;
        }

        private int getNextId()
        {
            int id = 0;
            for (int i = 0; i < UsersTemp.Count; i++)
            {
                if (UsersTemp[i].user_id > id)
                {
                    id = UsersTemp[i].user_id;
                }
            }

            return ++id;
        }
        public User Register(User details)
        {
            User ret = null;
            bool exists = false;

            foreach (User item in UsersTemp)
            {
                if (item.email == details.email)
                {
                    exists = true;
                }
            }

            if (!exists)
            {
                ret = details;
                ret.user_id = getNextId();
                UsersTemp.Add(ret);

            }

            return ret;
        }

        #endregion
    }
}