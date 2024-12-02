using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{

    public int pointSkill;


    public float mana;
    [Header("Skill1")]
    public int levelSkill1 = 1;
    public GameObject prefabSword;
    public int numberSword;
    public int dame1 = 1;
    public float timeconti1 = 5;
    public float counD1 = 20f;
    public bool endCountDow1 = true;
    public float orbitRadius = 2f;
    public float rotationSpeed = 100f;
    public Transform swordSpawn;
    private List<GameObject> swords = new List<GameObject>();
    public int manaskill1 = 10;
    private bool activeSkill = false;
    private Coroutine coroutine;

    [Header("Skill2")]
    public int levelSkill2 = 1;
    public int dameBuff = 10;
    public float counD2 = 25f;
    public bool endCountDow2 = true;
    public float duration = 10f;
    public int manaskill2 = 20;
    public float ameInterval = 1f;
    public GameObject eff;
    public bool buff = false;

    Animator animator;
    [Header("Skill 3")]
    public int levelSkill3 = 1;
    public GameObject eff1;
    public int dame3 = 40;
    public float counD3 = 35f;
    public bool endCountDow3 = true;
    public GameObject colliderSkill;
    public int manaskill3 = 35;
    PlayerInfor playerInfor;
    AudioSource audioSource;
    public SoundEffect soundEffect;
    public float maxRange = 5f;

    public TMP_Text lvsk1;
    public TMP_Text lvsk2;
    public TMP_Text lvsk3;

    public Button skill1;
    public Button skill2;
    public Button skill3;
    PlayerController playerController;

    public int dame = 0;
    ComboAtack comboAtack;
    void Start()
    {
        playerInfor = GetComponent<PlayerInfor>();
        animator = GetComponent<Animator>();
        skill1 = GameObject.Find("Skill1").GetComponent<Button>();
        skill2 = GameObject.Find("Skill2").GetComponent<Button>();
        skill3 = GameObject.Find("Skill3").GetComponent<Button>();
        lvsk1 = GameObject.FindWithTag("LevelSkill1").GetComponent<TMP_Text>();
        lvsk2 = GameObject.FindWithTag("LevelSkill2").GetComponent<TMP_Text>();
        lvsk3 = GameObject.FindWithTag("LevelSkill3").GetComponent<TMP_Text>();
        skill1.onClick.AddListener(BuffDameNormal);
        skill2.onClick.AddListener(ActiveSkill);
        skill3.onClick.AddListener(SpawmSkillUltimate);
        lvsk1.text = "Lv : " + levelSkill1.ToString();
        lvsk2.text = "Lv : " + levelSkill2.ToString();
        lvsk3.text = "Lv : " + levelSkill3.ToString();
        playerController = GetComponent<PlayerController>();
        dame = playerInfor.dame;
        audioSource = FindObjectOfType<AudioSource>();
        soundEffect = FindObjectOfType<SoundEffect>();
        comboAtack = GetComponent<ComboAtack>();
        dame = playerInfor.dame;
        LoadSkill();
    }
    void Update()
    {
        mana = playerInfor.manaPoint;
        dame = playerInfor.dame;
        if (activeSkill)
        {
            RotateSwords();
        }
        pointSkill = gameObject.GetComponent<PlayerInfor>().skillPoint;
        if (Input.GetKeyDown(KeyCode.M))
        {
            UpdateSkill(1);
        }if (Input.GetKeyDown(KeyCode.H))
        {
            UpdateSkill(2);
        }if (Input.GetKeyDown(KeyCode.B))
        {
            UpdateSkill(3);
        }
    }

    public void LoadSkill()
    {
        if (PlayerPrefs.HasKey("Skill1"))
        {
            levelSkill1 = PlayerPrefs.GetInt("Skill1");
            levelSkill2 = PlayerPrefs.GetInt("Skill2");
            levelSkill3 = PlayerPrefs.GetInt("Skill3");
            numberSword = PlayerPrefs.GetInt("NumberSw");
            counD1 = PlayerPrefs.GetFloat("CD1");
            timeconti1 = PlayerPrefs.GetFloat("Tmc1");
            counD2 = PlayerPrefs.GetFloat("CD2");
            counD3 = PlayerPrefs.GetFloat("CD3");
            dame1 = PlayerPrefs.GetInt("DameSk1");
            manaskill1 = PlayerPrefs.GetInt("MN1");
            manaskill2 = PlayerPrefs.GetInt("MN2");
            dameBuff = PlayerPrefs.GetInt("DameSk2");
            manaskill3 = PlayerPrefs.GetInt("MN3");
            dame3 = PlayerPrefs.GetInt("DameSk3");
        }
        else
        {
            levelSkill1 = 1;
            levelSkill2 = 1;
            levelSkill3 = 1;
            numberSword = 2;
            counD1 = 20f;
            timeconti1 = 5;
            counD2 = 25f;
            counD3 = 35f;
            dame1 = 1;
            manaskill1 = 10;
            manaskill2 = 20;
            manaskill3 = 35;
            dameBuff = 10;
            dame3 = 40;
        }
    }
    public void SaveSkill()
    {
        PlayerPrefs.SetInt("Skill1", levelSkill1);
        PlayerPrefs.SetInt("Skill2", levelSkill2);
        PlayerPrefs.SetInt("Skill3", levelSkill3);
        PlayerPrefs.SetInt("NumberSw",numberSword);
        PlayerPrefs.SetInt("DameSk1",dame1);
        PlayerPrefs.SetInt("MN1", manaskill1);
        PlayerPrefs.SetFloat("CD1", counD1);
        PlayerPrefs.SetFloat("Tmc1", timeconti1);
        PlayerPrefs.SetFloat("CD2", counD2);
        PlayerPrefs.SetFloat("CD3", counD3);
        PlayerPrefs.SetInt("MN2", manaskill2);
        PlayerPrefs.SetInt("DameSk2", dameBuff);
        PlayerPrefs.SetInt("MN3", manaskill3);
        PlayerPrefs.SetInt("DameSk3", dame3);
        PlayerPrefs.Save();
    }
    public void BuffDameNormal()
    {
        if (!endCountDow2)
        {
            Debug.Log("Chưa hồi chiêu");
            return;
        }
        if (!CheckMana(manaskill2))
        {
            return;
        }
        else
        {
            if (!buff)
            {
                endCountDow2 = false;
                StartCoroutine(CountDown(counD2, 2));
                StartCoroutine(BuffDame());
                playerInfor.manaPoint -= manaskill2;
            }
        }
    }
    IEnumerator BuffDame()
    {
        dame = playerInfor.dame;
        buff = true;
        float elapsedTime = 0f;
        GameObject effhp = Instantiate(eff, transform.position, Quaternion.identity);
        effhp.transform.SetParent(transform);
        soundEffect.PlaySound("BuffDame");
        playerInfor.dame += dameBuff;
        Debug.Log("buff");
        while (elapsedTime < duration)
        {
            yield return new WaitForSeconds(ameInterval); // Chờ 1 giây
            elapsedTime += ameInterval;
        }
        Debug.Log("0 buff");

        playerInfor.dame = dame;
        Destroy(effhp);
        buff = false;
        playerInfor.SaveData();
    }
    void SpawmSkillUltimate()
    {
        if (!CheckMana(manaskill3))
        {
            return;
        }
        if (comboAtack.targetEnemy == null)
        {
            Debug.Log("Không có mục tiêu");
            return;
        }
        else if (!endCountDow3)
        {
            Debug.Log(" Chưa hồi chiêu");
            return;
        }
        else if (endCountDow3)
        {
            endCountDow3 = false;
            StartCoroutine(CountDown(counD3, 3));
            playerInfor.manaPoint -= manaskill3;
            playerController.freeze = true;
            animator.SetTrigger("Skill 1");
            GameObject explo = Instantiate(eff1, comboAtack.targetEnemy.position, Quaternion.identity);
            GameObject coliskill = Instantiate(colliderSkill, explo.transform.position, Quaternion.identity);
            coliskill.transform.SetParent(explo.transform);
            Destroy(explo, 5f);
        }
    }
    void SpawnSwords()
    {
        for (int i = 0; i < numberSword; i++)
        {
            float angle = i * Mathf.PI * 2 / numberSword;
            Vector3 newPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * orbitRadius;

            Vector3 direction = (newPos).normalized;
            Quaternion swordRotation = Quaternion.LookRotation(direction);
            GameObject sword = Instantiate(prefabSword, swordSpawn.position + newPos, swordRotation);
            swords.Add(sword);
        }
    }
    void RotateSwords()
    {
        for (int i = 0; i < swords.Count; i++)
        {
            float angle = (i * Mathf.PI * 2 / numberSword) + Time.time * rotationSpeed;
            Vector3 newPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * orbitRadius;
            swords[i].transform.position = swordSpawn.position + newPos;

            Vector3 direction = (swords[i].transform.position - swordSpawn.position).normalized;
            swords[i].transform.rotation = Quaternion.LookRotation(direction);
            swords[i].transform.Rotate(90, 0, 0);
        }
    }

    IEnumerator DeactivateSkillAfterTime()
    {
        yield return new WaitForSeconds(timeconti1);
        DeactivateSkill();
    }
    void DeactivateSkill()
    {
        if (activeSkill)
        {
            foreach (var sword in swords)
            {
                Destroy(sword);
            }
            swords.Clear();
            activeSkill = false;
        }
    }
    void ActiveSkill()
    {
        if (!endCountDow1)
        {
            Debug.Log("Chưa hồi chiêu");
            return;
        }
        if (!CheckMana(manaskill1))
        {
            return;
        }
        if (!activeSkill)
        {
            endCountDow1 = false;
            StartCoroutine(CountDown(counD1, 1));
            activeSkill = true;
            playerInfor.manaPoint -= manaskill1;
            SpawnSwords();
            StartCoroutine(DeactivateSkillAfterTime());

        }
    }

    IEnumerator CountDown(float time, int skill)
    {
        yield return new WaitForSeconds(time);
        SetSkillReady(skill, true);
    }
    private void SetSkillReady(int skillIndex, bool isReady)
    {
        if (skillIndex == 1) endCountDow1 = isReady;
        if (skillIndex == 2) endCountDow2 = isReady;
        if (skillIndex == 3) endCountDow3 = isReady;
    }
    bool CheckMana(int manaskill)
    {
        if (mana < manaskill)
        {
            Debug.Log("Không đủ mana để dùng skill này");
            return false;
        }
        return true;
    }

    public void UpdateSkill(int skillName)
    {
        if (pointSkill == 0)
        {
            Debug.Log("Khoong đủ điểm nâng cấp Skill");
            return;
        }
        else if (pointSkill >= 1)
        {
            playerInfor.skillPoint -= 1;
            if (skillName == 1)
            {
                UpdateSkill1();
            }
            else if (skillName == 2)
            {
                UpdateSkill2();
            }
            else if (skillName == 3)
            {
                UpdateSkill3();
            }
            SaveSkill();
        }
    }

    public void UpdateSkill1()
    {
        if (levelSkill1 == 5)
        {
            Debug.Log("Skill đạt cấp tối đa");
            return;
        }
        else if (levelSkill1 < 5)
        {

            levelSkill1++;
            if (levelSkill1 == 3 || levelSkill1 == 5)
            {
                numberSword++;
            }
            manaskill1 += 2;
            dame1 += 1;
            timeconti1++;
            counD1 -= 1;
            Debug.Log("Nang capp 1 thanh cong");
        }
        lvsk1.text = "Lv : " + levelSkill1.ToString();

    }
    public void UpdateSkill2()
    {
        if (levelSkill2 == 5)
        {
            Debug.Log("Skill đạt cấp tối đa");
            return;
        }
        else if (levelSkill2 < 5)
        {
            levelSkill2++;
            dameBuff += 4;
            manaskill2 += 4;
            counD2--;
            Debug.Log("Nang capp 2 thanh cong");
        }
        lvsk2.text = "Lv : " + levelSkill2.ToString();

    }
    public void UpdateSkill3()
    {
        if (levelSkill3 == 5)
        {
            Debug.Log("Skill đạt cấp tối đa");
            return;
        }
        else if (levelSkill3 < 5)
        {
            levelSkill3++;
            dame3 += 5;
            manaskill3 += 7;
            counD3 -= 2.5f;
            Debug.Log("Nang capp 1 thanh cong");
        }
        lvsk3.text = "Lv : " + levelSkill3.ToString();

    }
}
