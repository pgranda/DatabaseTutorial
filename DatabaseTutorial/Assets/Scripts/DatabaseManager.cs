using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class DatabaseManager
{
    private static DatabaseManager instance;
    private SqliteConnection connection;
    private static object syncRoot = new object();

    public static DatabaseManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new DatabaseManager();
                    }
                }
            }
            return instance;
        }
    }

    private DatabaseManager()
    {
        LoadDatabase("DatabaseTutorial.db");
    }

    public bool LoadDatabase(string databaseName)
    {
        string loadDb = "URI=file:" + Application.dataPath + "/StreamingAssets/" + databaseName;
        connection = new SqliteConnection(loadDb);

        if (connection != null)
        {
            return true;
        }

        Debug.LogError("Unable to access database.");
        return false;
    }

    public DataTable ExecuteQuery(string query)
    {
        if (connection == null)
        {
            return null;
        }

        SqliteDataAdapter dataAdapter;
        DataTable dataTable = new DataTable();

        try
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = query;

            dataAdapter = new SqliteDataAdapter(command);
            dataAdapter.Fill(dataTable);

            command.Dispose();
        }
        catch (SqliteException ex)
        {
            Debug.LogError(string.Format("Error occured when trying to execute query on database: {0}", ex.Message));
        }
        finally
        {
            connection.Close();
        }
        return dataTable;
    }

    public void ExecuteNonQuery(string query)
    {
        if (connection == null)
        {
            return;
        }

        try
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }
}
