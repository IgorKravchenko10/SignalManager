using SignalManager.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalManager
{
    public static class Utils
    {
        public static string GetPathToDatabase(string databaseName)
        {
            string fullPath;
            if (!String.IsNullOrEmpty(Settings.Default.PathToDatabase))
            {
                fullPath = Settings.Default.PathToDatabase;
            }
            else
            {
                string pathToDatabase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SignalManager");
                Directory.CreateDirectory(pathToDatabase);
                fullPath = Path.Combine(pathToDatabase, databaseName);
            }
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("Database file not found", databaseName);
            }
            return fullPath;
        }        
    }
}
