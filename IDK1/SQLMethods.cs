using System.Collections;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;

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

    //public static ArrayList GetData(string tableName)
    //{
    //    // Create a connection to the SQLite database
    //    using (SQLiteConnection sqlite_conn = CreateConnection())
    //    {
    //        // Declare a SQLiteDataReader object and an ArrayList
    //        SQLiteDataReader sqlite_datareader;
    //        ArrayList data = new ArrayList();

    //        // Create a command to select all rows from the specified table
    //        SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
    //        sqlite_cmd.CommandText = "SELECT * FROM " + tableName;

    //        // Execute the command and store the resulting data
    //        sqlite_datareader = sqlite_cmd.ExecuteReader();

    //        // Loop through each row in the data reader
    //        while (sqlite_datareader.Read())
    //        {
    //            // Get the value in the second column of the current row and add the value to the data ArrayList
    //            data.Add(sqlite_datareader.GetString(1));
    //        }

    //        // Return the ArrayList
    //        return data;
    //    }
    //}

    public static ArrayList GetData(string tableName)
    {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection())
        {
            // Set the busy timeout to 5 seconds
            sqlite_conn.Open();
            sqlite_conn.BusyTimeout = 5000;

            // Declare a SQLiteDataReader object and an ArrayList
            SQLiteDataReader sqlite_datareader;
            ArrayList data = new ArrayList();

            // Create a command to select all rows from the specified table
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM " + tableName;

            // Execute the command and store the resulting data
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            // Loop through each row in the data reader
            while (sqlite_datareader.Read())
            {
                // Get the value in the second column of the current row and add the value to the data ArrayList
                data.Add(sqlite_datareader.GetString(1));
            }

            // Return the ArrayList
            return data;
        }
    }

    public static ArrayList GetSexData()
    {
        return GetData("Sex");
    }

    public static ArrayList GetColorData()
    {
        return GetData("Color");
    }

    public static void InsertData(string tableName, Dictionary<string, object> data)
    {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection())
        {
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

            // Prepare command text and parameters
            StringBuilder commandText = new StringBuilder();
            commandText.Append("INSERT INTO " + tableName + " (");
            commandText.Append(string.Join(", ", data.Keys));
            commandText.Append(") VALUES (");
            commandText.Append(string.Join(", ", data.Keys.Select(key => "@" + key)));
            commandText.Append(");");
            sqlite_cmd.CommandText = commandText.ToString();

            // Add parameters to command
            foreach (var item in data)
            {
                sqlite_cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
            }

            // Execute command
            sqlite_cmd.ExecuteNonQuery();
        }
    }

    public static void InsertEntityData(string ID, string Date, int Sex, int Color)
    {
        var data = new Dictionary<string, object> {
        { "ID", ID },
        { "Birth", Date },
        { "Sex", Sex },
        { "Color", Color }
    };
        InsertData("Entities", data);
    }

    public static void InsertDeathData(string ID, string Date)
    {
        var data = new Dictionary<string, object> {
        { "ID", ID },
        { "Death", Date }
    };
        InsertData("Death", data);
    }

    public static void InsertParentData(string CID, string PID)
    {
        var data = new Dictionary<string, object> {
        { "ChildID", CID },
        { "ParentID", PID }
    };
        InsertData("Parent", data);
    }

    public static object GetDataFromEntity(string ID, string column)
    {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection())
        {
            // Declare a SQLiteDataReader object
            SQLiteDataReader sqlite_datareader;

            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT " + column + " FROM Entities WHERE ID = @ID";
            sqlite_cmd.Parameters.AddWithValue("@ID", ID);

            // Execute the command and store the resulting data
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            if (sqlite_datareader.Read())
            {
                if (column == "Sex" || column == "Color")
                {
                    int data = sqlite_datareader.GetInt32(0);
                    return data - 1;
                }
                else
                {
                    return sqlite_datareader.GetValue(0);
                }
            }
            else
            {
                return null;
            }
        }
    }

    public static object GetDataFromTable(string tableName, string column, string ID)
    {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection())
        {
            // Declare a SQLiteDataReader object
            SQLiteDataReader sqlite_datareader;

            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT " + column + " FROM " + tableName + " WHERE ID = @ID";
            sqlite_cmd.Parameters.AddWithValue("@ID", ID);

            // Execute the command and store the resulting data
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            if (sqlite_datareader.Read())
            {
                return sqlite_datareader.GetValue(0);
            }
            else
            {
                return null;
            }
        }
    }

    public static string? GetIDFromEntity(string ID)
    {
        return (string?)GetDataFromEntity(ID, "ID");
    }

    public static int GetSexFromEntity(string ID)
    {
        return (int)GetDataFromEntity(ID, "Sex");
    }

    public static string GetBirthFromEntity(string ID)
    {
        return (string)GetDataFromEntity(ID, "Birth");
    }

    public static string GetDeathFromEntity(string ID)
    {
        return (string)GetDataFromTable("Death", "Death", ID);
    }

    public static int GetColorFromEntity(string ID)
    {
        return (int)GetDataFromEntity(ID, "Color");
    }


    public static ArrayList GetDatabase() {
        return null;
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection())
        {
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

            //return null;
        }
    }
}