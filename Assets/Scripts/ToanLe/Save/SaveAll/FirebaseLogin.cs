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
    public TextMeshProUGUI statusText;
    bool isLog = true;

    
    private void Start()
    {
        InitializeUI();
        auth = FirebaseAuth.DefaultInstance;
    }
    private void Update()
    {
        if (auth.CurrentUser != null)
        {
            ReportMain.instance.SetReport("Đăng nhập thành công!");
            gameObject.SetActive(false);
        }
        if (isLog == false)
        {
            statusText.text = "Đăng nhập thất bại. Vui lòng thử lại!";
        }
    }
    private void InitializeUI()
    {
        SetPasswordVisibility(false);
        statusText.text = "";
        emailInput.text = "";
        passwordInput.text = "";
    }

    public void HandleLogin()
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

    public void Login(string email, string password)
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
        });
    }

    public void SetPasswordVisibility(bool visible)
    {
        passwordInput.contentType = visible ? InputField.ContentType.Standard : InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate();
    }
}
