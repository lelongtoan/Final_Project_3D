﻿using Firebase.Auth;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    [Header("Infor")]
    public InforMainMenu inforMenu;
    [Header("Panel")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject characterMenu;
    [SerializeField] GameObject perkMenu;
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject chestMenu;
    [SerializeField] GameObject achievementMenu;
    [SerializeField] GameObject settingMenu;
    [SerializeField] GameObject loginMenu;
    [SerializeField] GameObject resMenu;
    [SerializeField] GameObject accountMenu;
    [SerializeField] GameObject startGameMenu;
    [SerializeField] GameObject newCharMenu;
    [SerializeField] GameObject checkInMenu;
    [SerializeField] GameObject statsTreeMenu;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject creditPanel;
    [Header("Detail")]
    [SerializeField] GameObject skillNodeDetail;
    public SoundEffect sound;

    [SerializeField] SettingPanel settingPanel;
    private void Awake()
    {
        Instance = this; CloseAll();
    }
    private void Start()
    {
        CloseAll(); 
    }
    public void CloseAll()
    {
        //mainMenu.SetActive(false);
        characterMenu.SetActive(false);
        perkMenu.SetActive(false);
        shopMenu.SetActive(false);
        chestMenu.SetActive(false);
        achievementMenu.SetActive(false);
        settingMenu.SetActive(false);
        loginMenu.SetActive(false);
        startGameMenu.SetActive(false);
        newCharMenu.SetActive(false);
        checkInMenu.SetActive(false);
        statsTreeMenu.SetActive(false);
        skillNodeDetail.SetActive(false);
        tutorialPanel.SetActive(false);
    }
    public void SetCreditPanel()
    {
        creditPanel.SetActive(!creditPanel.activeInHierarchy);
    }
    public void SetTutorialPanel()
    {
        tutorialPanel.SetActive(!tutorialPanel.activeInHierarchy);
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
    public void SetPerkMenu(int i = 1)
    {
        sound.PlaySound("Button");
        if (i != 1) 
            ShowListPerk.instance.DisplayUnlockedPerks(-1,false);
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
    public void SetAccount()
    {
        sound.PlaySound("Button");
        accountMenu.SetActive(!accountMenu.activeInHierarchy);
    }
    public void SetLoginMenu()
    {
        sound.PlaySound("Button");
        loginMenu.SetActive(!loginMenu.activeInHierarchy);
    }
    
    public void SetResMenu()
    {
        sound.PlaySound("Button");
        resMenu.SetActive(!resMenu.activeInHierarchy);
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
