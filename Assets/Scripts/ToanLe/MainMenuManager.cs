using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject characterMenu;
    [SerializeField] GameObject perkMenu;
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject chestMenu;
    [SerializeField] GameObject detailChest;
    [SerializeField] GameObject achievementMenu;
    [SerializeField] GameObject settingMenu;
    [SerializeField] GameObject loginMenu;
    [SerializeField] GameObject languageMenu;
    [SerializeField] GameObject startGameMenu;
    [SerializeField] GameObject newCharMenu;
    [Header("Detail")]
    [SerializeField] GameObject skillNodeDetail;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        
    }
    public void SetSkillNodeDetail()
    {
        skillNodeDetail.SetActive(!skillNodeDetail.activeInHierarchy);
    }
    public void SetMainMenu()
    {
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
    }
    public void SetCharMenu()
    {
        characterMenu.SetActive(!characterMenu.activeInHierarchy);
    }
    public void SetPerkMenu()
    {
        perkMenu.SetActive(!perkMenu.activeInHierarchy);
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
    public void SetLanguageMenu()
    {
        languageMenu.SetActive(!languageMenu.activeInHierarchy);
    }
    public void SetStartMenu()
    {
        startGameMenu.SetActive(!startGameMenu.activeInHierarchy);
    }
    public void SetNewCharMenu()
    {
        newCharMenu.SetActive(!newCharMenu.activeInHierarchy);
    }
}
