using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine;

public class FirebaseInitializer : MonoBehaviour
{
    private FirebaseApp firebaseApp;

    void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                firebaseApp = FirebaseApp.DefaultInstance;
                Debug.Log("Firebase is ready to use.");
            }
            else
            {
                Debug.LogError("Firebase initialization failed.");
            }
        });
    }
}
