using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public static Quest instance { get; set; }
    [SerializeField] public TextMeshProUGUI questText;
    [SerializeField] public TextMeshProUGUI numberTextCheck;
    [SerializeField] public GameObject take;
    [SerializeField] public GameObject completed;

    public Item itemOpen;

    public int numberInt;
    public int numberComplete;
    public string questString;
    public bool completedBool;

    public int questId;
    public bool questShow;

    public Item itemCheck;
    [SerializeField] QuestManager questManager;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        numberInt = 0;
        questText.text = questString;
        numberTextCheck.text = numberInt + " / " + numberComplete;
    }
    private void Update()
    {
        if (completedBool)
        {
            numberTextCheck.enabled = false;
            completed.SetActive(true);
        }
        numberTextCheck.text = numberInt.ToString();
    }
    public void CheckItem(Item item, int count)
    {
        if (itemCheck != null)
            if (item == itemCheck)
            {
                numberInt += count;
                if (numberInt == numberComplete)
                {
                    item.canShow = true;
                    completedBool = true;
                    questManager.UpdateQuest();
                }
            }
    }
    public void CheckEnemy(Item item, int count)
    {
        if (itemCheck != null)
            if (item == itemCheck)
            {
                numberInt += count;
                if (numberInt == numberComplete)
                {
                    item.canShow = true;
                    completedBool = true;
                    questManager.UpdateQuest();
                }
            }
    }
}
