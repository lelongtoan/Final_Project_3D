using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{

    public FirebaseAuth auth;
    public bool isLogin;
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    private void Update()
    {
        if (auth.CurrentUser == null)
        {
            isLogin = false;
        }
        else
        {
            isLogin = true;
        }
    }
}
