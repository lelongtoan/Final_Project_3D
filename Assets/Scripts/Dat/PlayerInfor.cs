using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfor : MonoBehaviour
{
    public static PlayerInfor Instance; // Biến tĩnh để lưu trữ đối tượng Player

    [Header("Thông tin nhân vật")]
    public float hp = 100f;
    public float currenthp = 100f;
    public float mp = 100f;
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

    public void Heal(int amount)
    {
        currenthp += amount;

        if (currenthp > hp)
        {
            currenthp = hp;
        }
    }
    // Hàm nhận sát thương
    public void PlayerReciveDame(float atk)
    {
        currenthp -= atk;
        if (currenthp <= 0)
        {
            hp = 0;
            // Xử lý khi nhân vật chết
        }
    }

    public float PlayerUpdateDame()
    {
        return dame;
    }

    public void PlayerUseSkill(float mana)
    {
        mp -= mana;
        if (mp < 0)
        {
            mp = 0;
        }
    }
    public void PlayerTakeDame(int hp)
    {
        currenthp -= hp;
    }
}
