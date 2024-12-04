using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public Transform player;
    public float speed = 4f;
    public float followDistans = 4f;
    public int money;
    public int exp;
    public List<ItemSlot> itemSlots;
    SoundEffect soundEffect;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        soundEffect = FindObjectOfType<SoundEffect>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < followDistans)
        {
            MoveTowardsPlayer();
        }
    }
    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (transform.position == player.position)
        {
            soundEffect.PlaySound("Drop");
            CollectExperience();
        }
    }
    void CollectExperience()
    {
        PlayerInfor inf = player.GetComponent<PlayerInfor>();
        inf.GetMoney(money);
        inf.GetExp(exp);
        AddItem(itemSlots);
        Destroy(gameObject);
    }
    void AddItem(List<ItemSlot> itemSlots)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if(GameInstance.instance.itemContainer.CheckFull(itemSlots[i].item))
            {
                GameInstance.instance.itemContainer.Add(itemSlots[i].item, itemSlots[i].count);
                GameInstance.instance.pickUpItem.ShowPickUp(itemSlots[i]);
            }
        }
    }
}
