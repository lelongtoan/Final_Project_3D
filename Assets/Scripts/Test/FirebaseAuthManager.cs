using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseAuth auth;

    [Header("UI Elements")]
    public TextMeshProUGUI nameTxt;
    public InputField emailInput;
    public InputField passwordInput;
    public Button seeOnButton;
    public Button seeOffButton;
    public Button primaryButton;
    public Button secondaryButton;
    public TextMeshProUGUI statusText;

    private bool isLoginMode = true;

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
        if (auth.CurrentUser != null)
        {
            gameObject.SetActive(false);
        }
    }

    private void InitializeUI()
    {
        // Setup button listeners
        primaryButton.onClick.AddListener(HandlePrimaryAction);
        secondaryButton.onClick.AddListener(ToggleMode);

        // Setup password visibility buttons
        seeOnButton.onClick.AddListener(() => SetPasswordVisibility(true));
        seeOffButton.onClick.AddListener(() => SetPasswordVisibility(false));

        // Set default states
        SetPasswordVisibility(false);
        UpdateUIForMode();

        statusText.text = string.Empty;
    }

    private void UpdateUIForMode()
    {
        nameTxt.text = isLoginMode ? "Đăng Nhập" : "Đăng Ký";
        primaryButton.GetComponentInChildren<Text>().text = isLoginMode ? "Đăng Nhập" : "Đăng Ký";
        secondaryButton.GetComponentInChildren<Text>().text = isLoginMode ? "Đăng Ký" : "Đăng Nhập";
    }

    private void HandlePrimaryAction()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            statusText.text = "Vui lòng nhập email và mật khẩu.";
            return;
        }

        if (isLoginMode)
        {
            Login(email, password);
        }
        else
        {
            CreateAccount(email, password);
        }
    }

    private void ToggleMode()
    {
        isLoginMode = !isLoginMode;
        UpdateUIForMode();
    }

    private void Login(string email, string password)
    {
        statusText.text = "Đang đăng nhập...";

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Login failed: " + task.Exception?.Message);
                statusText.text = "Đăng nhập thất bại. Vui lòng thử lại!";
                return;
            }

            FirebaseUser user = task.Result.User;
            Debug.Log("Login Success! User ID: " + user.UserId);
            statusText.text = "Đăng nhập thành công!";
        });
    }

    private void CreateAccount(string email, string password)
    {
        statusText.text = "Đang tạo tài khoản...";

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Account creation failed: " + task.Exception?.Message);
                statusText.text = "Tạo tài khoản thất bại. Vui lòng thử lại!";
                return;
            }

            FirebaseUser user = task.Result.User;
            Debug.Log("Account Created! User ID: " + user.UserId);
            statusText.text = "Tạo tài khoản thành công!";
            ToggleMode();
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
