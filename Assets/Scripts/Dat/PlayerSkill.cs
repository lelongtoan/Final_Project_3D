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
    public PlayerSkillData skillData;
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
    public TMP_Text mn1;
    public TMP_Text mn2;
    public TMP_Text mn3;

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
        mn1 = GameObject.FindWithTag("MN1").GetComponent<TMP_Text>();
        mn2 = GameObject.FindWithTag("MN2").GetComponent<TMP_Text>();
        mn3 = GameObject.FindWithTag("MN3").GetComponent<TMP_Text>();
        LoadSkill();
        skill1.onClick.AddListener(ActiveSkill);
        skill2.onClick.AddListener(BuffDameNormal);
        skill3.onClick.AddListener(SpawmSkillUltimate);
        lvsk1.text = "Lv : " + levelSkill1.ToString();
        lvsk2.text = "Lv : " + levelSkill2.ToString();
        lvsk3.text = "Lv : " + levelSkill3.ToString();
        mn1.text = manaskill1.ToString();
        mn2.text = manaskill2.ToString();
        mn3.text = manaskill3.ToString();
        playerController = GetComponent<PlayerController>();
        dame = playerInfor.dame;
        audioSource = FindObjectOfType<AudioSource>();
        soundEffect = FindObjectOfType<SoundEffect>();
        comboAtack = GetComponent<ComboAtack>();
        dame = playerInfor.dame;
    }
    void Update()
    {
        mana = playerInfor.manaPoint;
        dame = playerInfor.dame;
        if (activeSkill)
        {
            RotateSwords();
        }
    }

    public void LoadSkill()
    {
        levelSkill1 = skillData.levelSkill1;
        levelSkill2 = skillData.levelSkill2;
        levelSkill3 = skillData.levelSkill3;
        dame1 = skillData.dame1;
        dame3 = skillData.dame3;
        dameBuff = skillData.dameBuff;
        counD1 = skillData.counD1;
        counD2 = skillData.counD2;
        counD3 = skillData.counD3;
        timeconti1 = skillData.timeconti1;
        numberSword = skillData.numberSword;
        manaskill1 = skillData.manaskill1;
        manaskill2 = skillData.manaskill2;
        manaskill3 = skillData.manaskill3;
    }
    public void SaveSkill()
    {
        skillData.levelSkill1 = levelSkill1;
        skillData.levelSkill2 = levelSkill2;
        skillData.levelSkill3 = levelSkill3;
        skillData.dame1 = dame1;
        skillData.dame3 = dame3;
        skillData.dameBuff = dameBuff;
        skillData.counD1 = counD1;
        skillData.counD2 = counD2;
        skillData.counD3 = counD3;
        skillData.timeconti1 = timeconti1;
        skillData.numberSword = numberSword;
        skillData.manaskill1 = manaskill1;
        skillData.manaskill2 = manaskill2;
        skillData.manaskill3 = manaskill3;
        #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(skillData);
        #endif
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
        mn1.text = manaskill1.ToString();
        SaveSkill();
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
        mn2.text = manaskill2.ToString();
        SaveSkill();
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
        mn3.text = manaskill3.ToString();
        SaveSkill();
    }
}
