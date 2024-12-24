using System.IO;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewPlayerSkillData", menuName = "Player/Player Skill Data")]
public class PlayerSkillData : ScriptableObject
{
    [Range(1, 5)] public int levelSkill1;
    [Range(1, 5)] public int levelSkill2;
    [Range(1, 5)] public int levelSkill3;
    [Range(2, 5)] public int numberSword;
    public int dame1;
    public int dameBuff;
    public int dame3;
    public int manaskill1;
    public int manaskill2;
    public int manaskill3;
    public float timeconti1;
    public float counD1;
    public float counD2;
    public float counD3;

    public void Initialize()
    {
        levelSkill1 = 1;
        levelSkill2 = 1;
        levelSkill3 = 1;
    }

    public void CheckSkill()
    {
        Skill1();
        Skill2();
        Skill3();
    }

    void Skill1()
    {
        switch (levelSkill1)
        {
            case 1:
                numberSword = 2;
                manaskill1 = 10;
                counD1 = 15;
                dame1 = 2;
                timeconti1 = 5;
                break;
            case 2:
                numberSword = 3;
                manaskill1 = 15;
                counD1 = 14;
                dame1 = 2;
                timeconti1 = 5;
                break;
            case 3:
                numberSword = 3;
                manaskill1 = 18;
                counD1 = 13;
                dame1 = 3;
                timeconti1 = 7;
                break;
            case 4:
                numberSword = 4;
                manaskill1 = 21;
                counD1 = 12;
                dame1 = 3;
                timeconti1 = 7;
                break;
            case 5:
                numberSword = 5;
                manaskill1 = 30;
                counD1 = 10;
                dame1 = 4;
                timeconti1 = 6;
                break;
        }
    }
    void Skill2()
    {
        switch (levelSkill2)
        {
            case 1:
                manaskill2 = 15;
                dameBuff = 10;
                counD2 = 15;
                break;
            case 2:
                manaskill2 = 18;
                dameBuff = 15;
                counD2 = 15;
                break;
            case 3:
                manaskill2 = 21;
                dameBuff = 20;
                counD2 = 15;
                break;
            case 4:
                manaskill2 = 27;
                dameBuff = 25;
                counD2 = 15;
                break;

            case 5:
                manaskill2 = 35;
                dameBuff = 30;
                counD2 = 15;
                break;
        }
    }
    void Skill3()
    {
        switch (levelSkill3)
        {
            case 1:
                dame3 = 25;
                counD3 = 20;
                manaskill3 = 25;
                break;
            case 2:
                dame3 = 35;
                counD3 = 19;
                manaskill3 = 30;
                break;
            case 3:
                dame3 = 45;
                counD3 = 18;
                manaskill3 = 35;
                break;
            case 4:
                dame3 = 55;
                counD3 = 17;
                manaskill3 = 50;
                break;

            case 5:
                dame3 = 70;
                counD3 = 18;
                manaskill3 = 70;
                break;
        }
    }
}
