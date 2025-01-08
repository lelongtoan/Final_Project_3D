    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SaveAllTemp : MonoBehaviour
{
    public SaveAllData saveAllData;
    [SerializeField] AchievementCheck checkAcie;
    [SerializeField] InforMainMenu inforMenu;
    [SerializeField] PerkEquipmentData equipPerk;
    [SerializeField] ListPerkData listPerkData;
    [SerializeField] ListSkillNode listSkillNode;
    [SerializeField] ListAchievement listAchievement;

    public void SaveMenu()
    {
        saveAllData._01_countBoss = checkAcie.countBoss;
        saveAllData._02_countEnemy = checkAcie.countEnemy;
        saveAllData._03_countGold = checkAcie.countGold;
        saveAllData._04_countSignIn = checkAcie.countSignIn;
        saveAllData._05_maxLevel = checkAcie.countLevel;
        saveAllData._06_money = inforMenu.money;
        saveAllData._07_diamond = inforMenu.diamond;
        saveAllData._08_ironKey = inforMenu.ironKey;
        saveAllData._09_silverKey = inforMenu.silverKey;
        for (int i = 0; i < equipPerk.equippedPerks.Count; i++)
        {
            saveAllData._10_perks[i]._01_id = i;
            saveAllData._10_perks[i]._02_idPerk = equipPerk.equippedPerks[i] != null ? equipPerk.equippedPerks[i].id : -1;
        }
        listPerkData.Set();
        for (int i = 0; i < listPerkData.listPerk.Count; i++)
        {
            saveAllData._11_listPerk[i]._01_id = i;
            saveAllData._11_listPerk[i]._02_idPerk = listPerkData.listPerk[i].id;
            saveAllData._11_listPerk[i]._03_statePerk = (int)listPerkData.listPerk[i].perkState;
            saveAllData._11_listPerk[i]._04_levelPerk = listPerkData.listPerk[i].levelPerk;
            saveAllData._11_listPerk[i]._05_quantityPerk = listPerkData.listPerk[i].quantity;
        }
        for (int i = 0; i < listSkillNode.dataSkillNode.Count; i++)
        {
            saveAllData._12_skillNode[i]._01_id = i;
            saveAllData._12_skillNode[i]._02_state = (int)listSkillNode.dataSkillNode[i].state;
        }
        for (int i = 0; i < listAchievement.listAchievement.Count; i++)
        {
            saveAllData._13_achies[i]._01_id = i;
            saveAllData._13_achies[i]._02_state = (int)listAchievement.listAchievement[i].stateAchievement;
        }
        SaveLoadData.instance.SaveData();
    }

    public void LoadMenu(SaveAllData data)
    {
        checkAcie.countBoss = data._01_countBoss;
        checkAcie.countEnemy = data._02_countEnemy;
        checkAcie.countGold = data._03_countGold;
        checkAcie.countSignIn = data._04_countSignIn;
        checkAcie.countLevel = data._05_maxLevel;

        inforMenu.money = data._06_money;
        inforMenu.diamond = data._07_diamond;
        inforMenu.ironKey = data._08_ironKey;
        inforMenu.silverKey = data._09_silverKey;

        for (int i = 0; i < data._10_perks.Count; i++)
        {
            if (i < equipPerk.equippedPerks.Count)
            {
                PerkData perk = listPerkData.listPerk.Find(p => p.id == data._10_perks[i]._02_idPerk);
                if (perk != null)
                {
                    equipPerk.equippedPerks[i] = perk;
                }
            }
        }

        listPerkData.Set();
        for (int i = 0; i < data._11_listPerk.Count; i++)
        {
            if (i < listPerkData.listPerk.Count)
            {
                listPerkData.listPerk[i].id = data._11_listPerk[i]._02_idPerk;
                listPerkData.listPerk[i].perkState = (PerkState)data._11_listPerk[i]._03_statePerk;
                listPerkData.listPerk[i].levelPerk = data._11_listPerk[i]._04_levelPerk;
                listPerkData.listPerk[i].quantity = data._11_listPerk[i]._05_quantityPerk;
            }
        }

        for (int i = 0; i < data._12_skillNode.Count; i++)
        {
            if (i < listSkillNode.dataSkillNode.Count)
            {
                listSkillNode.dataSkillNode[i].state = (SkillNodeState)data._12_skillNode[i]._02_state;
            }
        }

        for (int i = 0; i < data._13_achies.Count; i++)
        {
            if (i < listAchievement.listAchievement.Count)
            {
                listAchievement.listAchievement[i].stateAchievement = (StateAchie)data._13_achies[i]._02_state;
            }
        }
    }
}
