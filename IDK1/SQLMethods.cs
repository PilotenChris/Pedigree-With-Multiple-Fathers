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
                        // Get the value in the first column of the current row and add the value to the fatherData ArrayList
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

    public static void DeleteDeath(string ID) {
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "DELETE FROM Death WHERE ID = @ID";
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);
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

    public static void DeleteParentChildLink(string CID, string PID) {
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

    public static void DeleteAllChildrenLinks(string CID) {
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "DELETE FROM Parent WHERE ParentID = @CID";
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
    public static ArrayList GetChildren(string ID) {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            ArrayList childData = new ArrayList();

            // x
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "SELECT Parent.ChildID FROM Parent, Entity WHERE Parent.parentID = @ID";
                sqlite_cmd.Parameters.AddWithValue("@ID", ID);

                // Execute the command and store the resulting data
                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader()) {
                    while (sqlite_datareader.Read()) {
                        // Get the value in the first column of the current row and add the value to the childData ArrayList
                        childData.Add(sqlite_datareader.GetString(0));
                    }

                    return childData;
                }
            }
        }
    }

    public static ArrayList GetParents(string ID) {
        // Create a connection to the SQLite database
        ArrayList parentData = GetFatherFromEntity(ID); 
        parentData.Add(GetMotherFromEntity(ID));
        return parentData;
    }

    public static async Task<ArrayList> GetDatabase() {
        ArrayList databaseData = new ArrayList() { await GetEntityDatabase(), await GetDeathDatabase(), await GetParentDatabase() };
        return databaseData;
    }

    public static async Task<List<(string, string, string, string)>> GetEntityDatabase() {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            // Declare a SQLiteDataReader object and an ArrayList
            List<(string, string, string, string)> databaseEntityData = new List<(string, string, string, string)>();

            //
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "SELECT Entity.ID, Entity.Birth, Sex.Sex, Color.Color FROM Entity, Sex, Color WHERE Entity.Sex = Sex.ID AND Entity.Color = Color.ID";

                // Execute the command and store the resulting data
                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader()) {
                    // Loop through each row in the data reader
                    while (sqlite_datareader.Read()) {
                        // Get the value in the first column of the current row and add the value to the fatherData ArrayList
                        databaseEntityData.Add((sqlite_datareader.GetString(0), sqlite_datareader.GetString(1), sqlite_datareader.GetString(2), sqlite_datareader.GetString(3)));
                    }

                    return databaseEntityData;
                }
            }
        }
    }

    public static async Task<List<(string, string)>> GetDeathDatabase() {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            // Declare a SQLiteDataReader object and an ArrayList
            List<(string, string)> databaseDeathData = new List<(string, string)>();

            //
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "SELECT * FROM Death";

                // Execute the command and store the resulting data
                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader()) {
                    // Loop through each row in the data reader
                    while (sqlite_datareader.Read()) {
                        // Get the value in the first column of the current row and add the value to the fatherData ArrayList
                        databaseDeathData.Add((sqlite_datareader.GetString(0), sqlite_datareader.GetString(1)));
                    }

                    return databaseDeathData;
                }
            }
        }
    }

    public static async Task<List<(string, string)>> GetParentDatabase() {
        // Create a connection to the SQLite database
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            // Declare a SQLiteDataReader object and an ArrayList
            List<(string, string)> databaseParentData = new List<(string, string)>();

            //
            using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand()) {
                sqlite_cmd.CommandText = "SELECT * FROM Parent";

                // Execute the command and store the resulting data
                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader()) {
                    // Loop through each row in the data reader
                    while (sqlite_datareader.Read()) {
                        // Get the value in the first column of the current row and add the value to the fatherData ArrayList
                        databaseParentData.Add((sqlite_datareader.GetString(0), sqlite_datareader.GetString(1)));
                    }

                    return databaseParentData;
                }
            }
        }
    }


    public static void InsertDummy() {
        using (SQLiteConnection sqlite_conn = CreateConnection()) {
            

            // Insert Colors
            string[] colors = { "Light Blue", "Light Yellow", "Light Green", "Light Grey", "Light Orange" };
            

            // Insert Sexes
            string[] sexes = { "Unknown", "Male", "Female" };
            

            // Insert Entities
            Random random = new Random();
            DateTime startDate = new DateTime(1999, 1, 1);
            DateTime endDate = DateTime.Today;

            for (int i = 1; i <= 100; i++) {
                int sexId = random.Next(1, 4);
                int colorId = random.Next(1, 6);
                string entityId = $"{sexes[sexId - 1][0]}{i}";

                DateTime birthDate = startDate.AddDays(random.Next((endDate - startDate).Days));
                string birthDateString = birthDate.ToString("yyyy/MM/dd");

                using (SQLiteCommand sqlite_cmd = new SQLiteCommand($"INSERT INTO Entity (ID, Birth, Sex, Color) VALUES ('{entityId}', '{birthDateString}', {sexId}, {colorId})", sqlite_conn)) {
                    sqlite_cmd.ExecuteNonQuery();
                }

                // Insert Death
                if (random.NextDouble() > 0.7) // 30% chance of having a death record
                {
                    DateTime deathDate = birthDate.AddDays(random.Next((endDate - birthDate).Days));
                    string deathDateString = deathDate.ToString("yyyy/MM/dd");

                    using (SQLiteCommand sqlite_cmd = new SQLiteCommand($"INSERT INTO Death (ID, Death) VALUES ('{entityId}', '{deathDateString}')", sqlite_conn)) {
                        sqlite_cmd.ExecuteNonQuery();
                    }
                }

                // Insert Parents
                if (random.NextDouble() > 0.3) // 50% chance of having parent records
                {
                    for (int j = 1; j <= 2; j++) {
                        string parentId = $"{sexes[random.Next(1, 4) - 1][0]}{random.Next(1, 11)}";
                        if (parentId != entityId) // Ensure the parent is not the same as the child
                        {
                            using (SQLiteCommand sqlite_cmd = new SQLiteCommand($"INSERT OR IGNORE INTO Parent (ChildID, ParentID) VALUES ('{entityId}', '{parentId}')", sqlite_conn)) {
                                sqlite_cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
    }


}
