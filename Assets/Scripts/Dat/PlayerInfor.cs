using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

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
    public int level = 1;
    public float exp = 0;
    public int money = 0;
    Animator animator;
    public bool isDead = false;
    Image hpbar;
    Image mpbar;
    public Image Exp_Image;
    public TMP_Text text;

    public Transform parent;
    public GameObject deadPanel;
    float baseXp = 10f;
    public float XPToLevelUp => baseXp * level;

    //public GameObject baloP;

    private void Awake()
    {
        // Đảm bảo rằng chỉ có một instance duy nhất của player
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Load Data");
            Destroy(gameObject); // Hủy các instance khác nếu đã tồn tại
        }
        LoadData();
    }
    private void Start()
    {
        hpbar = GameObject.FindWithTag("HPBar").GetComponent<Image>();
        mpbar = GameObject.FindWithTag("MPBar").GetComponent<Image>();
        Exp_Image = GameObject.FindWithTag("EXP").GetComponent<Image>();
        text= GameObject.FindWithTag("Exp_Text").GetComponent<TMP_Text>();
        isDead = false;
        animator = gameObject.GetComponent<Animator>();
        parent = GameObject.Find("UIGame").transform;
        deadPanel = parent.Find("GameOver").gameObject;
    }
    private void Update()
    {
        if (text == null)
        {
            text = GameObject.FindWithTag("Exp_Text").GetComponent<TMP_Text>();
        }
        if (Exp_Image == null)
        {
            Exp_Image = GameObject.FindWithTag("EXP").GetComponent<Image>();
        }
        if(deadPanel == null)
        {
            deadPanel = GameObject.Find("GameOver");
            deadPanel.SetActive(false);
        }
        UpdateLevel();
        UpdateExp();
        UpdateHpMP();
        CheckLevelUp();
        CheclDead();
        //UpdateInfor();
    }
    void UpdateLevel()
    {
        text.text = "Lv : " + level.ToString();
    }
    void UpdateHpMP()
    {
        mpbar.fillAmount = manaPoint / maxMP;
        hpbar.fillAmount = healthPoint / maxHP;
    }
    
    void UpdateExp()
    {
        Exp_Image.fillAmount = exp / XPToLevelUp;
    }
    private void LoadData()
    {
        if (PlayerPrefs.HasKey("Health"))
        {
            maxHP = PlayerPrefs.GetFloat("MaxHP");
            healthPoint = PlayerPrefs.GetFloat("Health");
            maxMP = PlayerPrefs.GetFloat("MaxMP");
            manaPoint = PlayerPrefs.GetFloat("Mana");
            money = PlayerPrefs.GetInt("Money");
            level = PlayerPrefs.GetInt("Level");
            exp = PlayerPrefs.GetFloat("EXP");
            dame = PlayerPrefs.GetInt("Dame");
            def = PlayerPrefs.GetInt("Def");
        }
        else
        {
            healthPoint = maxHP;
            manaPoint = maxMP;
            dame = 10;
            def = 5;
            level = 1;
            money = 0;
            exp = 0;
        }
    }
    public void SaveData()
    {
        PlayerPrefs.SetFloat("MaxHP", maxHP);
        PlayerPrefs.SetFloat("Health", healthPoint);
        PlayerPrefs.SetFloat("MaxMP", maxMP);
        PlayerPrefs.SetFloat("Mana", manaPoint);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetFloat("EXP", exp);
        PlayerPrefs.SetInt("Dame", dame);
        PlayerPrefs.SetInt("Def", def);
        PlayerPrefs.Save();
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

        if (manaPoint > maxMP)
        {
            manaPoint = maxMP;
        }
        SaveData();
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
        if (hp <= 0)
        {
            hp = 0;
        }
        healthPoint -= hp;
        Debug.Log("Tru " + hp.ToString() + " mau");
        SaveData();
    }
    public void PlayerTakeStandanDame(int hp)
    {
        if (hp < 0)
        {
            hp = 0;
            
        }
        healthPoint -= hp;
        Debug.Log("Tru " + hp.ToString() + " mau");
        SaveData();
    }
    public void GetMoney(int moneyCollect)
    {
        money += moneyCollect;
        SaveData();
    }
    public void UseMoney(int moneyUse)
    {
        money -= moneyUse;
        SaveData();
    }
    public void GetExp(int expCollect)
    {
        exp += expCollect;
        SaveData();
    }
    void CheclDead()
    {
        if (healthPoint <= 0)
        {
            Dead();
        }
    }
    void CheckLevelUp()
    {
        while(exp >= XPToLevelUp)
        {
            exp -= XPToLevelUp;
            level++;
            OnLevelUp();
            SaveData();
        }
    }
    void OnLevelUp()
    {
        maxHP += 5;
        maxMP += 5;
        if (level % 5 == 0)
        {
            dame += 2;
            def += 1;
        }
        if(level % 10 == 0)
        {
            gameObject.GetComponent<PlayerSkill>().UpdateSkill1();
            gameObject.GetComponent<PlayerSkill>().UpdateSkill2();
            gameObject.GetComponent<PlayerSkill>().UpdateSkill3();
        }
    }
    public void UpMaxHP(float hp)
    {
        maxHP += hp;
        SaveData() ;
    }
    public void UpMaxMP(float mp)
    {
        maxMP += mp;
        SaveData() ;
    }
    public void UpDame(int dmg)
    {
        dame+= dmg;
        SaveData();
    }
    public void Dead()
    {
        if (!isDead)
        {
            GetComponent<PlayerController>().enabled = false;
            GetComponent<PlayerSkill>().enabled = false;
            GetComponent<Collider>().enabled = false;
            GetComponent<ComboAtack>().enabled = false;
            isDead = true;
            animator.SetTrigger("Dead");
            DeadPanel();
        }
    }
    public void DeadPanel()
    {
        deadPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void DeadPos()
    {
        animator.ResetTrigger("Dead");
        animator.SetTrigger("DeadPos");
    }
    public void UpDef(int defen)
    {
        def+= defen;
        SaveData();
    }
}
