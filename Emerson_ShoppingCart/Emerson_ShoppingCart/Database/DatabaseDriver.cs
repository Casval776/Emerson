using System;
using System.Collections;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using Emerson_ShoppingCart.Database.Data;

namespace Emerson_ShoppingCart.Database
{
    public static class DatabaseDriver
    {
        private static readonly string DbPath = $@"{AppDomain.CurrentDomain.BaseDirectory}testdb.sqlite";
        public static SQLiteConnection DbConn;
        
        public static void ValidateDBStatus()
        {
            //Validate whether Sqlite file exists
            if (File.Exists(DbPath))
            {
                DbConn = new SQLiteConnection($@"Data Source={DbPath};Version=3;");
                return;
            }

            //Generate db file and begin setting up default data
            SQLiteConnection.CreateFile(DbPath);

            //Assign public variable
            DbConn = new SQLiteConnection($@"Data Source={DbPath};Version=3;");
            
            //Generate Table structure
            GenerateStructure();
            
            //Seed DB with test data
            SeedData();
        }

        private static void GenerateStructure()
        {
            //Obtain types
            var tableTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.Namespace == "Emerson_ShoppingCart.Models").Select(t => t);

            //Begin iterating through types
            foreach (var type in tableTypes)
            {
                //Create general format of create statement
                var createStatement = $@"CREATE TABLE {type.Name} ( {
                        string.Join(", ", type.GetProperties().Select(prop => prop.PropertyType == typeof(string)
                                ? $"{prop.Name} VARCHAR(500)"
                                : prop.PropertyType == typeof(int)
                                    ? $"{prop.Name} INT"
                                    : $"{prop.Name} REAL")
                            .ToArray())
                    } )";

                using (var cmd = DbConn.CreateCommand())
                {
                    DbConn.Open();

                    cmd.CommandText = createStatement;
                    cmd.ExecuteNonQuery();

                    DbConn.Close();
                }
            }
        }

        private static void SeedData()
        {
            SeedData_DoWork(Seed.ItemSeed);
            SeedData_DoWork(Seed.LinkTableSeed);
            SeedData_DoWork(Seed.UserSeed);
            SeedData_DoWork(Seed.DiscountTableSeed);
        }

        private static void SeedData_DoWork(IEnumerable items)
        {
            foreach (var item in items)
            {
                using (var cmd = DbConn.CreateCommand())
                {
                    DbConn.Open();

                    var insertStatement =
                        $@"INSERT INTO {item.GetType().Name} ( {
                                string.Join(", ", item.GetType().GetProperties().Select(t => t.Name).ToArray())
                            } ) VALUES ({
                                string.Join(", ", item.GetType()
                                    .GetProperties()
                                    .Select(prop => prop.PropertyType == typeof(string)
                                        ? $@"'{prop.GetValue(item)}'"
                                        : $"{prop.GetValue(item)}")
                                    .ToList())
                            })";

                    cmd.CommandText = insertStatement;
                    cmd.ExecuteNonQuery();

                    DbConn.Close();
                }
            }
        }
    }
}