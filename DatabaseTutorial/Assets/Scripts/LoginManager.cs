using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public static LoginManager Instance;

    public GameObject loginParent;
    public GameObject registerParent;

    public InputField Login_UsernameField;
    public InputField Login_PasswordField;
    public InputField Register_UsernameField;
    public InputField Register_PasswordField;
    public InputField Register_ConfirmPasswordField;
    public InputField Register_EmailField;

    public Text Login_ErrorText;
    public Text Register_ErrorText;

    public int LoggedPlayerID { get; private set; }
    public string LoggedPlayerName { get; private set; }
    private LoginRepository loginRepository;

    void Awake()
    {
        Instance = this;
        ResetAllUIElements();
        loginRepository = new LoginRepository();
    }

    void ResetAllUIElements()
    {
        Login_UsernameField.text = null;
        Login_PasswordField.text = null;
        Register_UsernameField.text = null;
        Register_PasswordField.text = null;
        Register_ConfirmPasswordField.text = null;
        Register_EmailField.text = null;
        Login_ErrorText.text = null;
        Register_ErrorText.text = null;
    }

    public void Login_RegisterButtonPressed()
    {
        ResetAllUIElements();
        loginParent.gameObject.SetActive(false);
        registerParent.gameObject.SetActive(true);
    }

    public void Register_BackButtonPressed()
    {
        ResetAllUIElements();
        loginParent.gameObject.SetActive(true);
        registerParent.gameObject.SetActive(false);
    }

    public void Login_LoginButtonPressed()
    {
        string username = Login_UsernameField.text;
        string password = Login_PasswordField.text;

        LoginUser();
    }

    public void Register_RegisterButtonPressed()
    {
        string username = Register_UsernameField.text;
        string password = Register_PasswordField.text;
        string confirmedPassword = Register_ConfirmPasswordField.text;
        string email = Register_EmailField.text;

        SaveUserAndCreatePlayer();
    }

    public void SaveUserAndCreatePlayer()
    {
        if (Register_PasswordField.text != Register_ConfirmPasswordField.text)
        {
            Register_ErrorText.text = "Passwords don't match.";
            return;
        }

        if (loginRepository.CheckIfUserAlreadyExists(Register_UsernameField.text, Register_EmailField.text))
        {
            Register_ErrorText.text = "User already exists.";
            return;
        }

        var passwordHash = HashHelper.GetHashString(Register_PasswordField.text);
        loginRepository.SaveUserAndCreatePlayer(Register_UsernameField.text, Register_EmailField.text, passwordHash);

        SceneManager.LoadScene(0);
    }

    public void LoginUser()
    {
        var passwordHash = HashHelper.GetHashString(Login_PasswordField.text);
        int playerID = loginRepository.AuthorizeUser(Login_UsernameField.text, passwordHash);

        if (playerID == 0)
        {
            Login_ErrorText.text = "Not correct username or password";
            return;
        }

        LoggedPlayerID = playerID;
        LoggedPlayerName = Login_UsernameField.text;

        SceneManager.LoadScene(1);
    }
}
