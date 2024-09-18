using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAtack : MonoBehaviour
{
    Animator ani;
    bool trigger;
    public int combo;
    public int combonum;
    public bool isAttack;
    public float comboTiming;
    public float comboDelay;

    Rigidbody rb;

    public float attackmove = 0.5f;
    PlayerController playerController;
    void Start()
    {
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
        Combo();
    }   

    public void Combo()
    {
        comboDelay -= Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && comboDelay < 0)
        {
            isAttack = true;
            playerController.freze = true;
            MoveForwardDuringAttack();
            ani.SetTrigger("Attack" + combo);
            comboDelay = comboTiming;
        }
        else if (Input.GetMouseButtonDown(0) && comboDelay > 0 && comboDelay < 0.5)
        {
            isAttack = true;
            playerController.freze = true;
            MoveForwardDuringAttack();
            combo++;
            if (combo > combonum)
            {
                combo = 1;
            }
            ani.SetTrigger("Attack" + combo);
            if (combo == 4)
            {
                //GameObject camObject = GameObject.Find("Camera");
                //CameraShaker shaker = camObject.GetComponent<CameraShaker>();
                //shaker.CameraShake(2f); // Rung camera trong 0.2s với độ mạnh 0.3
            }
            comboDelay = comboTiming;

        }
        else if(comboDelay<0 && !Input.GetMouseButton(0))
        {
            isAttack=false;
            playerController.freze = false;
        }
        if(comboDelay<0)
        {
            combo = 1;
        }
    }
    void MoveForwardDuringAttack()
    {
        // Di chuyển nhân vật khi tấn công
        Vector3 moveDirection = transform.forward * attackmove;
        rb.MovePosition(rb.position + moveDirection);
    }
}
