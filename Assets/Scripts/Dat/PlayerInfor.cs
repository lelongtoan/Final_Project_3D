using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfor : MonoBehaviour
{
    public static PlayerInfor Instance; // Biến tĩnh để lưu trữ đối tượng Player

    [Header("Thông tin nhân vật")]
    public float maxHP = 100f;
    public float healthPoint = 100f;
    public float manaPoint = 100f;
    public float maxMP = 100f;
    public int def = 10;
    public int dame = 10;


    private void Awake()
    {
        // Đảm bảo rằng chỉ có một instance duy nhất của player
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Hủy các instance khác nếu đã tồn tại
        }
    }

    public void HealthRecovery(int amount)
    {
        healthPoint += amount;

        if (healthPoint > maxHP)
        {
            healthPoint = maxHP;
        }
    }public void ManaRecover(int amount)
    {
        manaPoint += amount;

        if (manaPoint > maxHP)
        {
            manaPoint = maxHP;
        }
    }
    // Hàm nhận sát thương
    public void PlayerReciveDame(float atk)
    {
        healthPoint -= atk;
        if (healthPoint <= 0)
        {
            maxHP = 0;
            // Xử lý khi nhân vật chết
        }
    }

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
    }
    public void PlayerTakeDame(int hp)
    {
        healthPoint -= hp;
    }
}
