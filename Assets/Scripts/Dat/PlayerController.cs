using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Thuộc tính nhân vật")]
    public float speed = 5f;
    public float jumpForce = 10f;


    public bool freze=false;//Biến để cấm nhân vật di chuyển
    private float animSpeed = 0f;
    public bool isRun = false;
    Rigidbody rb;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isRun == false)
            {
                isRun = true;
                speed = 8f;
            }
            else if (isRun == true)
            {
                animator.SetBool("IsRun", false);
                isRun = false;
                speed = 5f;
            }
        }
        Debug.Log(isRun);
        if (freze == false)
        {
            HandleMovement();
        }
    }
    public void HandleMovement()
    {
        float ipVetical = Input.GetAxis("Vertical");
        float ipHorizontal = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(ipHorizontal, 0, ipVetical);
        RotateCharacter(move);
        Debug.Log(move);
        if (move != Vector3.zero && isRun == false)
        {
            animSpeed = 1;
        }
        if (move != Vector3.zero && isRun == true)
        {
            animSpeed = 2;
            animator.SetBool("IsRun", true);
        }
        animator.SetFloat("Speed", animSpeed);
        transform.position = transform.position + move * speed * Time.deltaTime;
        if (move == Vector3.zero)
        {
            animator.SetFloat("Speed", 0);
        }
    }
    // Hàm xoay
    public void RotateCharacter(Vector3 playerMovementInput)
    {
        Vector3 lookDirection = playerMovementInput;
        lookDirection.y = 0f;
        if (lookDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = rotation;
        }
    }
}
