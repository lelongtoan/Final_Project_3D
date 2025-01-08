using Firebase;
using UnityEngine;

public class FirebaseTest : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log("Firebase đã được cấu hình thành công!");
            }
            else
            {
                Debug.LogError($"Firebase không hoạt động: {task.Result}");
            }
        });
    }
}
