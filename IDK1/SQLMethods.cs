using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace IDK1;
internal class SQLMethods
{
    public static readonly string dbpath = @"PedigreeDB.db";
    public static SQLiteConnection CreateConnection()
    {
        SQLiteConnection sqlite_conn;
        // Create a new database connection:
        sqlite_conn = new SQLiteConnection("Data Source=" + dbpath + ";foreign keys=true;Version=3;New=True;Compress=True;");

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
    public static void GetSexData(SQLiteConnection conn) {

    }
    public static void GetColorData(SQLiteConnection conn) {

    }
}