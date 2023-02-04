using System.Collections;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text;

namespace IDK1;
internal class SQLMethods {
    public static readonly string dbpath = @"PedigreeDB.db";
    public static SQLiteConnection CreateConnection() {
        // Create a new database connection:
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + dbpath + ";foreign keys=true;Version=3;New=True;Compress=True;");

        // Open the connection:
        try {
            sqlite_conn.Open();
            sqlite_conn.BusyTimeout = 5000;
        }
        catch (Exception ex) {
            // If an exception occurs while trying to open the connection, print the error message to the debugger
            Debug.WriteLine("Connection to database " + dbpath + " failed. \n" + ex.ToString());
        }
        return sqlite_conn;
    }

    public static ArrayList GetData(string tableName) {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            // Declare a SQLiteDataReader object and an ArrayList
            SQLiteDataReader sqlite_datareader;
            ArrayList data = new ArrayList();

            // Create a command to select all rows from the specified table
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "SELECT * FROM " + tableName;

                // Execute the command and store the resulting data
                using (sqlite_datareader = sqlite_cmd.ExecuteReader()) {
                    // Loop through each row in the data reader
                    while (sqlite_datareader.Read()) {
                        // Get the value in the second column of the current row and add the value to the data ArrayList
                        data.Add(sqlite_datareader.GetString(1));
                    }

                    // Return the ArrayList
                    return data;
                }
            }
        }
    }

    public static ArrayList GetSexData() {
        return GetData("Sex");
    }

    public static ArrayList GetColorData() {
        return GetData("Color");
    }

    public static void InsertData(string tableName, Dictionary<string, object> data) {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                // Prepare command text and parameters
                StringBuilder commandText = new StringBuilder();
                commandText.Append("INSERT INTO " + tableName + " (");
                commandText.Append(string.Join(", ", data.Keys));
                commandText.Append(") VALUES (");
                commandText.Append(string.Join(", ", data.Keys.Select(key => "@" + key)));
                commandText.Append(");");
                sqlite_cmd.CommandText = commandText.ToString();

                // Add parameters to command
                foreach (var item in data) {
                    sqlite_cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                // Execute command
                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }

    public static void InsertEntityData(string ID, string Date, int Sex, int Color) {
        var data = new Dictionary<string, object> {
            { "ID", ID },
            { "Birth", Date },
            { "Sex", Sex },
            { "Color", Color }
        };
        InsertData("Entity", data);
    }

    public static void InsertDeathData(string ID, string Date) {
        var data = new Dictionary<string, object> {
            { "ID", ID },
            { "Death", Date }
        };
        InsertData("Death", data);
    }

    public static void InsertParentData(string CID, string PID) {
        var data = new Dictionary<string, object> {
            { "ChildID", CID },
            { "ParentID", PID }
        };
        InsertData("Parent", data);
    }

    public static object GetDataFromEntity(string ID, string column, bool isInt = false) {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            // Declare a SQLiteDataReader object
            SQLiteDataReader sqlite_datareader;

            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "SELECT " + column + " FROM Entity WHERE ID = @ID";
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);

                // Execute the command and store the resulting data
                using (sqlite_datareader = sqlite_cmd.ExecuteReader()) {
                    if (sqlite_datareader.Read()) {
                        if (isInt) {
                            int data = sqlite_datareader.GetInt32(0);
                            return data - 1;
                        }
                        else {
                            return sqlite_datareader.GetString(0);
                        }
                    }
                    else {
                        return null;
                    }
                }
            }

        }
    }

    public static object GetDataFromTable(string tableName, string column, string ID) {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection()) {

            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "SELECT " + column + " FROM " + tableName + " WHERE ID = @ID";
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);

                // Execute the command and store the resulting data
                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader()) {
                    if (sqlite_datareader.Read()) {
                        return sqlite_datareader.GetString(0);
                    }
                    else {
                        return null;
                    }
                }


            }

        }
    }

    // Checks if it actually exists or not.
    public static string? GetIDFromEntity(string ID) {
        return (string?)GetDataFromEntity(ID, "ID");
    }

    public static int GetSexFromEntity(string ID) {
        return (int)GetDataFromEntity(ID, "Sex", true);
    }

    public static string GetBirthFromEntity(string ID) {
        return (string)GetDataFromEntity(ID, "Birth");
    }

    public static string GetDeathFromEntity(string ID) {
        return (string)GetDataFromTable("Death", "Death", ID);
    }

    public static int GetColorFromEntity(string ID) {
        return (int)GetDataFromEntity(ID, "Color", true);
    }

    public static string GetMotherFromEntity(string ID) {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "SELECT Parent.ParentID FROM Parent, Entity WHERE Parent.ChildID = @ID AND Parent.ParentID = Entity.ID AND Entity.Sex = 3";
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);

                // Execute the command and store the resulting data
                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader()) {
                    if (sqlite_datareader.Read()) {
                        string mother = sqlite_datareader.GetString(0);
                        sqlite_conn.Close();
                        return mother;
                    }
                    else {
                        return null;
                    }
                }

            }

        }
    }

    public static ArrayList GetFatherFromEntity(string ID) {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            ArrayList fatherData = new ArrayList();

            //
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "SELECT Parent.ParentID FROM Parent, Entity WHERE Parent.ChildID = @ID AND Parent.ParentID = Entity.ID AND Entity.Sex = 2";
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);

                // Execute the command and store the resulting data
                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader()) {
                    // Loop through each row in the data reader
                    while (sqlite_datareader.Read()) {
                        // Get the value in the second column of the current row and add the value to the sexData ArrayList
                        fatherData.Add(sqlite_datareader.GetString(0));
                    }

                    return fatherData;
                }
            }

        }


    }

    public static void UpdateBirth(string ID, string Date) {
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "UPDATE Entity SET Birth = @Date WHERE ID = @ID";
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);
                sqlite_cmd.Parameters.AddWithValue("@Date", Date);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateSex(string ID, int Sex) {
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "UPDATE Entity SET Sex = @Sex WHERE ID = @ID";
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);
                sqlite_cmd.Parameters.AddWithValue("@Sex", Sex);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateColor(string ID, int Color) {
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "UPDATE Entity SET Color = @Color WHERE ID = @ID";
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);
                sqlite_cmd.Parameters.AddWithValue("@Color", Color);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateDeath(string ID, string Date) {
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "UPDATE Death SET Death = @Date WHERE ID = @ID";
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);
                sqlite_cmd.Parameters.AddWithValue("@Date", Date);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateParent(string CID, string PID, string OPID) {
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "UPDATE Parent SET ParentID = @PID WHERE ChildID = @CID AND ParentID = @OPID";
                sqlite_cmd.Parameters.AddWithValue("@CID", CID);
                sqlite_cmd.Parameters.AddWithValue("@PID", PID);
                sqlite_cmd.Parameters.AddWithValue("@OPID", OPID);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }

    public static void DeleteDeath(string ID) {
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "DELETE FROM Death WHERE ID = @ID";
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }

    public static void DeleteParent(string CID, string PID) {
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "DELETE FROM Parent WHERE ChildID = @CID AND ParentID = @PID";
                sqlite_cmd.Parameters.AddWithValue("@CID", CID);
                sqlite_cmd.Parameters.AddWithValue("@PID", PID);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }

    public static void DeleteAllParents(string CID) {
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "DELETE FROM Parent WHERE ChildID = @CID";
                sqlite_cmd.Parameters.AddWithValue("@CID", CID);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }

    public static void DeleteEntity(string ID) {
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                string sql = "DELETE FROM Death WHERE ID = @ID";
                string sql1 = "DELETE FROM Parent WHERE ChildID = @ID";
                string sql2 = "DELETE FROM Entity WHERE ID = @ID";
                sqlite_cmd.CommandText = sql;
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.CommandText = sql1;
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.CommandText = sql2;
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);
                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }

    public static async Task<string> FUCK() {
        await Task.Delay(10000);
        Debug.WriteLine("test");
        return "test";
    }

    // Make IsParent return ArrayList of children

    public static ArrayList GetDatabase() {
        return null;
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            // Declare a SQLiteDataReader object and an ArrayList
            SQLiteDataReader sqlite_datareader;
            ArrayList databaseData = new ArrayList();

            //
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT E.ID, E.Birth, M.ID, F.ID, S.Sex, E.Death, E.Color FROM Entity as E, Sex as S, Entity as M, Entity as F WHERE ...";

            // Execute the command and store the resulting data
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            // Loop through each row in the data reader
            while (sqlite_datareader.Read()) {
                //
                databaseData.Add(sqlite_datareader.GetString(1));
            }

            //return null;
        }
    }
}
