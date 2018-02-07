using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text PlayerName;

    void Start()
    {
        PlayerName.text = LoginManager.Instance.LoggedPlayerName;
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerManager.Instance.SavePlayerPositionAndRotation();
            Application.Quit();
        }
	}
}
