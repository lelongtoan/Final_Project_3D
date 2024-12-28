using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyInfor : MonoBehaviour
{
    public int enemyId;
    public int level = 1;
    public int coin = 1;
    public int exp = 1;
    public int dame = 10;
    public float hp = 100f;
    public float hpcurrent;
    public bool isdead = false;
    public float fadeDuration = 2f;
    bool isCollision = false;
    public Image hpBar;
    Animator animator;
    public GameObject canvas;
    public GameObject orb;
    public delegate void EnemyDeathHandler(GameObject enemy);
    public event EnemyDeathHandler OnEnemyDeath;
    private void Start()
    {
       
        if (PlayerPrefs.GetInt("EnemyStatus_" + enemyId, 1) == 0)
        {
            Destroy(gameObject); 
        }
        else
        {
            animator = GetComponent<Animator>();
            hpcurrent = hp;
        }
    }
    private void Update()
    {
        hpBar.GetComponent<Image>().fillAmount = hpcurrent / hp;
        if (hpcurrent <= 0)
        {
            Die();
        }
    }
    public void EnemyTakeDame(float dame)
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
        //PlayerPrefs.SetInt(enemyId, 1);
        PlayerPrefs.Save();
        animator.SetTrigger("Dead");
        Dropitem();
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<AIEnemy>().enabled = false;
        canvas.SetActive(false);

        PlayerPrefs.SetInt("EnemyStatus_" + enemyId, 1);
        PlayerPrefs.Save();

        if (OnEnemyDeath != null)
        {
            OnEnemyDeath.Invoke(gameObject);
        }

        Destroy(gameObject);
    }
    void Dropitem()
    {
        Vector3 sapwnpoint = transform.position + new Vector3(0, 1, 0);
        GameObject item = Instantiate(orb, sapwnpoint, Quaternion.identity);
        Drop dr = item.GetComponent<Drop>();
        ItemEnemyDrop itemDrop = GetComponent<ItemEnemyDrop>();
        itemDrop.Set(dr);
        dr.money = coin;
        dr.exp = exp;
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
        OnEnemyDeath = null;
    }
}
