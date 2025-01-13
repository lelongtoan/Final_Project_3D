using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine;
using Unity.VisualScripting;

public class SaveLoadData : MonoBehaviour
{
    public static SaveLoadData instance;
    private DatabaseReference reference;
    private FirebaseAuth auth;
    [SerializeField] SaveAllTemp temp;
    private void Awake()
    {
        instance = this;
        auth = FirebaseAuth.DefaultInstance;
    }
    void Start()
    {
        // Khởi tạo Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance; // Khởi tạo Firebase Auth
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    // Lưu dữ liệu lên Firebase
    public void SaveData()
    {
        if (auth.CurrentUser != null)
        {
            string email = auth.CurrentUser.Email; // Lấy email người dùng
            string safeEmail = email.Replace("@", "_").Replace(".", "_"); // Firebase không cho phép dấu @ và . trong tên đường dẫn, thay bằng dấu _
            string json = JsonUtility.ToJson(temp.saveAllData); // Chuyển đổi dữ liệu thành JSON

            // Lưu dữ liệu vào Firebase theo email của người dùng
            reference.Child("users").Child(safeEmail).Child("saveData").SetRawJsonValueAsync(json)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted)
                    {
                        temp.statusText.text = "Đã Lưu Dữ Liệu";
                    }
                    else
                    {
                        temp.statusText.text = "Dữ Liệu Chưa Được Lưu";
                        return;
                    }
                });

                for (int i = 0; i < 3; i++)
                {
                
                    SaveData save = new SaveData();
                    save = SaveLoadJson.LoadFromJson(i);

                    if (save != null)
                    {
                        string js = JsonUtility.ToJson(save);
                        reference.Child("users").Child(safeEmail).Child("Char" + i).SetRawJsonValueAsync(js)
                        .ContinueWithOnMainThread(task =>
                        {
                            if (task.IsCompleted)
                            {
                                Debug.Log($"Dữ liệu Char{i} đã được lưu thành công!");
                            }
                            else
                            {
                                temp.statusText.text = "Dữ Liệu Chưa Được Lưu";
                                return;
                            }
                        });
                    }
                    else
                    {
                        // Nếu dữ liệu null, xóa mục tương ứng trên Firebase
                        reference.Child("users").Child(safeEmail).Child("Char" + i).RemoveValueAsync()
                        .ContinueWithOnMainThread(task =>
                        {
                            if (task.IsCompleted)
                            {
                                Debug.Log($"Char{i} đã được xóa khỏi Firebase do không có dữ liệu.");
                            }
                            else
                            {
                                Debug.LogError($"Không thể xóa Char{i}: " + task.Exception);
                            }
                        });
                    }
                }
        }
        else
        {
            Debug.LogError("Người dùng chưa đăng nhập!");
        }
    }

    // Tải dữ liệu từ Firebase
    public void LoadData()
    {
        if (auth.CurrentUser != null)
        {
            string email = auth.CurrentUser.Email; // Lấy email người dùng
            string safeEmail = email.Replace("@", "_").Replace(".", "_"); // Firebase không cho phép dấu @ và . trong tên đường dẫn, thay bằng dấu _

            // Tải dữ liệu chính
            reference.Child("users").Child(safeEmail).Child("saveData").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.Exists && !string.IsNullOrEmpty(snapshot.GetRawJsonValue()))
                    {
                        string json = snapshot.GetRawJsonValue();
                        Debug.Log($"Dữ liệu saveData: {json}");

                        SaveAllData loadedData = JsonUtility.FromJson<SaveAllData>(json);
                        if (loadedData == null)
                        {
                            Debug.LogError("Lỗi khi tải dữ liệu SaveAllData!");
                        }
                        else
                        {
                            temp.statusText.text = "Đã Tải Dữ Liệu";
                            temp.saveAllData = loadedData;
                            temp.LoadMenu(temp.saveAllData); // Cập nhật dữ liệu hiện tại với dữ liệu đã tải
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Không có dữ liệu chính trong Firebase hoặc dữ liệu rỗng.");
                    }
                }
                else
                {
                    temp.statusText.text = "Dữ Liệu Chưa Được Tải";
                    return;
                }
            });

            for (int i = 0; i < 3; i++)
            {
                int charIndex = i; // Đảm bảo giá trị index không bị thay đổi trong lambda
                reference.Child("users").Child(safeEmail).Child("Char" + charIndex).GetValueAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        if (snapshot.Exists && !string.IsNullOrEmpty(snapshot.GetRawJsonValue()))
                        {
                            string json = snapshot.GetRawJsonValue();
                            Debug.Log($"Dữ liệu Char{charIndex}: {json}");

                            SaveData loadedCharacterData = JsonUtility.FromJson<SaveData>(json);
                            if (loadedCharacterData != null)
                            {
                                Debug.Log($"Dữ liệu nhân vật {charIndex} đã được tải thành công!");
                                SaveLoadJson.SaveToJson(loadedCharacterData, charIndex); // Lưu dữ liệu tải về vào JSON nếu cần
                            }
                            else
                            {
                                Debug.LogWarning($"Dữ liệu nhân vật {charIndex} bị null hoặc không hợp lệ.");
                            }
                        }
                        else
                        {
                            Debug.LogWarning($"Không có dữ liệu nhân vật {charIndex} hoặc dữ liệu rỗng.");
                        }
                    }
                    else
                    {
                        Debug.LogError($"Không thể tải dữ liệu Char{charIndex}: {task.Exception}");
                    }
                });
            }
        }
        else
        {
            temp.statusText.text = "Người dùng chưa đăng nhập!";
            return;
        }
    }


}
