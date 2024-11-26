using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    public float mana;
    [Header("Skill1")]
    public float levelskill1 = 1;
    public GameObject prefabSword;
    public int numberSword;
    public int dame1 = 1;
    public bool endCountDow1 = true;
    public float orbitRadius = 2f;
    public float rotationSpeed = 100f;
    public Transform swordSpawn;
    private List<GameObject> swords = new List<GameObject>();
    public int manaskill1 = 10;
    private bool activeSkill = false;
    private Coroutine coroutine;

    [Header("Skill2")]
    public int hpAmount = 2;
    public bool endCountDow2 = true;
    public float duration = 10f;
    public int manaskill2 = 20;
    public float ameInterval = 1f;
    public GameObject eff;
    public bool buff = false;

    Animator animator;
    [Header("Skill 3")]
    public GameObject eff1;
    public AudioClip clip1;
    public float dame3 = 35f;
    public bool endCountDow3 = true;
    public float timingskill = 1f;
    public int exploCount = 3;
    public float size = 1;
    public float upsize = 1.5f;
    public float exploDistance = 2f;
    public Transform skill1Spawm;
    public GameObject colliderSkill;
    public int manaskill3 = 50;
    PlayerInfor playerInfor;
    AudioSource audioSource;
    public SoundEffect soundEffect;
    public float maxRange = 5f;
    public LineRenderer lineRenderer;
    private Vector3 startTouchPos;
    private Vector3 direction;

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
        skill1.onClick.AddListener(BuffDameNormal);
        skill2.onClick.AddListener(ActiveSkill);
        skill3.onClick.AddListener(SpawmSkillUltimate);
        playerController = GetComponent<PlayerController>();
        dame = playerInfor.dame;
        audioSource = FindObjectOfType<AudioSource>();
        soundEffect = FindObjectOfType<SoundEffect>();
        comboAtack = GetComponent<ComboAtack>();
        dame = playerInfor.dame;
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
                CountDown(15, 2);
                playerInfor.manaPoint -= manaskill2;
                StartCoroutine(BuffDame());
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
        playerInfor.dame += 10;
        while (elapsedTime < duration)
        {
            yield return new WaitForSeconds(ameInterval); // Chờ 1 giây
            elapsedTime += ameInterval;
        }
        playerInfor.dame = dame;
        Destroy(effhp);
        buff = false;
        playerInfor.SaveData();
    }
    void Update()
    {
        mana = playerInfor.manaPoint;
        if (activeSkill)
        {
            RotateSwords();
        }
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
        else if (endCountDow3)
        {
            StartCoroutine(CountDown(30, 3));
            playerInfor.manaPoint -= manaskill3;
            playerController.freeze = true;
            animator.SetTrigger("Skill 1");
            GameObject explo = Instantiate(eff1, comboAtack.targetEnemy.position, Quaternion.identity);
            GameObject coliskill = Instantiate(colliderSkill, explo.transform.position, Quaternion.identity);
            Destroy(explo, 5f);
            coliskill.transform.SetParent(explo.transform);
            endCountDow3 = false;
        }
        else if (!endCountDow3)
        {
            Debug.Log(" Chưa hồi chiêu");
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
        yield return new WaitForSeconds(5);
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
            CountDown(5, 1);
            activeSkill = true;
            playerInfor.manaPoint -= manaskill1;
            SpawnSwords();
            StartCoroutine(DeactivateSkillAfterTime());
        }
    }

    IEnumerator CountDown(float time, int skill)
    {
        SetSkillReady(skill, false);
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
}
