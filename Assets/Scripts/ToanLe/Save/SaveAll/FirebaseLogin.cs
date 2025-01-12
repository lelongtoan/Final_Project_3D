using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseLogin : MonoBehaviour
{
    private FirebaseAuth auth;

    [Header("UI Elements")]
    public InputField emailInput;
    public InputField passwordInput;
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
        SetPasswordVisibility(false);
        statusText.text = string.Empty;
    }

    public void HandleLogin()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
        {
            statusText.text = "Vui lòng nhập email và mật khẩu.";
            return;
        }
        if (string.IsNullOrEmpty(email))
        {
            statusText.text = "Vui lòng nhập email.";
        }
        if(string.IsNullOrEmpty(password))
        {
            statusText.text = "Vui lòng nhập mật khẩu.";
        }
        Login(email, password);
    }

    public void Login(string email, string password)
    {
        statusText.text = "Đang đăng nhập...";

        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
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

    public void SetPasswordVisibility(bool visible)
    {
        passwordInput.contentType = visible ? InputField.ContentType.Standard : InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate();
    }
}
