using Firebase;
using UnityEngine;

public class FirebaseInitializer : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log("Firebase đã sẵn sàng!");
            }
            else
            {
                Debug.LogError($"Không thể khởi tạo Firebase: {task.Result}");
            }
        });
    }
}
