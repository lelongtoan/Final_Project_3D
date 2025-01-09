using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceAndOpen : MonoBehaviour
{
    public ListTakeData listData;
    public InforMainMenu inforMain;
    public GameObject content;
    public GameObject prefabA;
    public GameObject goB;

    [SerializeField] Button button;
    private void Start()
    {
        button.onClick.AddListener(SetTakeItem);
    }
    void Update()
    {
        if (listData.isTake)
        {
            goB.SetActive(true);
            foreach (TakedData data in listData.takedDatas)
            {
                if(data.point<= listData.point)
                {
                    GameObject instance = Instantiate(prefabA);
                    instance.transform.SetParent(content.transform);
                    instance.gameObject.GetComponent<ItemTake>().Set(data.sprite, data.quantity);

                    if(data.stateTake == StateTake.Gold)
                    {
                        inforMain.money += data.quantity;
                    }
                    else if(data.stateTake == StateTake.Diamond)
                    {
                        inforMain.diamond += data.quantity;
                    }
                    else if(data.stateTake == StateTake.IronKey)
                    {
                        inforMain.ironKey += data.quantity;
                    }
                    else if(data.stateTake == StateTake.SilverKey)
                    {
                        inforMain.silverKey += data.quantity;
                    }
                }
            }

            listData.isTake = false;
        }
    }

    public void SetTakeItem()
    {
        goB.SetActive(false);
    }
}
