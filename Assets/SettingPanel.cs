using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{

    public FirebaseAuth auth;
    public bool isLogin;
    [SerializeField] GameObject login;
    [SerializeField] GameObject account;
    [SerializeField] GameObject res;
    [SerializeField] Button sound;
    [SerializeField] GameObject soundpanel;
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    private void Start()
    {
        soundpanel.SetActive(false);
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
    public void OpenSoundSetting()
    {
        soundpanel.SetActive(true);
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
