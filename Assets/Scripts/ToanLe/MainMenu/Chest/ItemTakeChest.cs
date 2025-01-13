using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemTakeChest : MonoBehaviour
{
    [SerializeField] private GameObject itemTakePanel;
    [SerializeField] private GameObject chestAnimation;
    [SerializeField] private GameObject lightAnimation;
    [SerializeField] private GameObject content;
    [SerializeField] private float animationDuration = 1.0f;

    private Animator chestAnimator;
    private Animator lightAnimator;
    private Button button;
    private int clickCount;

    private void Awake()
    {
        if (chestAnimation != null)
        {
            chestAnimator = chestAnimation.GetComponent<Animator>();
        }

        if (lightAnimation != null)
        {
            lightAnimator = lightAnimation.GetComponent<Animator>();
        }

        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Button component missing on GameObject.");
        }
    }

    private void OnEnable()
    {
        ResetState();
        if (button != null)
        {
            button.interactable = false;
            StartCoroutine(EnableButtonAfterDelay(1.0f));
        }
    }

    public void ClickOpen()
    {
        if (clickCount > 0)
        {
            CloseChest();
        }
        else
        {
            OpenChest();
        }
    }

    private void ResetState()
    {
        clickCount = 0;

        if (chestAnimation != null)
        {
            chestAnimator?.Rebind();
            chestAnimator?.Update(0);
            chestAnimation.SetActive(true);
        }

        if (lightAnimation != null)
        {
            lightAnimator?.Rebind();
            lightAnimator?.Update(0);
            lightAnimation.SetActive(true);
        }

        if (content != null)
        {
            content.SetActive(false);
        }
    }

    private void OpenChest()
    {
        clickCount++;

        if (chestAnimator != null && lightAnimator != null)
        {
            chestAnimator.SetBool("IsShow", true);
            StartCoroutine(PlayChestAnimation());
        }
    }

    private void CloseChest()
    {
        clickCount = 0;

        if (chestAnimator != null && lightAnimator != null)
        {
            chestAnimator.SetBool("SetPos", false);
            lightAnimator.SetBool("SetPos", false);
        }

        chestAnimation?.SetActive(false);
        lightAnimation?.SetActive(false);
        itemTakePanel?.SetActive(false);

        ResetState();
    }

    private IEnumerator PlayChestAnimation()
    {
        if (button != null)
        {
            button.interactable = false;
        }

        yield return new WaitForSeconds(animationDuration);

        if (button != null)
        {
            button.interactable = true;
        }

        if (chestAnimator != null && lightAnimator != null)
        {
            chestAnimator.SetBool("IsShow", false);
            chestAnimator.SetBool("SetPos", true);
            lightAnimator.SetBool("SetPos", true);
        }

        yield return new WaitForSeconds(animationDuration);

        if (content != null)
        {
            content.SetActive(true);
            ProcessRewards();
        }
    }

    private IEnumerator EnableButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (button != null)
        {
            button.interactable = true;
        }
    }

    private void ProcessRewards()
    {
        foreach (Transform child in content.transform)
        {
            ItemTake itemTake = child.GetComponent<ItemTake>();
            if (itemTake != null)
            {
                MainMenuInstance.instance.inforMenu.money += itemTake.gold;
                MainMenuInstance.instance.inforMenu.diamond += itemTake.diamond;
            }
        }
    }
}
