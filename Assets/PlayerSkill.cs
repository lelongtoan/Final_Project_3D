﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{ 
    Animator animator;
    [Header("Skill 1")]
    public GameObject eff1;
    public AudioClip clip1;
    public bool endCountDow1 = true;
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



    [Header("Skill2")]
    public GameObject prefabSword;
    public int numberSword;
    public bool endCountDow2 = true;
    public float orbitRadius = 2f;
    public float rotationSpeed = 100f;
    public Transform skill2Spawn;
    private List<GameObject> swords = new List<GameObject>();
    private bool activeSkill = false;
    private Coroutine coroutine;


    protected Button skill1;
    protected Button skill3;
    //public Button skill3;
    PlayerController playerController;
    void Start()
    {
        playerInfor = GetComponent<PlayerInfor>();
        animator = GetComponent<Animator>();
        skill1 = GameObject.Find("Skill1").GetComponent<Button>();
        skill3 = GameObject.Find("Skill3").GetComponent<Button>();
        skill1.onClick.AddListener(SpawmSkill1);
        skill3.onClick.AddListener(ActiveSkill);
        playerController = GetComponent<PlayerController>();
        dame = playerInfor.dame;
        audioSource = FindObjectOfType<AudioSource>();


    }

    void Update()
    {
        if (activeSkill)
        {
            RotateSwords();
        }
    }
    void SpawmSkill1()
    {
        if (endCountDow1)
        {
            playerController.freeze = true;
            animator.SetTrigger("Skill 1");
            StartCoroutine(CountDown(5,"Skill1"));
            endCountDow1 = false;
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
            sword.transform.SetParent(skill2Spawn);
            swords.Add(sword);
        }
    }
    void RotateSwords()
    {
        for (int i = 0; i < swords.Count; i++)
        {
            float angle = Time.time * rotationSpeed + i * Mathf.PI * 2 / numberSword;
            Vector3 newPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * orbitRadius;

            swords[i].transform.localPosition = newPos;
            swords[i].transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator DeactivateSkillAfterTime()
    {
        yield return new WaitForSeconds(8); // Đợi thời gian tồn tại của skill
        DeactivateSkill(); // Dừng skill
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


    private IEnumerator TriggerExplosion()
    {
        //float dame = playerInfor.dame;
        float a = dame;
        Vector3 exPositio = skill1Spawm.position;
        Vector3 expDirection = skill1Spawm.forward;
        float currentSize = size;
        for (int i = 0; i < exploCount; i++)
        {
            playerInfor.dame += 2 * (i + 1);
            GameObject explo = Instantiate(eff1, exPositio, Quaternion.identity);
            GameObject coliskill = Instantiate(colliderSkill, explo.transform.position,Quaternion.identity);
            audioSource.PlayOneShot(clip1);
            explo.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
            coliskill.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
            currentSize *= upsize;
            exPositio += expDirection.normalized * exploDistance;
            Destroy(coliskill, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
        playerInfor.dame = a ;
    }
    IEnumerator CountDown(float time,string name)
    {
        Debug.Log("Dung");
        yield return new WaitForSeconds(time);
        if (name == "Skill1")
        {
            endCountDow1 = true;
        }
        else if (name == "Skill2")
        {
            endCountDow2 = true;
        }
    }

}
