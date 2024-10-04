using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{ 
    Animator animator;
    protected Button skill1;
    //public Button skill2;
    //public Button skill3;
    PlayerController playerController;
    void Start()
    {
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
}
