using JetEntityFrameworkProvider;
using SignalManager.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalManager.Context
{
    public class LocalContext : DbContext
    {
        public const string DatabaseName = "LocalBase.accdb";

        public DbSet<Point> Points { get; set; }

        public DbSet<PointList> PointLists { get; set; }

        public DbSet<Settings> Settings { get; set; }

        private static LocalContext _localContext;

        private LocalContext():this(String.Format("Provider=Microsoft.ACE.OleDb.12.0; Data Source={0};", Utils.GetPathToDatabase(DatabaseName)))
        {
        }

        private LocalContext(string connectionString):base(connectionString)
        {
            if (this.Database.Exists())
            {
                System.Data.Entity.Database.SetInitializer<LocalContext>(new MigrateDatabaseToLatestVersion<LocalContext, LocalMigrationsConfiguration>(true));
            }
            else
            {
                this.Database.Create();
            }
        }

        public static LocalContext Instance
        {
            get
            {
                if (_localContext == null)
                {
                    _localContext = this.Create();
                }
                return _localContext;
            }
        }

        private static LocalContext Create()
        {
            Database.DefaultConnectionFactory = new JetEntityFrameworkProvider.JetConnectionFactory();
            JetConnection.DUAL = JetConnection.DUALForAccdb;
            LocalContext localContext = new Context.LocalContext();
            localContext.Database.Initialize(true);
            return localContext;
        }
    }
}
