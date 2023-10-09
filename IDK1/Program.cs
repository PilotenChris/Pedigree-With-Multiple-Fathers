using System.Collections;
using System.Data.SQLite;
using System.Diagnostics;

namespace IDK1 {
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (!File.Exists(SQLMethods.dbpath)) {
                // Creates the database if it doesn't exist
                SQLiteConnection sqlite_conn = SQLMethods.CreateConnection();
                CreateTable(sqlite_conn);
                InsertData(sqlite_conn);
                sqlite_conn.Close();
            }
            Application.Run(new Form1());
            //Debug.WriteLine(string.Join(",", (string[])SQLMethods.GetSexData().ToArray(typeof(string))));
            //TestDatabaseAsync();
        }

        static async Task TestDatabaseAsync() {
            ArrayList databaseData = await SQLMethods.GetDatabase();

            // Iterate through the ArrayLists in databaseData
            foreach (IList list in databaseData) {
                Debug.WriteLine(string.Join(",", list.Cast<object>().Select(x => x.ToString()).ToArray()));
            }

        }

        static void CreateTable(SQLiteConnection conn) {
            // Create a connection to the SQLite database
            SQLiteCommand sqlite_cmd = conn.CreateCommand();

            // Define the SQL statements to create four tables: "Color", "Sex", "Entity", and "Parent"
            string Sql = "CREATE TABLE IF NOT EXISTS Color(ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Color VARCHAR(24) NOT NULL)";
            string Sql1 = "CREATE TABLE IF NOT EXISTS Sex(ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Sex VARCHAR(10) NOT NULL)";
            string Sql2 = "CREATE TABLE IF NOT EXISTS Entity(ID VARCHAR(10) NOT NULL, Birth DATE NOT NULL, Sex INTEGER NOT NULL, Color INTEGER NOT NULL, PRIMARY KEY(ID), FOREIGN KEY (Color) REFERENCES Color(ID), FOREIGN KEY (Sex) REFERENCES Sex(ID))";
            string Sql3 = "CREATE TABLE IF NOT EXISTS Parent(ChildID VARCHAR(10) NOT NULL, ParentID VARCHAR(10), PRIMARY KEY(ChildID, ParentID), FOREIGN KEY (ChildID) REFERENCES Entity(ID), FOREIGN KEY (ParentID) REFERENCES Entity(ID))";
            string Sql4 = "CREATE TABLE IF NOT EXISTS Death(ID VARCHAR(10) NOT NULL, Death DATE NOT NULL, PRIMARY KEY(ID), FOREIGN KEY(ID) REFERENCES Entity(ID))";

            // Execute each of the SQL statements
            sqlite_cmd.CommandText = Sql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Sql1;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Sql2;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Sql3;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Sql4;
            sqlite_cmd.ExecuteNonQuery();
        }

        static void InsertData(SQLiteConnection conn) {
            // Create a connection to the SQLite database
            SQLiteCommand sqlite_cmd = conn.CreateCommand();

            // Insert three rows into the "Sex" table
            sqlite_cmd.CommandText = "INSERT INTO Sex (Sex) VALUES ('Unknown'),('Male'),('Female');";
            sqlite_cmd.ExecuteNonQuery();

            // Insert five rows into the "Color" table
            sqlite_cmd.CommandText = "INSERT INTO Color (Color) VALUES ('Green'),('Yellow'),('Blue'),('Grey'),('Red');";
            sqlite_cmd.ExecuteNonQuery();
        }
    }
    /*
     * (T[{vXD:8y+[]kP,DIt._=fQtA$t._:gP</JuOH?BDZ#}DEAD@>Ro"lOseWNq}'q'Fvtc;awR-y/t#W[m?BY!$Z*D>\J@S}]"#RImiMbp[kPyxOIayef;,DIi{ee@E')A*cp-Pg@uZDlHxc!</J¨gbS(qi{eMFTKiCrWyL=nIRxV@efmgf{p@(knBsVKdMbpMuEWpdK5hpbkA<cKiC/t#ef;W]IrqfwnST=N$X*A#FIV:Mbp*cp;miRBd{e$nUcvB@N#[$¨9VKd=TaS}]KDudXjRBQcjo#D?9:hIV:ps/{C;KwNDpVMFT"GvKzY\J@?(KcjoUwU
     * m%*s[]t<,')AmYh-D}nAh')AZ.[!Txrcvps/ZVAjujz=l(j<U_x/y>JA/"GvEL<\J@fBuMFTa¨OY¨Sr<@OLchAzIRx=\qHFq*tYIV#Hypmwq
     */
}
