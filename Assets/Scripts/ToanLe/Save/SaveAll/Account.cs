﻿using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

public class Account : MonoBehaviour
{
    private FirebaseAuth auth;
    public Text accounTxt;
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    private void OnEnable()
    {
        if (auth.CurrentUser != null)
        {
            accounTxt.text = auth.CurrentUser.Email;
        }
        else
        {
            accounTxt.text = "Chưa đăng nhập";
        }
    }
    private void Update()
    {
        if (auth.CurrentUser != null)
        {
            accounTxt.text = auth.CurrentUser.Email;
        }
        else
        {
            accounTxt.text = "Chưa đăng nhập";
        }
    }
    public void Logout()
    {
        auth.SignOut();  // Đăng xuất người dùng
        ReportMain.instance.SetReport("Đã đăng xuất thành công!");
    }
}
