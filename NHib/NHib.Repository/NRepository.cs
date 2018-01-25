using NHib.Factory;
using NHib.Repository;
using NHib.Repository.Interfaces;
using NHib.Types.Entity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NHib.Repository
{
    public class NRepository : INRepository
    {
        NHibernateFactory factory = null;

        public NRepository()
        {
            Assembly ass = Assembly.Load("NHib.Types, Version=1.0.0.0, Culture=neutral, PublicKeyToken=92be0c10c668dbeb");

            Dictionary<string, string> drivers = new Dictionary<string, string> {
                { "connection.provider",ConfigurationManager.AppSettings["nhib-provider"] },//NHibernate.Connection.DriverConnectionProvider
                { "connection.driver_class",ConfigurationManager.AppSettings["nhib-driver"] },//NHibernate.Driver.MySqlDataDriver
                { "connection.connection_string",ConfigurationManager.AppSettings["nhib-connection_string"] },//Server=127.0.0.1;database=SrkTestServiceDb;Uid=root;Pwd=;
                { "dialect",ConfigurationManager.AppSettings["nhib-dialect"] } //NHibernate.Dialect.MySQL5Dialect                
            };
            factory = new NHibernateFactory(drivers, ass);
            factory.Configure();
        }

        public object Save<T>(T t)
        {
            object obj = null;

            using (ISession session = factory.SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        obj = session.Save(t);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return obj;
        }
    }
}
