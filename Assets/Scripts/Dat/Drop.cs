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
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
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
            CollectExperience();
        }
    }
    void CollectExperience()
    {
        PlayerInfor inf = player.GetComponent<PlayerInfor>();
        inf.GetMoney(money);
        inf.GetExp(exp);
        //gọi thêm hàm item vô dây
        Destroy(gameObject);
    }
}
