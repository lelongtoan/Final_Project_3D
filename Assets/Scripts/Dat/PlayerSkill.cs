using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{ 
    Animator animator;
    [Header("Skill 3")]
    public GameObject eff1;
    public AudioClip clip1;
    public float dame3 = 25f;
    public bool endCountDow3 = true;
    public float timingskill = 1f;
    public int exploCount = 3;
    public float size = 1;
    public float upsize = 1.5f;
    public float exploDistance = 2f;
    public Transform skill1Spawm;
    public GameObject colliderSkill;
    PlayerInfor playerInfor;
    public float dame = 0f;
    AudioSource audioSource;
    public SoundEffect soundEffect;
    public float maxRange = 5f;
    public LineRenderer lineRenderer;
    private Vector3 startTouchPos;
    private Vector3 direction;



    [Header("Skill1")]
    public GameObject prefabSword;
    public int numberSword;
    public int dame1 = 1;
    public bool endCountDow2 = true;
    public float orbitRadius = 2f;
    public float rotationSpeed = 100f;
    public Transform skill2Spawn;
    private List<GameObject> swords = new List<GameObject>();
    private bool activeSkill = false;
    private Coroutine coroutine;

    [Header("Skill2")]
    public int hpAmount = 2;
    public float duration = 10f;
    public float hpInterval = 1f;
    public GameObject eff;
    private bool isHealing = false;


    protected Button skill1;
    protected Button skill2;
    protected Button skill3;
    PlayerController playerController;
    void Start()
    {
        playerInfor = GetComponent<PlayerInfor>();
        animator = GetComponent<Animator>();
        skill1 = GameObject.Find("Skill1").GetComponent<Button>();
        skill2 = GameObject.Find("Skill2").GetComponent<Button>();
        skill3 = GameObject.Find("Skill3").GetComponent<Button>();
        skill1.onClick.AddListener(ActivateHeal);
        skill2.onClick.AddListener(ActiveSkill);
        skill3.onClick.AddListener(SpawmSkillUltimate);
        playerController = GetComponent<PlayerController>();
        dame = playerInfor.dame;
        audioSource = FindObjectOfType<AudioSource>();
        soundEffect = FindObjectOfType<SoundEffect>();
    }
    public void ActivateHeal()
    {
        if (!isHealing)
        {
            StartCoroutine(HealOverTime());
        }
    }
    IEnumerator HealOverTime()
    {
        isHealing = true;
        float elapsedTime = 0f;
        GameObject effhp = Instantiate(eff, transform.position, Quaternion.identity);
        effhp.transform.SetParent(transform);
        while (elapsedTime < duration)
        {
            // Mỗi giây hồi phục 2 máu
            playerInfor.Heal(hpAmount);
            soundEffect.PlaySound("RecoveryHP");
            yield return new WaitForSeconds(hpInterval); // Chờ 1 giây
            elapsedTime += hpInterval;
        }
        isHealing = false;
        Destroy(effhp);
    }
    void Update()
    {
        if (activeSkill)
        {
            RotateSwords();
        }
    }
    void SpawmSkillUltimate()
    {
        if (endCountDow3)
        {
            playerController.freeze = true;
            animator.SetTrigger("Skill 1");
            GameObject explo = Instantiate(eff1, skill1Spawm.transform.position, Quaternion.identity);
            GameObject coliskill = Instantiate(colliderSkill, explo.transform.position, Quaternion.identity);
            Destroy(explo, 5f);
            coliskill.transform.SetParent(explo.transform);
            StartCoroutine(CountDown(5,"Skill3"));
            endCountDow3 = false;
        }
        else
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

            
            GameObject sword = Instantiate(prefabSword, skill2Spawn.position + newPos, swordRotation);
            swords.Add(sword);
        }
    }
    void RotateSwords()
    {
        for (int i = 0; i < swords.Count; i++)
        {
            float angle = (i * Mathf.PI * 2 / numberSword) + Time.time * rotationSpeed;
            Vector3 newPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * orbitRadius;
            swords[i].transform.position = skill2Spawn.position + newPos;

            Vector3 direction = (swords[i].transform.position - skill2Spawn.position).normalized;
            swords[i].transform.rotation = Quaternion.LookRotation(direction);
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
        if (!activeSkill)
        {
            activeSkill = true;
            SpawnSwords();
            StartCoroutine(DeactivateSkillAfterTime());
        }
    }

    IEnumerator CountDown(float time,string name)
    {
        Debug.Log("Dung");
        yield return new WaitForSeconds(time);
        if (name == "Skill1")
        {
           // endCountDow1 = true;
        }
        else if (name == "Skill2")
        {
            endCountDow2 = true;
        }
        else if (name == "Skill3")
        {
            endCountDow3 = true;
        }
    }

    
}
