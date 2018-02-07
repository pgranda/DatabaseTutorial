using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public GameObject Player;

    private PositionRepository positionRepository;

    void Awake()
    {
        Instance = this;

        positionRepository = new PositionRepository();
        LoadAndSetPosition(LoginManager.Instance.LoggedPlayerID);
        LoadAndSetRotation(LoginManager.Instance.LoggedPlayerID);
    }

    private void LoadAndSetPosition(int playerID)
    {
        Player.transform.position = positionRepository.GetPlayerPosition(playerID);
    }

    private void LoadAndSetRotation(int playerID)
    {
        Player.transform.rotation = positionRepository.GetPlayerRotation(playerID);
    }

    public void SavePlayerPositionAndRotation()
    {
        var position = Player.transform.position;
        var rotation = Player.transform.rotation.eulerAngles;

        positionRepository.SavePlayerPositionAndRotation(position, rotation, LoginManager.Instance.LoggedPlayerID);
    }



    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
