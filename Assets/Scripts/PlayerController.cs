using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Thuộc tính nhân vật")]
    public float speed = 5f;
    public float jumpForce = 10f;


    private float animSpeed = 0.5f;
    Rigidbody rb;
    Animator animator;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
    }
    public void HandleMovement()
    {
        float ipVetical = Input.GetAxis("Vertical");
        float ipHorizontal = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(ipHorizontal, 0, ipVetical);
        Debug.Log(move);
        if(move!=Vector3.zero)
        {
            animator.SetFloat("Speed", animSpeed);
            transform.position = transform.position + move * speed * Time.deltaTime;
        }
        
    }
}
