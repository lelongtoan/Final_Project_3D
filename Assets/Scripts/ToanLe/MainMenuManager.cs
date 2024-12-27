using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    [Header("Infor")]
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI diamondText;

    [Header("Panel")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject characterMenu;
    [SerializeField] GameObject perkMenu;
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject chestMenu;
    [SerializeField] GameObject achievementMenu;
    [SerializeField] GameObject settingMenu;
    [SerializeField] GameObject loginMenu;
    [SerializeField] GameObject languageMenu;
    [SerializeField] GameObject startGameMenu;
    [SerializeField] GameObject newCharMenu;
    [SerializeField] GameObject checkInMenu;
    [SerializeField] GameObject statsTreeMenu;
    [Header("Detail")]
    [SerializeField] GameObject skillNodeDetail;

    public SoundEffect sound;
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        moneyText.text = MainMenuInstance.instance.inforMenu.money.ToString();
        diamondText.text = MainMenuInstance.instance.inforMenu.diamond.ToString();
    }
    public void SetStatsTreeMenu()
    {
        sound.PlaySound("Button");
        statsTreeMenu.SetActive(!statsTreeMenu.activeInHierarchy);
    }
    public void SetCheckInMenu()
    {
        sound.PlaySound("Button");
        checkInMenu.SetActive(!checkInMenu.activeInHierarchy);
    }
    public void SetSkillNodeDetail()
    {
        sound.PlaySound("Button");
        skillNodeDetail.SetActive(!skillNodeDetail.activeInHierarchy);
    }
    public void SetMainMenu()
    {
        sound.PlaySound("Button");
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
    }
    public void SetCharMenu()
    {
        sound.PlaySound("Button");
        characterMenu.SetActive(!characterMenu.activeInHierarchy);
    }
    public void SetPerkMenu(bool equip)
    {
        sound.PlaySound("Button");
        if (!equip)
        {
            ShowListPerk.instance.DisplayUnlockedPerks(-1,false);
        }
        perkMenu.SetActive(!perkMenu.activeInHierarchy);
    }
    public void SetShopMenu()
    {
        sound.PlaySound("Button");
        shopMenu.SetActive(!shopMenu.activeInHierarchy);
    }
    public void SetChestMenu()
    {
        sound.PlaySound("Button");
        chestMenu.SetActive(!chestMenu.activeInHierarchy);
    }
    public void SetAchievementMenu()
    {
        sound.PlaySound("Button");
        achievementMenu.SetActive(!achievementMenu.activeInHierarchy);
    }
    public void SetSettingMenu()
    {
        sound.PlaySound("Button");
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
            sound.PlaySound("Button");
            loginMenu.SetActive(!loginMenu.activeInHierarchy);
        }
    }
    public void SetLanguageMenu()
    {
        sound.PlaySound("Button");
        languageMenu.SetActive(!languageMenu.activeInHierarchy);
    }
    public void SetStartMenu()
    {
        SaveInGame.instance.LoadPanelChar();
        sound.PlaySound("Button");
        startGameMenu.SetActive(!startGameMenu.activeInHierarchy);
    }
    public void SetNewCharMenu()
    {
        sound.PlaySound("Button");
        newCharMenu.SetActive(!newCharMenu.activeInHierarchy);
    }
}
