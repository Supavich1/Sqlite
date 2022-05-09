using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlite_app
{
    internal class Customers
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename=Customers.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Customers (uid INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "first_Name NVARCHAR(50) NOT NULL, " +
                    "last_Name NVARCHAR(50) NULL," +
                    "email NVARCHAR(100) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }

        public static void AddData(string first_Name,string last_Name,string email)
        {
            using (SqliteConnection db =
              new SqliteConnection($"Filename=Customers.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                // Use parameterized query to prevent SQL injection attacks
                //@Entry หมายถึงผู้ใช้ไม่สามารถกรอก sql command เข้ามาได้
                //insertCommand.CommandText = "INSERT INTO MyTable VALUES (NULL, @Entry);";
                //insertCommand.Parameters.AddWithValue("@Entry", inputText);
                insertCommand.CommandText = "INSERT INTO Customers(first_Name,last_Name,email)" +
                                            "VALUES (@first_Name,@last_Name,@email);";
                insertCommand.Parameters.AddWithValue("@first_Name", first_Name);
                insertCommand.Parameters.AddWithValue("@last_Name", last_Name);
                insertCommand.Parameters.AddWithValue("@email", email);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }

        public static List<String> GetData()
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename=Customers.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT uid,first_Name,last_Name,email from Customers", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                    entries.Add(query.GetString(1));
                    entries.Add(query.GetString(2));
                    entries.Add(query.GetString(3));
                }
                db.Close();
            }
            return entries;
        }

        public static void alterData()
        {
            using (SqliteConnection db =
              new SqliteConnection($"Filename=Customers.db"))
            {
                db.Open();
                String alterCommand = "ALTER TABLE Customers Rename To Customers_old";

                SqliteCommand alterTable = new SqliteCommand(alterCommand,db);
                alterTable.ExecuteReader();
                db.Close();
            }
        }
        public static void dropData()
        {
            using (SqliteConnection db =
              new SqliteConnection($"Filename=Customers.db"))
            {
                db.Open();
                String dropCommand = "DROP TABLE Customers_old";

                SqliteCommand dropTable = new SqliteCommand(dropCommand, db);
                dropTable.ExecuteReader();
                db.Close();
            }
        }
    }
}
