using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInfor : MonoBehaviour
{
    public static PlayerInfor Instance; // Biến tĩnh để lưu trữ đối tượng Player

    [Header("Thông tin nhân vật")]
    public PlayerData playerData;

    public float maxHP = 100f;
    public float healthPoint = 100f;
    public float manaPoint = 100f;
    public float maxMP = 100f;
    public int def = 10;
    public int dame = 10;
    public int exp = 0;
    public int money = 0;


    private void Awake()
    {
        // Đảm bảo rằng chỉ có một instance duy nhất của player
        if (Instance == null)
        {
            playerData.ResetData();
            Instance = this;
        }
        else
        {
            Debug.Log("Load Data");
            LoadData();
            Destroy(gameObject); // Hủy các instance khác nếu đã tồn tại
        }
    }
    private void Start()
    {
        LoadData();
    }
    private void LoadData()
    {
        if (playerData != null)
        {
            maxHP = playerData.maxHP;
            healthPoint=playerData.healthPoint;
            maxMP = playerData.maxMP;
            manaPoint = playerData.manaPoint;
            def = playerData.def;
            money = playerData.money;
            dame = playerData.dame;
            exp = playerData.exp;
        }
    }
    public void SaveData()
    {
        if (playerData != null)
        {
            playerData.maxHP = maxHP;
            playerData.healthPoint = healthPoint;
            playerData.maxMP = maxMP;
            playerData.manaPoint = manaPoint;
            playerData.dame = dame;
            playerData.def = def;
            playerData.exp = exp;
            playerData.money = money;
        }
    }
    public void HealthRecovery(int amount)
    {
        healthPoint += amount;

        if (healthPoint > maxHP)
        {
            healthPoint = maxHP;
        }
        SaveData();
    }
    public void ManaRecover(int amount)
    {
        manaPoint += amount;

        if (manaPoint > maxHP)
        {
            manaPoint = maxHP;
        }
        SaveData();
    }
    // Hàm nhận sát thương

    public float PlayerUpdateDame()
    {
        return dame;
    }

    public void PlayerUseSkill(float mana)
    {
        manaPoint -= mana;
        if (manaPoint < 0)
        {
            manaPoint = 0;
        }
        SaveData();
    }
    public void PlayerTakeDame(int hp)
    {
        hp -= def;
        if (hp < 0)
        {
            hp = 0;
        }
        healthPoint -= hp;
        SaveData();
    }
}
