using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfor : MonoBehaviour
{
    public static PlayerInfor Instance; // Biến tĩnh để lưu trữ đối tượng Player

    [Header("Thông tin nhân vật")]
    public float hp = 100f;
    public float mp = 100f;
    public float dame = 10;

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

    // Hàm nhận sát thương
    public void PlayerReciveDame(float atk)
    {
        hp -= atk;
        if (hp <= 0)
        {
            hp = 0;
            // Xử lý khi nhân vật chết
        }
    }

    // Hàm trả về sát thương hiện tại của người chơi
    public float PlayerUpdateDame()
    {
        return dame;
    }

    // Hàm sử dụng mana cho kỹ năng
    public void PlayerUseSkill(float mana)
    {
        mp -= mana;
        if (mp < 0)
        {
            mp = 0;
        }
    }
}
