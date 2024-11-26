using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level = 1;
    public float baseXp = 10f;
    public float scaleFactor = 1.5f;
    public PlayerInfor inf;
    public float currentXp;
    public float XPToLevelUp => baseXp * Mathf.Pow(level, scaleFactor);

    void Start()
    {
        inf = GetComponent<PlayerInfor>();
    }

    void Update()
    {
        currentXp = inf.exp;
        CheckLevelUp();
    }
    private void CheckLevelUp()
    {
        while (currentXp >= XPToLevelUp)
        {
            inf.exp -= XPToLevelUp;
            level++;
            OnLevelUp();
        }
    }
    private void OnLevelUp()
    {

    }
}
