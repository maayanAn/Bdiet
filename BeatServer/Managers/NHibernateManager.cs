using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using NHibernate.SqlCommand;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Mapping;
using System.Web.Hosting;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using NHibernate.Proxy.DynamicProxy;
using BeatServer.Models;
using System.Reflection;

namespace BeatServer.Managers
{
    public class NHibernateManager
    {
        private static NHibernateManager m_instance = new NHibernateManager();
       
        public static NHibernateManager Instance
        {
            get
            {
                return (m_instance);
            }
            private set
            {

            }
        }

        private static ISessionFactory m_sessionFactory;
        public static ISessionFactory SessionFactory
        {
            get
            {
                if (m_sessionFactory == null)
                {
                    m_sessionFactory = BuildSessionFactory();
                }

                return m_sessionFactory;
            }
            private set
            {
                m_sessionFactory = value;
            }
        }

        protected static ISessionFactory BuildSessionFactory()
        {
            IDictionary<string, string> props = new Dictionary<string, string>();

            var cfg = new NHibernate.Cfg.Configuration().AddProperties(props);

            cfg.SetInterceptor(new Intercepter());

            try
            {
                var ch = Fluently.Configure().Database(MySQLConfiguration.Standard.ConnectionString(c => c.FromAppSetting("NHBCon"))
                .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                .ExposeConfiguration(conf => new SchemaUpdate(conf).Execute(true, true))
                .BuildSessionFactory();
                return ch;
            }
            catch (Exception e)
            {
            }

            return null;
        }

        private static void BuildSchema(Configuration config)
        {

        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }

    public class Intercepter : EmptyInterceptor, NHibernate.IInterceptor
    {
        SqlString NHibernate.IInterceptor.OnPrepareStatement(SqlString sql)
        {
            return sql;
        }

        public override void BeforeTransactionCompletion(ITransaction tx)
        {
            base.BeforeTransactionCompletion(tx);
        }
    }
}

