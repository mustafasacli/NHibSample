using NHib.Core.Entity;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NHib.Factory
{
    public class NHibernateFactory
    {
        private Assembly pAssembly = null;
        private Dictionary<string, string> pDriverSettings = null;
        //private string pConfigFileFullName = null;

        private ISessionFactory sessionFactory = null;
        private object objLock2 = new object();

        public NHibernateFactory(Dictionary<string, string> driverSettings, Assembly typesAssembly)
        {
            if (driverSettings == null || driverSettings.Count < 1)
                throw new ArgumentNullException(nameof(driverSettings));

            if (typesAssembly == null || typesAssembly.GetExportedTypes().Length < 1)
                throw new ArgumentNullException(nameof(typesAssembly));

            pDriverSettings = driverSettings;
            pAssembly = typesAssembly;
        }

        /*

        public NHibernateFactory(string configFileFullName, Assembly typesAssembly)
        {
            pConfigFileFullName = configFileFullName;
            pAssembly = typesAssembly;
        }

        */

        public virtual void Configure()
        {
            var configuration = new Configuration();

            //for nhibernate configuration properties
            configuration.SetProperties(pDriverSettings);

            //for nhibernate configuration file
            //var configurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hibernate.cfg.xml");
            //var configurationPath = HttpContext.Current.Server.MapPath(@"~\hibernate.cfg.xml");
            //configuration.Configure(configurationPath);

            // for entity config files
            //var employeeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Employee.hbm.xml");
            //configuration.AddFile(employeeConfigurationFile);

            var serializer = new HbmSerializer() { Validate = true };

            Type[] arr_typ = pAssembly.GetExportedTypes();//pAssembly.GetTypes();

            foreach (var item in arr_typ)
            {
                if (item.IsClass &&
                    item.GetInterfaces().Contains(typeof(IEntity)) &&
                    item.IsAbstract == false &&
                    typeof(IEntity).IsAssignableFrom(item)
                    )
                {
                    using (var stream = serializer.Serialize(item))
                    {
                        configuration.AddInputStream(stream, item.Name);
                    }
                }
            }

            //using (var stream = serializer.Serialize(typeof(ServiceDeviceData)))
            //{
            //    configuration.AddInputStream(stream, typeof(ServiceDeviceData).Name);
            //}

            sessionFactory = configuration.BuildSessionFactory();
        }

        public ISessionFactory SessionFactory { get { return sessionFactory; } }
    }
}