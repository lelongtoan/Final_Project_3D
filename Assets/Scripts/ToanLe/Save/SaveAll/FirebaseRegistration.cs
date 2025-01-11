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
    public Button seeOnButton;
    public Button seeOffButton;
    public Button registerButton;
    public TextMeshProUGUI statusText;

    bool isRes = true; 
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    private void Start()
    {
        InitializeUI();
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
        // Setup button listeners
        registerButton.onClick.AddListener(HandleRegistration);

        // Setup password visibility buttons
        seeOnButton.onClick.AddListener(() => SetPasswordVisibility(true));
        seeOffButton.onClick.AddListener(() => SetPasswordVisibility(false));

        // Set default password visibility
        SetPasswordVisibility(false);
        statusText.text = string.Empty;
    }

    private void HandleRegistration()
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

    private void CreateAccount(string email, string password)
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

    private void SetPasswordVisibility(bool visible)
    {
        passwordInput.contentType = visible ? InputField.ContentType.Standard : InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate();

        seeOnButton.gameObject.SetActive(!visible);
        seeOffButton.gameObject.SetActive(visible);
    }
}
