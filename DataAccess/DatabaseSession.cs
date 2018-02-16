using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SimpleNHibernate
{
    public class DatabaseSession
    {

        // internal database connection session
        //NHibernate.ISession _dbSession;
        NHibernate.IStatelessSession _dbSession;

        public DatabaseSession()
        {
            initDB(); 
        }

        public List<Bodies> GetAllBodies()
        {
            // criteria are your queries, you can just give them a type and it'll get all of them
            var criteria = _dbSession.CreateCriteria(typeof(Bodies));
            return criteria.List<Bodies>().ToList();
        }

        public Bodies GetOneBody(int Id)
        {
            var criteria = _dbSession.CreateCriteria(typeof(Bodies));
            // or you can add restrictions to make a where clause
            criteria.Add(Restrictions.Eq("id", Id));
            return criteria.UniqueResult<Bodies>();
        }

        public void SaveABody(Bodies newBody)
        {
            // writing to the database is as simple as this...
            //var transaction = _dbSession.BeginTransaction();
            //_dbSession.Merge(newBody);
            //transaction.Commit();
        }

        public void SaveManyTestObjs(List<Bodies> newObjs)
        {
            var transaction = _dbSession.BeginTransaction();
            foreach (Bodies o in newObjs)
            {
                //_dbSession.Merge(o);
                _dbSession.Insert(o);
            }
            transaction.Commit();
        }


        private void initDB() // TODO: Check if DB exists. If it exists, truncate tables and move to filling DB
        {
            // you can have a path here if you don't want the db file in the program directory
            string dbFile = "bodies.db";

            // this bit works out the database schema to go with all the mapped classes
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetAssembly(typeof(Bodies)).GetExportedTypes());
            var mappings = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var config = new Configuration();
            config.DataBaseIntegration(db =>
            {
                db.Dialect<SQLiteDialect>();
                db.Driver<SQLite20Driver>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
                db.ConnectionString = string.Format("Data Source={0};Version=3;", dbFile);
            });
            config.SetProperty(Environment.DefaultSchema, "EDSM");
            config.AddDeserializedMapping(mappings, "test");
            
            // Create the database from the mapped schema if it doesn't exist
            if (!File.Exists(dbFile))
            {
                System.Console.WriteLine("No existing DB-file found. Creating new one.");
                new SchemaExport(config).Create(true, true);
            }

            // Connect to the database and open a connection we can use to read/write
            SchemaMetadataUpdater.QuoteTableAndColumns(config);

            var sessionFactory = config.BuildSessionFactory();

            //_dbSession = sessionFactory.OpenSession();
            _dbSession = sessionFactory.OpenStatelessSession();
            
            // TruncateBodies();
        }

        private void TruncateBodies()
        {
            var transaction = _dbSession.BeginTransaction();
            try
            {
                System.Console.WriteLine("Truncating tables.");
                _dbSession.CreateSQLQuery("DELETE FROM EDSM_Materials").ExecuteUpdate();
                _dbSession.CreateSQLQuery("DELETE FROM EDSM_Rings").ExecuteUpdate();
                _dbSession.CreateSQLQuery("DELETE FROM EDSM_Bodies").ExecuteUpdate();
                transaction.Commit();
                System.Console.WriteLine("Tables truncated.");
            }
            //catch (Exception e) // TODO
            //{
            //    transaction.Rollback();
            //}
            finally
            {
                transaction.Dispose();
            }
        }

    }
}