using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTakeChest : MonoBehaviour
{
    public GameObject chestAnimation;
    public GameObject lightAnimation;
    public GameObject content;
    Animator animator;
    Animator animator1;
    public float animationDuration = 1.0f;

    private void Awake()
    {
        if (chestAnimation != null)
        {
            animator = chestAnimation.GetComponent<Animator>();
            animator1 = lightAnimation.GetComponent<Animator>();
        }
    }

    private void OnEnable()
    {
        if (chestAnimation != null && animator != null)
        {
            animator.Rebind();
            animator.Update(0); 
            animator1.Rebind();
            animator1.Update(0);
            chestAnimation.SetActive(true);
            lightAnimation.SetActive(true);
        }

        if (content != null)
        {
            content.SetActive(false);
        }

        StartCoroutine(PlayChestAnimation());
    }

    private IEnumerator PlayChestAnimation()
    {
        gameObject.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(animationDuration);
        gameObject.GetComponent<Button>().interactable = true;
        if (chestAnimation != null)
        {
            chestAnimation.SetActive(false);
            lightAnimation.SetActive(false);
        }

        if (content != null)
        {
            content.SetActive(true);
        }
    }
}
