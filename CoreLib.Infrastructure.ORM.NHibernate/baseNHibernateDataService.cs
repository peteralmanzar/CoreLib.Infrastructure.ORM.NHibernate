using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;

namespace CoreLib.Infrastructure.ORM.NHibernate
{
    public abstract class baseNHibernateDataService
    {
        #region Members
        protected ISessionFactory _sessionFactory;
        #endregion

        #region Constructors
        public baseNHibernateDataService(string server, string database, string username = "", string password = "")
        {
            #region Guards
            if (string.IsNullOrEmpty(server)) throw new ArgumentNullException(nameof(server));
            if (string.IsNullOrEmpty(database)) throw new ArgumentNullException(nameof(database));
            #endregion            

            buildSessionFactory(server, database, username, password);
            initRepositories();
        }
        #endregion

        #region Private Functions
        private void buildSessionFactory(string server, string database, string username = "", string password = "")
        {
            #region Guards
            if (string.IsNullOrEmpty(server)) throw new ArgumentNullException(nameof(server));
            if (string.IsNullOrEmpty(database)) throw new ArgumentNullException(nameof(database));
            #endregion   

            _sessionFactory = Fluently.Configure()
                .Database(databaseConnectionConfiguration(server, database, username, password))
                .Mappings(mappingConfiguration())
                .BuildSessionFactory();
        }

        protected abstract IPersistenceConfigurer databaseConnectionConfiguration(string server, string database, string username = "", string password = "");
        protected abstract Action<MappingConfiguration> mappingConfiguration();
        protected abstract void initRepositories();
        #endregion
    }
}
