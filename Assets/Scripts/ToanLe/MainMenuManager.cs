using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject characterMenu;
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject chestMenu;
    [SerializeField] GameObject detailChest;
    [SerializeField] GameObject achievementMenu;
    [SerializeField] GameObject settingMenu;
    [SerializeField] GameObject loginMenu;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SetCharMenu()
    {
        characterMenu.SetActive(!characterMenu.activeInHierarchy);
    }
    public void SetShopMenu()
    {
        shopMenu.SetActive(!shopMenu.activeInHierarchy);
    }
    public void SetChestMenu()
    {
        chestMenu.SetActive(!chestMenu.activeInHierarchy);
    }
    public void SetChestDetailMenu(bool open)
    {
        if (open)
        {

        }
        else
        {

        }
        detailChest.SetActive(!detailChest.activeInHierarchy);
    }
    public void SetAchievementMenu()
    {
        achievementMenu.SetActive(!achievementMenu.activeInHierarchy);
    }
    public void SetSettingMenu()
    {
        settingMenu.SetActive(!settingMenu.activeInHierarchy);
    }
    public void SetLoginMenu()
    {
        bool checkLogin = false;//Check Login
        if(checkLogin)
        {

        }
        else
        {
            loginMenu.SetActive(!loginMenu.activeInHierarchy);
        }
    }
}
