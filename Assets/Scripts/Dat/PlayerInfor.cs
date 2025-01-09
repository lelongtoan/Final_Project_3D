using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInfor : MonoBehaviour
{
    public static PlayerInfor Instance;

    public CameraShake shake;
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
    public int point = 0;
    Animator animator;
    public bool isDead = false;
    public Image hpbar;
    public Image mpbar;
    public Image Exp_Image;
    public TMP_Text text;
    public TMP_Text hp_Text;
    public TMP_Text mp_Text;
    public TMP_Text gold_Text;

    public Transform parent;
    public GameObject deadPanel;
    float baseXp = 10f;
    public float XPToLevelUp => baseXp * level;

    public Button reSpawn;
    public Button reTurn;
    public Button endGame;

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
            LoadData();
        }
    }

    bool isFirst()
    {
        return PlayerPrefs.GetInt("FirstStart1", 1) == 1;
    }
    private void Start()
    {
        if (isFirst())
        {
            playerData.Initialaze();
            PlayerPrefs.SetInt("FirstStart1", 0);
            PlayerPrefs.Save();
            LoadData();
            SaveData();
        }
        else
        {
            LoadData();
        }
        healthPoint = maxHP;
        manaPoint = maxMP;
        SaveData();
        shake = FindObjectOfType<CinemachineVirtualCamera>().GetComponent<CameraShake>();
        hpbar = GameObject.FindWithTag("HPBar").GetComponent<Image>();
        mpbar = GameObject.FindWithTag("MPBar").GetComponent<Image>();  
        Exp_Image = GameObject.FindWithTag("EXP").GetComponent<Image>();
        text= GameObject.FindWithTag("Exp_Text").GetComponent<TMP_Text>();
        hp_Text = GameObject.FindWithTag("HPText").GetComponent<TMP_Text>();
        mp_Text = GameObject.FindWithTag("MPText").GetComponent<TMP_Text>();
        gold_Text = GameObject.FindWithTag("GoldText").GetComponent<TMP_Text>();
        isDead = false;
        animator = gameObject.GetComponent<Animator>();
        parent = GameObject.Find("UIGame").transform;
        deadPanel = parent.Find("GameOver").gameObject;
        Transform imageui = deadPanel.transform.Find("Image");
        reSpawn = imageui.Find("Respawn").GetComponent<Button>();
        reTurn = imageui.Find("Return").GetComponent<Button>();
        endGame = imageui.Find("EndGame").GetComponent<Button>();
        reSpawn.onClick.AddListener(Respawn);
        reTurn.onClick.AddListener(Return);
        endGame.onClick.AddListener(EndGame);
        
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
        UpdateHpMpGold();
        CheckLevelUp();
        CheclDead();
        //UpdateInfor();
    }
    void UpdateLevel()
    {
        text.text = "Lv : " + level.ToString();
    }
    void UpdateHpMpGold()
    {
        gold_Text.text = money.ToString();
        hp_Text.text = healthPoint + " / " + maxHP.ToString();
        mp_Text.text = manaPoint.ToString() + " / " + maxMP.ToString();
        mpbar.fillAmount = manaPoint / maxMP;
        hpbar.fillAmount = healthPoint / maxHP;
    }
    
    void UpdateExp()
    {
        Exp_Image.fillAmount = exp / XPToLevelUp;
    }
    public void LoadData()
    {
        maxHP = playerData.maxHP;
        healthPoint = playerData.healthPoint;
        maxMP = playerData.maxMP;
        manaPoint = playerData.manaPoint;
        def = playerData.def;
        dame = playerData.dame;
        level = playerData.level;
        exp = playerData.exp;
        money = playerData.money;
        point = playerData.point;
    }
    private void LoadDataByDie()
    {
        maxHP = playerData.maxHP;
        healthPoint = playerData.maxHP;
        maxMP = playerData.maxMP;
        manaPoint = playerData.maxMP;
        SaveData();
        Time.timeScale = 1;
    }
    private void EndGame()
    {

    }
    private void Respawn()
    {
        LoadDataByDie();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    public void Return()
    {
        LoadDataByDie();
        SceneManager.LoadScene(0);
    }
    public void SaveData()
    {
        playerData.maxHP = maxHP;
        playerData.healthPoint = healthPoint;
        playerData.maxMP = maxMP;
        playerData.manaPoint = manaPoint;
        playerData.def = def;
        playerData.dame = dame;
        playerData.level = level;
        playerData.exp = exp;
        playerData.money = money;
        playerData.point = point;

        #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(playerData);
        #endif
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
        if (hp <= 0)
        {
            hp = 1;
        }
        healthPoint -= hp;
        shake.ShakeCamera(1.5f, 0.2f);
        Vector3 hit = -transform.forward;
        gameObject.GetComponent<PlayerHit>().TakeDamage(hit);

        Debug.Log("Shake");
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
    public void GetPoint(int moneyCollect)
    {
        point += moneyCollect;
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
        AchievementCheck check = GameInstance.instance.achieCheck;
        while(exp >= XPToLevelUp)
        {
            exp -= XPToLevelUp;
            level++;
            OnLevelUp();
            if (check.countLevel < level)
            {
                check.countLevel = level;
            }
            SaveData();
        }
    }
    void OnLevelUp()
    {
        maxHP += 5;
        maxMP += 5;
        GetComponent<PlayerController>().UpdateManaDash();
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
