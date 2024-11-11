﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInfor : MonoBehaviour
{
    public int dame = 10;
    public float hp = 100f;
    public float hpcurrent;
    public bool isdead = false;
    public float fadeDuration = 2f;
    bool isCollision = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        hpcurrent = hp;
    }
    private void Update()
    {
        if (hpcurrent <= 0)
        {
            Die();
        }
    }
    public void EnemyTakeDame(int dame)
    {
        hpcurrent -= dame;
    }
    private void OnTriggerExit(Collider other)
    {

    }

    IEnumerator DisableCollider(Collider other, float delay)
    {
        yield return new WaitForSeconds(delay);
        isCollision = false;
        other.enabled = false;
    }
    public void Die()
    {
        if (isdead) return;

        isdead = true;
        animator.SetTrigger("Dead");

        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<AIEnemy>().enabled = false;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
