using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseRegistration : MonoBehaviour
{
    private FirebaseAuth auth;

    [Header("UI Elements")]
    public InputField emailInput;
    public InputField passwordInput;
    public TextMeshProUGUI statusText;

    bool isRes = true; 
    private void Start()
    {
        InitializeUI();
        auth = FirebaseAuth.DefaultInstance;
    }
    private void Update()
    {
        if(isRes == false)
        {
            statusText.text = "Tạo tài khoản thất bại. Vui lòng thử lại!";
        }
    }
    private void InitializeUI()
    {
        SetPasswordVisibility(false);
        statusText.text = string.Empty;
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

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                isRes = false;
                Debug.LogError("Account creation failed: " + task.Exception?.Message);
                return;
            }

            FirebaseUser user = task.Result.User;
            Debug.Log("Account Created! User ID: " + user.UserId);
            statusText.text = "Tạo tài khoản thành công!";
        });
    }

    public void SetPasswordVisibility(bool visible)
    {
        passwordInput.contentType = visible ? InputField.ContentType.Standard : InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate();
    }
}
