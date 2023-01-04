using System.Data.SQLite;
using System.Diagnostics;

namespace IDK1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        private static string path = @"PedigreeIDKv1.db";
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

            
            if (!File.Exists(path)) {
                //Debug.WriteLine("Feil");
                SQLiteConnection sqlite_conn;
                sqlite_conn = CreateConnection();
                CreateTable(sqlite_conn);
            }
        }

        static SQLiteConnection CreateConnection() {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source="+path+";foreign keys=true;Version=3;New=True;Compress=True;");

            // Open the connection:
            try {
                sqlite_conn.Open();
            } 
            catch (Exception ex) {
                Debug.WriteLine("Connection to database " + path + " failed. \n" + ex.ToString());
            }
            return sqlite_conn;
        }

        static void CreateTable(SQLiteConnection conn) {
            SQLiteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE IF NOT EXISTS Color(ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Color VARCHAR(24) NOT NULL)";
            string Createsql1 = "CREATE TABLE IF NOT EXISTS Sex(ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Sex VARCHAR(10) NOT NULL)";
            string Createsql2 = "CREATE TABLE IF NOT EXISTS Entity(ID VARCHAR(10) NOT NULL, Birth DATE NOT NULL, Sex INTEGER NOT NULL, Death DATE, Color INTEGER NOT NULL, PRIMARY KEY(ID), FOREIGN KEY (Color) REFERENCES Color(ID), FOREIGN KEY (Sex) REFERENCES Sex(ID))";
            string Createsql3 = "CREATE TABLE IF NOT EXISTS Parent(ChildID VARCHAR(10) NOT NULL, ParentID VARCHAR(10), Known INTEGER NOT NULL, PRIMARY KEY(ChildID, ParentID), FOREIGN KEY (ChildID) REFERENCES Entity(ID), FOREIGN KEY (ParentID) REFERENCES Entity(ID))";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Createsql1;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Createsql2;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Createsql3;
            sqlite_cmd.ExecuteNonQuery();
        }
    }
}