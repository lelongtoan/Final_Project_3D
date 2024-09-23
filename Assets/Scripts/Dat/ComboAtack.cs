using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class ComboAtack : MonoBehaviour
{
    public GameObject atackCollider;
    private bool isComboActive = false;
    Animator ani;
    bool trigger;
    public int combo;
    public int combonum;
    public bool isAttack;
    public float comboTiming;
    public float comboDelay;

    public Button attack;
    Rigidbody rb;

    public float attackmove = 0.1f;
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
            isAttack = true;
            atackCollider.SetActive(true);
            MoveForwardDuringAttack();
            ani.SetTrigger("Attack" + combo);
            comboDelay = comboTiming;
        }
        else if (isComboActive == true && comboDelay > 0 && comboDelay < 0.4)
        {
            isAttack = true;
            atackCollider.SetActive(true);
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
        //atackCollider.SetActive(false);
    }
    private void OnAtackClick()
    {
        if (!isComboActive)
        {
            playerController.freeze = true;
            isComboActive = true;
            Debug.Log(isComboActive);
            Combo();
            isComboActive = false;
            playerController.freeze = false;
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
        atackCollider.SetActive(false);
    }
}
