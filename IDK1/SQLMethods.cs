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
            // Provided Dummy Data with death dates
            var dummyData = new List<string[]> {
                new[] {"M1", "2000-02-01", "Unknown", "Unknown", "Male", "2008-04-01"},
                new[] {"M2", "2000-04-01", "Unknown", "Unknown", "Male", "2008-04-01"},
                new[] {"F3", "1999-06-01", "Unknown", "Unknown", "Female", "2018-01-05"},
                new[] { "F4", "1999/01/08", "Unknown", "Unknown", "Female", "2012/05/13" },
                new[] {"F5", "2003-01-01", "F3", "M2", "Female", "2012-10-08"},
                new[] { "M6", "2003/01/01", "F3", "M2", "Male", "2007/01/03" },
                new[] { "M7", "2003/01/01", "F3", "M2", "Male", "2014/03/16" },
                new[] { "M8", "2003/01/01", "F3", "M2", "Male", "2007/01/03" },
                new[] {"M9", "2004-12-01", "F4", "M1", "Male", "2007-09-15"},
                new[] {"M10", "2004-12-01", "F4", "M1", "Male", "2007-09-15"},
                new[] { "F11", "2004/01/06", "F3", "M2", "Female", "2016/06/13" },
                new[] { "F12", "2004/01/06", "F3", "M2", "Female", "2012/06/09" },
                new[] { "F13", "2004/01/06", "F3", "M2", "Female", "2021/01/13" },
                new[] {"M14", "2004-06-01", "F3", "M2", "Male", "2014-05-23"},
                new[] {"M15", "2006-06-01", "F5", "M1", "Male", "2010-02-17"},
                new[] { "M16", "2006/01/06", "F5", "M1", "Male", "2013/06/16" },
                new[] {"F17", "2006-06-01", "F5", "M1", "Female", "2012-10-08"},
                new[] {"M18", "2008-01-01", "F4", "M2", "Male", "2010-05-24"},
                new[] {"M19", "2008-01-01", "F4", "M2", "Male", "2010-05-24"},
                new[] { "F20", "2008/01/01", "F4", "M2", "Female", "" },
                new[] {"F21", "2008-01-01", "F4", "M2", "Female", "2017-10-29"},
                new[] {"M23", "2008-01-01", "F11", "Unknown", "Male", "2012-10-08"},
                new[] {"F24", "2008-01-01", "F13", "Unknown", "Female", "2013-11-02"},
                new[] { "M25", "2008/01/01", "F12", "M14", "Male", "2017/02/07" },
                new[] { "M26", "2008/01/01", "F12", "M14", "Male", "2016/12/02" },
                new[] { "F27", "2008/01/01", "F12", "M14", "Female", "2011/10/01" },
                new[] { "F28", "2008/01/01", "F12", "M14", "Female", "2011/10/01" },
                new[] {"M29", "2008-01-01", "F13", "Unknown", "Male", "2012-10-08"},
                new[] {"M30", "2008-01-01", "F11", "Unknown", "Male", "2013-01-25"},
                new[] {"F31", "2008-01-01", "F11", "Unknown", "Female", "2013-10-23"},
                new[] {"F32", "2008-01-01", "F11", "Unknown", "Female", "2013-10-23"},
                new[] { "M33", "2008/12/01", "F4", "M1", "Male", "2012/09/04" },
                new[] {"F34", "2008-12-01", "F4", "M1", "Female", "2015-07-23"},
                new[] { "F35", "2008/12/01", "F4", "M1", "Female", "2015/10/13" },
                new[] { "F36", "2009/01/01", "F21", "M7", "Female", "2017/07/22" },
                new[] {"M37", "2009-01-01", "F21", "M7", "Male", "2014-05-16"},
                new[] { "F38", "2009/01/01", "F21", "M7", "Female", "" },
                new[] {"F39", "2009-01-01", "F21", "M7", "Female", "2017-07-22"},
                new[] { "F40", "2008/12/01", "F4", "M1", "Female", "2015/12/31" },
                new[] {"M41", "2009-02-01", "F5", "M7", "Male", "2011-06-26"},
                new[] {"M42", "2009-02-01", "F5", "M7", "Male", "2011-06-26"},
                new[] {"M43", "2010-01-01", "F5", "M7", "Male", "2014-05-17"},
                new[] {"M44", "2010-01-01", "F5", "M7", "Male", "2012-09-12"},
                new[] {"F45", "2010-01-01", "F5", "M7", "Female", "2012-07-26"},
                new[] {"F46", "2010-01-01", "F5", "M7", "Female", "2012-07-26"},
                new[] {"F47", "2010-01-01", "F5", "M7", "Female", "2012-10-08"},
                new[] {"M48", "2010-06-01", "F5", "M7", "Male", "2014-01-21"},
                new[] {"M49", "2010-06-01", "F5", "M7", "Male", "2014-01-21"},
                new[] {"M50", "2010-06-01", "F13", "M16", "Male", "2014-01-28"},
                new[] { "F51", "2010/01/01", "F5", "M7", "Female", "2016/06/13" },
                new[] { "F52", "2010/01/01", "F5", "M7", "Female", "2016/06/13" },
                new[] {"M53", "2010-06-01", "F20", "M7", "Male", "2014-05-16"},
                new[] {"F54", "2010-06-01", "F20", "M7", "Female", "2011-12-13"},
                new[] {"F55", "2010-06-01", "F20", "M7", "Female", "2011-12-13"},
                new[] {"F56", "2010-06-01", "F20", "M7", "Female", "2011-12-13"},
                new[] {"F57", "2010-06-01", "F20", "M7", "Female", "2011-12-13"},
                new[] { "M92", "2014/01/05", "F39", "M14/M25/M26", "Male", "2015/01/05" },
                new[] { "M93", "2014/01/05", "F39", "M14/M25/M26", "Male", "2017/07/22" },
                new[] { "F94", "2014/01/05", "F39", "M14/M25/M26", "Female", "2016/10/25" },
                new[] { "M105", "2014/01/06", "F34", "M25/M26", "Male", "2015/07/23" },
                new[] { "M106", "2014/01/06", "F34", "M25/M26", "Male", "2015/07/23" },
                new[] { "M107", "2014/01/06", "F34", "M25/M26", "Male", "2015/07/23" }
            };

            foreach (var entry in dummyData) {
                string entityID = entry[0];
                DateTime birth = DateTime.Parse(entry[1]);
                string motherID = entry[2];
                string[] fatherIDs = entry[3].Split('/');
                string sex = entry[4];
                DateTime? death = string.IsNullOrWhiteSpace(entry[5]) ? (DateTime?)null : DateTime.Parse(entry[5]);

                int sexId;
                using (SQLiteCommand sqlite_cmd = new SQLiteCommand($"SELECT ID FROM Sex WHERE Sex = '{sex}'", sqlite_conn)) {
                    sexId = Convert.ToInt32(sqlite_cmd.ExecuteScalar());
                }

                int colorId;
                using (SQLiteCommand sqlite_cmd = new SQLiteCommand($"SELECT ID FROM Color ORDER BY RANDOM() LIMIT 1", sqlite_conn)) {
                    colorId = Convert.ToInt32(sqlite_cmd.ExecuteScalar());
                }

                using (SQLiteCommand sqlite_cmd = new SQLiteCommand($"INSERT INTO Entity (ID, Birth, Sex, Color) VALUES ('{entityID}', '{birth.ToString("yyyy-MM-dd")}', {sexId}, {colorId})", sqlite_conn)) {
                    sqlite_cmd.ExecuteNonQuery();
                }

                if (motherID != "Unknown") {
                    using (SQLiteCommand sqlite_cmd = new SQLiteCommand($"INSERT INTO Parent (ChildID, ParentID) VALUES ('{entityID}', '{motherID}')", sqlite_conn)) {
                        sqlite_cmd.ExecuteNonQuery();
                    }
                }

                foreach (string fatherID in fatherIDs) {
                    if (fatherID != "Unknown") {
                        using (SQLiteCommand sqlite_cmd = new SQLiteCommand($"INSERT INTO Parent (ChildID, ParentID) VALUES ('{entityID}', '{fatherID}')", sqlite_conn)) {
                            sqlite_cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Insert death date if it exists
                if (death != null) {
                    using (SQLiteCommand sqlite_cmd = new SQLiteCommand($"INSERT INTO Death (ID, Death) VALUES ('{entityID}', '{death.Value.ToString("yyyy-MM-dd")}')", sqlite_conn)) {
                        sqlite_cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }


}
