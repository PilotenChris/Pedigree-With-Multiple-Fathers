using System.Collections;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace IDK1;
internal class SQLMethods
{
    public static readonly string dbpath = @"PedigreeDB.db";
    public static SQLiteConnection CreateConnection()
    {
        // Create a new database connection:
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + dbpath + ";foreign keys=true;Version=3;New=True;Compress=True;");
        
        // Open the connection:
        try
        {
            sqlite_conn.Open();
        }
        catch (Exception ex)
        {
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
            // Get the value in the second column of the current row as a string
            string myreader = sqlite_datareader.GetString(1);

            // Add the value to the sexData ArrayList
            sexData.Add(myreader);
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
            // Get the value in the second column of the current row as a string
            string myreader = sqlite_datareader.GetString(1);

            // Add the value to the sexData ArrayList
            colorData.Add(myreader);
        }

        // Close the database connection
        sqlite_conn.Close();

        // Return the ArrayList
        return colorData;
    }
}