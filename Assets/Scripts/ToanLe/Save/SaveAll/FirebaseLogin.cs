using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseLogin : MonoBehaviour
{
    private FirebaseAuth auth;

    [Header("UI Elements")]
    public InputField emailInput;
    public InputField passwordInput;
    public Button seeOnButton;
    public Button seeOffButton;
    public Button loginButton;
    public TextMeshProUGUI statusText;
    bool isLog = true;
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    private void Update()
    {
        if (auth.CurrentUser != null)
        {
            gameObject.SetActive(false);
        }
        if (isLog == false)
        {
            statusText.text = "Đăng nhập thất bại. Vui lòng thử lại!";
        }
    }
    private void Start()
    {
        InitializeUI();
    }
    private void InitializeUI()
    {
        loginButton.onClick.AddListener(HandleLogin);

        seeOnButton.onClick.AddListener(() => SetPasswordVisibility(true));
        seeOffButton.onClick.AddListener(() => SetPasswordVisibility(false));

        SetPasswordVisibility(false);
        statusText.text = string.Empty;
    }

    private void HandleLogin()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            statusText.text = "Vui lòng nhập email và mật khẩu.";
            return;
        }
        else if (string.IsNullOrEmpty(email))
        {
            statusText.text = "Vui lòng nhập email.";
        }
        else if(string.IsNullOrEmpty(password))
        {
            statusText.text = "Vui lòng nhập mật khẩu.";
        }
        Login(email, password);
    }

    private void Login(string email, string password)
    {
        statusText.text = "Đang đăng nhập...";

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                isLog = false;
                Debug.LogError("Login failed: " + task.Exception?.Message);
                return;
            }

            FirebaseUser user = task.Result.User;
            Debug.Log("Login Success! User ID: " + user.UserId);
            statusText.text = "Đăng nhập thành công!";
        });
    }

    private void SetPasswordVisibility(bool visible)
    {
        passwordInput.contentType = visible ? InputField.ContentType.Standard : InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate();

        seeOnButton.gameObject.SetActive(!visible);
        seeOffButton.gameObject.SetActive(visible);
    }
}
