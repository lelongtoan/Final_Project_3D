using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfor : MonoBehaviour
{
    [Header("Thông tin nhân vật")]
    public float hp = 100f;
    public float mp = 100f;
    public float dame = 10;




    // Hàm nhận sát thương
    public void PlayerReciveDame(float atk)
    {
        hp = atk;
    }

    public float PlayerUpdateDame()
    {
        return dame;
    }

    public void PlayerUseSkill(float mana)
    {
        mp -= mana;
    }
}
