using System;

public class LoginRepository
{
    private DatabaseManager databaseManager;

    public LoginRepository()
    {
        databaseManager = DatabaseManager.Instance;
    }

    public bool CheckIfUserAlreadyExists(string username, string email)
    {
        string userExistsQuery =
            string.Format("SELECT 1 FROM USER WHERE (USERNAME = '{0}' OR EMAIL = '{1}') AND VALID = 1", username,
                email);

        var result = databaseManager.ExecuteQuery(userExistsQuery);

        if (result.Rows.Count > 0)
        {
            return true;
        }
        return false;
    }

    public void SaveUserAndCreatePlayer(string username, string email, string passwordHash)
    {
        string userQuery = string.Format(
            "INSERT INTO USER (USERNAME, EMAIL, PASSWORD, CREATEDTIMESTAMP, VALID) SELECT '{0}', '{1}', '{2}', (SELECT DATETIME(CURRENT_TIMESTAMP, 'LOCALTIME')), 1;",
            username, email, passwordHash);

        string playerQuery = string.Format(
            "INSERT INTO PLAYER (USERID, HP, EXP, AUTOSAVETIMESTAMP, VALID) SELECT (SELECT LAST_INSERT_ROWID()), 100, 0, (SELECT DATETIME(CURRENT_TIMESTAMP, 'LOCALTIME')),1;");

        string query = string.Format("{0} {1}", userQuery, playerQuery);

        databaseManager.ExecuteNonQuery(query);
    }

    public int AuthorizeUser(string username, string passwordHash)
    {
        string userExistsQuery = string.Format(
            "SELECT P.PLAYERID FROM USER U JOIN PLAYER P ON U.USERID = P.PLAYERID WHERE (USERNAME = '{0}' AND PASSWORD = '{1}') AND U.VALID = 1 AND P.VALID = 1",
            username, passwordHash);

        var result = databaseManager.ExecuteQuery(userExistsQuery);

        if (result.Rows.Count == 0)
        {
            return 0;
        }

        int playerID = Convert.ToInt32((result.Rows[0])["PlayerID"]);
        return playerID;
    }
}
