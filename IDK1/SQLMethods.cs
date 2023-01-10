using System.Collections;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace IDK1;
internal class SQLMethods
{
    public static readonly string dbpath = @"PedigreeDB.db";
    public static SQLiteConnection CreateConnection() {
        // Create a new database connection:
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + dbpath + ";foreign keys=true;Version=3;New=True;Compress=True;");
        
        // Open the connection:
        try {
            sqlite_conn.Open();
        }
        catch (Exception ex) {
            // If an exception occurs while trying to open the connection, print the error message to the debugger
            Debug.WriteLine("Connection to database " + dbpath + " failed. \n" + ex.ToString());
        }
        return sqlite_conn;
    }
    public static ArrayList GetSexData() {
        // Create a connection to the SQLite database
        SQLiteConnection sqlite_conn = CreateConnection();

        // Declare a SQLiteDataReader object and an ArrayList
        SQLiteDataReader sqlite_datareader;
        ArrayList sexData = new ArrayList();

        // Create a command to select all rows from the "Sex" table
        SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
        sqlite_cmd.CommandText = "SELECT * FROM Sex";

        // Execute the command and store the resulting data
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        // Loop through each row in the data reader
        while (sqlite_datareader.Read()) {
            // Get the value in the second column of the current row and add the value to the sexData ArrayList
            sexData.Add(sqlite_datareader.GetString(1));
        }

        // Close the database connection
        sqlite_conn.Close();

        // Return the ArrayList
        return sexData;
    }

    public static ArrayList GetColorData() {
        // Create a connection to the SQLite database
        SQLiteConnection sqlite_conn = CreateConnection();

        // Declare a SQLiteDataReader object and an ArrayList
        SQLiteDataReader sqlite_datareader;
        ArrayList colorData = new ArrayList();

        // Create a command to select all rows from the "Sex" table
        SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
        sqlite_cmd.CommandText = "SELECT * FROM Color";

        // Execute the command and store the resulting data
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        // Loop through each row in the data reader
        while (sqlite_datareader.Read()) {
            // Get the value in the second column of the current row and add the value to the sexData ArrayList
            colorData.Add(sqlite_datareader.GetString(1));
        }

        // Close the database connection
        sqlite_conn.Close();

        // Return the ArrayList
        return colorData;
    }

    public static void InsertEntityData(string ID, string Date, int Sex, int Color) {
        // Create a connection to the SQLite database
        SQLiteConnection sqlite_conn = CreateConnection();
        SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

        // Insert data into Entity
        sqlite_cmd.CommandText = "INSERT INTO Entity (ID, Birth, Sex, Color) VALUES ('"+ ID +"','"+ Date +"','"+ Sex +"','"+ Color +"');";
        sqlite_cmd.ExecuteNonQuery();

    }

    public static void InsertDeathData(string ID, string Date) {
        // Create a connection to the SQLite database
        SQLiteConnection sqlite_conn = CreateConnection();
        SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

        // Insert data into Entity
        sqlite_cmd.CommandText = "INSERT INTO Death (ID, Death) VALUES ('" + ID + "'),('" + Date + "');";
        sqlite_cmd.ExecuteNonQuery();

    }

    public static void InsertParentData(string CID, string PID) {
        // Create a connection to the SQLite database
        SQLiteConnection sqlite_conn = CreateConnection();
        SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

        // Insert data into Entity
        sqlite_cmd.CommandText = "INSERT INTO Parent (ChildID, ParentID) VALUES ('" + CID + "'),('" + PID + "');";
        sqlite_cmd.ExecuteNonQuery();

    }

    public static ArrayList GetDatabase() {
        return null;
        // Create a connection to the SQLite database
        SQLiteConnection sqlite_conn = CreateConnection();

        // Declare a SQLiteDataReader object and an ArrayList
        SQLiteDataReader sqlite_datareader;
        ArrayList databaseData = new ArrayList();

        //
        SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
        sqlite_cmd.CommandText = "SELECT E.ID, E.Birth, M.ID, F.ID, S.Sex, E.Death, E.Color FROM Entity as E, Sex as S, Entity as M, Entity as F WHERE ...";

        // Execute the command and store the resulting data
        sqlite_datareader = sqlite_cmd.ExecuteReader();

        // Loop through each row in the data reader
        while (sqlite_datareader.Read())
        {
            //
            databaseData.Add(sqlite_datareader.GetString(1));
        }

        // Close the database connection
        sqlite_conn.Close();

        //return null;
    }
}