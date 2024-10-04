using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class ComboAtack : MonoBehaviour
{
    [System.Serializable]
    public class Effect
    {
        public int name;
        public GameObject vfx;
    }
    [Header("Hiệu ứng chém")]
    public List<Effect> effects;

    private bool isComboActive = false;
    Animator ani;
    [Header("Chỉ số combo")]
    bool trigger;
    public int combo;
    public int combonum;
    public bool isAttack;
    public float comboTiming;
    public float comboDelay;
    public float attackmove = 0.1f;

    [Header("Hiệu ứng và vị trí spamw hiệu ứng")]
    public Transform effectSpawn;
    public Transform headSword;
    public GameObject VFX;


    [Header("Các thông số khác")]
    public Button attack;
    public Collider atackCollider;
    Rigidbody rb;
    PlayerController playerController;
    void Start()
    {
        attack = GameObject.Find("Click").GetComponent<Button>();
        attack.onClick.AddListener(OnAtackClick);
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        combo = 1;
        comboTiming = 1f;
        comboDelay = comboTiming;
        combonum = 4;
        ani.speed = 0.8f;
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Combodelay();
        if(comboDelay<0)
        {
            combo = 1;
        }
    }

    public void Combo()
    {

        if( comboDelay < 0 && isComboActive == true)
        {
            playerController.freeze = true;
            isAttack = true;
            atackCollider.enabled = true;
            MoveForwardDuringAttack();
            ani.SetTrigger("Attack" + combo);
            comboDelay = comboTiming;
        }
        else if (isComboActive == true && comboDelay > 0 && comboDelay < 0.4)
        {
            playerController.freeze = true;
            isAttack = true;
            atackCollider.enabled = true;
            MoveForwardDuringAttack();
            combo++;
            if (combo > combonum)
            {
                combo = 1;
            }
            ani.SetTrigger("Attack" + combo);
            comboDelay = comboTiming;

        }
        else if(comboDelay<0 && !isComboActive==false)
        {
            isAttack=false;
        }
    }


    private void OnAtackClick()
    {
        if (!isComboActive)
        {
            isComboActive = true;
            Combo();
            isComboActive = false;
        }
    }
    private void Combodelay()
    {
        comboDelay -= Time.deltaTime;
    }
    void MoveForwardDuringAttack()
    {
        // Di chuyển nhân vật khi tấn công
        Vector3 moveDirection = transform.forward * attackmove;
        rb.MovePosition(rb.position + moveDirection);
    }
    public void SetEndCoiler()
    {
        atackCollider.enabled = false;
    }

    public void SpawnEffect(int comboeff)
    {
        if (comboeff >= 0 && comboeff < effects.Count)
        {
            Vector3 spawnPosittion = effectSpawn.position;
            if (combo == 4)
            {
                spawnPosittion = headSword.position;
            }
            GameObject slashVFX = Instantiate(effects[comboeff].vfx,spawnPosittion,effectSpawn.rotation);
            slashVFX.transform.Rotate(180, 0, 0);
            Destroy(slashVFX, 1f);
        }
        else
        {
            Debug.LogWarning("Combo index không hợp lệ!");
        }
    }
    
    public void SetFreeze()
    {
        playerController.freeze = false;
    }
}
