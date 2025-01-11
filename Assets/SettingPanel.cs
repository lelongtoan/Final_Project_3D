using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{

    public FirebaseAuth auth;
    public bool isLogin;
    [SerializeField] GameObject login;
    [SerializeField] GameObject account;
    [SerializeField] GameObject res;
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    private void Start()
    {
        login.SetActive(false);
        account.SetActive(false);
        res.SetActive(false);
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
    public void SetPanelAccount()
    {
        if (!isLogin)
        {
            login.SetActive(true);
        }
        else
        {
            account.SetActive(true);
        }
    }
}
