using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseAuth auth;
    [Header("Login")]
    public Text nameTxt;
    public InputField emailInput;
    public InputField passwordInput;
    public Button seeOnButton;  // Nút để hiển thị mật khẩu
    public Button seeOffButton;
    public Button aBtn;  // Nút để hiển thị mật khẩu
    public Button bBtn; // Nút để ẩn mật khẩu
    public Text statusText; // Hiển thị trạng thái (tùy chọn)
    public bool isLogin;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    private void OnEnable()
    {
        aBtn.onClick.RemoveAllListeners();
        bBtn.onClick.RemoveAllListeners();
        aBtn.onClick.AddListener(Login);
        bBtn.onClick.AddListener(CreateAccount);
        aBtn.GetComponentInChildren<Text>().text = "Đăng Nhập";
        bBtn.GetComponentInChildren<Text>().text = "Đăng Ký";
        isLogin = true;
        statusText.text = " ";
        // Khởi tạo Firebase Authentication
        passwordInput.contentType = InputField.ContentType.Password;

        // Đăng ký sự kiện cho các nút
        seeOnButton.onClick.AddListener(ShowPassword);
        seeOffButton.onClick.AddListener(HidePassword);

        // Ẩn nút "seeOff" khi bắt đầu vì mật khẩu đang được ẩn
        seeOffButton.gameObject.SetActive(false);
    }

    // Hàm đăng nhập
    public void Login()
    {
        if(isLogin)
{
            string email = emailInput.text; // Lấy email từ InputField
            string password = passwordInput.text; // Lấy mật khẩu từ InputField

            statusText.text = "Đăng nhập thất bại. Vui lòng thử lại!";
            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    
                }
                else
                {
                    // Nếu không có lỗi
                    Firebase.Auth.AuthResult authResult = task.Result;
                    FirebaseUser user = authResult.User;

                }
            });
            Debug.Log($"Đăng nhập thành công! Chào {auth.CurrentUser.Email}");
            if (statusText != null)
                statusText.text = $"Chào mừng, {auth.CurrentUser.Email}!";
            gameObject.SetActive(false);
        }

        else
        {
            aBtn.onClick.RemoveAllListeners();
            bBtn.onClick.RemoveAllListeners();
            nameTxt.text = "Đăng Nhập";
            aBtn.onClick.AddListener(Login);
            bBtn.onClick.AddListener(CreateAccount);
            aBtn.GetComponentInChildren<Text>().text = "Đăng Nhập";
            bBtn.GetComponentInChildren<Text>().text = "Đăng Ký";
            isLogin = true;
        }
    }

    public void CreateAccount()
    {
        if(!isLogin)
        {
            string email = emailInput.text; // Lấy email từ InputField
            string password = passwordInput.text; // Lấy mật khẩu từ InputField

            statusText.text = "Tạo tài khoản thất bại. Vui lòng thử lại!";
            auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    return;
                }
                else
                {
                    Firebase.Auth.AuthResult authResult = task.Result;
                    FirebaseUser user = authResult.User;
                }
            });
            if (statusText != null)
                statusText.text = $"Tạo tài khoản thành công!";
        }
        else
        {
            aBtn.onClick.RemoveAllListeners();
            bBtn.onClick.RemoveAllListeners();
            nameTxt.text = "Đăng Ký";
            bBtn.onClick.AddListener(Login);
            aBtn.onClick.AddListener(CreateAccount);
            bBtn.GetComponentInChildren<Text>().text = "Đăng Nhập";
            aBtn.GetComponentInChildren<Text>().text = "Đăng Ký";
            isLogin = false;
        }
    }

    // Hiển thị mật khẩu
    public void ShowPassword()
    {
        passwordInput.contentType = InputField.ContentType.Standard; // Hiển thị mật khẩu
        passwordInput.ActivateInputField(); // Cập nhật UI ngay lập tức

        // Ẩn nút "seeOn" và hiển thị nút "seeOff"
        seeOnButton.gameObject.SetActive(false);
        seeOffButton.gameObject.SetActive(true);
    }

    // Ẩn mật khẩu
    public void HidePassword()
    {
        passwordInput.contentType = InputField.ContentType.Password; // Ẩn mật khẩu
        passwordInput.ActivateInputField(); // Cập nhật UI ngay lập tức

        // Ẩn nút "seeOff" và hiển thị nút "seeOn"
        seeOnButton.gameObject.SetActive(true);
        seeOffButton.gameObject.SetActive(false);
    }
}
