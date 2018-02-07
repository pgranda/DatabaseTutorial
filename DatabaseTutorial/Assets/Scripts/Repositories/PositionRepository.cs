using System;
using System.Data;
using UnityEngine;

public class PositionRepository
{
    private DatabaseManager databaseManager;

    public PositionRepository()
    {
        databaseManager = DatabaseManager.Instance;
    }

    public Vector3 GetPlayerPosition(int playerID)
    {
        string query = string.Format(
            "SELECT X, Y, Z FROM POSITION WHERE PLAYERID = {0}", playerID);

        float xPosition = -11;
        float yPosition = -4.67f;
        float zPosition = 10.74f;

        DataTable position = databaseManager.ExecuteQuery(query);

        foreach (DataRow row in position.Rows)
        {
            xPosition = Convert.ToSingle(row["X"]);
            yPosition = Convert.ToSingle(row["Y"]);
            zPosition = Convert.ToSingle(row["Z"]);
        }
        return new Vector3(xPosition, yPosition, zPosition);
    }

    public Quaternion GetPlayerRotation(int playerID)
    {
        string query = string.Format(
            "SELECT X, Y, Z FROM ROTATION WHERE PLAYERID = {0}", playerID);

        float xRotation = 0;
        float yRotation = 0;
        float zRotation = 0;

        DataTable rotation = databaseManager.ExecuteQuery(query);

        foreach (DataRow row in rotation.Rows)
        {
            xRotation = Convert.ToSingle(row["X"]);
            yRotation = Convert.ToSingle(row["Y"]);
            zRotation = Convert.ToSingle(row["Z"]);
        }

        return Quaternion.Euler(xRotation, yRotation, zRotation);
    }

    public void SavePlayerPositionAndRotation(Vector3 position, Vector3 rotation, int playerID)
    {
        string positionQuery = string.Format(
            "INSERT INTO POSITION (X, Y, Z, PLAYERID) SELECT {0}, {1}, {2}, {3} WHERE NOT EXISTS (SELECT 1 FROM POSITION WHERE PLAYERID = {3}); UPDATE POSITION SET X = {0}, Y = {1}, Z = {2}, PLAYERID = {3} WHERE EXISTS (SELECT 1 FROM POSITION WHERE PLAYERID = {3});",
            position.x, position.y, position.z, playerID);

        string rotationQuery = string.Format(
            "INSERT INTO ROTATION (X, Y, Z, PLAYERID) SELECT {0}, {1}, {2}, {3} WHERE NOT EXISTS (SELECT 1 FROM ROTATION WHERE PLAYERID = {3}); UPDATE ROTATION SET X = {0}, Y = {1}, Z = {2}, PLAYERID = {3} WHERE EXISTS (SELECT 1 FROM ROTATION WHERE PLAYERID = {3});",
            rotation.x, rotation.y, rotation.z, playerID);

        string playerQuery = string.Format(
            "UPDATE PLAYER SET AUTOSAVETIMESTAMP = (SELECT DATETIME(CURRENT_TIMESTAMP, 'LOCALTIME')) WHERE PLAYERID = {0};",
            playerID);

        string query = string.Format(
            "{0} {1} {2}", positionQuery, rotationQuery, playerQuery);

        databaseManager.ExecuteNonQuery(query);
    }
}
