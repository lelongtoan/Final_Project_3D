using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseRegistration : MonoBehaviour
{
    private FirebaseAuth auth;

    [Header("UI Elements")]
    public InputField emailInput;
    public InputField passwordInput;
    public Text statusText;

    bool isRes = true;
    bool isResed = false;
    private void Awake()
    {
        auth = auth = FirebaseAuth.DefaultInstance;
    }
    private void OnEnable()
    {
        InitializeUI();
    }
    private void Update()
    {
        if(isRes == false)
        {
            statusText.text = "Tạo tài khoản thất bại. Vui lòng thử lại!";
            isRes = true;
        }
        if(auth.CurrentUser != null)
        {
            ReportMain.instance.SetReport("Tạo tài khoản và đăng nhập thành công.");
            gameObject.SetActive(false);
            isResed = false;
        }
    }
    private void InitializeUI()
    {
        SetPasswordVisibility(false);
        statusText.text = string.Empty;
        emailInput.text = "";
        passwordInput.text = "";
    }

    public void HandleRegistration()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            statusText.text = "Vui lòng nhập email và mật khẩu.";
            return;
        }

        CreateAccount(email, password);
    }

    public void CreateAccount(string email, string password)
    {
        statusText.text = "Đang tạo tài khoản...";

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                isRes = false;
                Debug.LogError("Account creation failed:Dã có tk " + task.Exception?.Message);
                return;
            }
            FirebaseUser user = task.Result.User;
            isResed = true;
        });
    }

    public void SetPasswordVisibility(bool visible)
    {
        passwordInput.contentType = visible ? InputField.ContentType.Standard : InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate();
    }
}
