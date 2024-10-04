using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{ 
    Animator animator;
    [Header("Skill 1")]
    public GameObject eff1;
    public float timingskill = 1f;
    public int exploCount = 3;
    public float size = 1;
    public float upsize = 1.5f;
    public float exploDistance = 2f;
    public Transform skillSpawm;
    public GameObject colliderSkill;
    PlayerInfor playerInfor;

    public float dame = 0f;




    protected Button skill1;
    //public Button skill2;
    //public Button skill3;
    PlayerController playerController;
    void Start()
    {
        playerInfor = GetComponent<PlayerInfor>();
        animator = GetComponent<Animator>();
        skill1 = GameObject.Find("Skill1").GetComponent<Button>();
        skill1.onClick.AddListener(SpawmSkill);
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }
    void SpawmSkill()
    {
        playerController.freeze = true;
        animator.SetTrigger("Skill 1");
    }

    private IEnumerator TriggerExplosion()
    {
        //float dame = playerInfor.dame;
        float a = dame;
        Vector3 exPositio = skillSpawm.position;
        Vector3 expDirection = skillSpawm.forward;
        float currentSize = size;
        for (int i = 0; i < exploCount; i++)
        {
            playerInfor.dame += 2 * (i + 1);
            GameObject explo = Instantiate(eff1, exPositio, Quaternion.identity);
            GameObject coliskill = Instantiate(colliderSkill, explo.transform.position,Quaternion.identity);
            explo.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
            coliskill.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
            currentSize *= upsize;
            exPositio += expDirection.normalized * exploDistance;
            //Destroy(coliskill, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
        playerInfor.dame = a ;
    }

}
