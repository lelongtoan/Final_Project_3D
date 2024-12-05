using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailSkillNode : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI desText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] GameObject iconCoin;
    [SerializeField] GameObject iconDiamond;
    SkillNodeData data;
    private void Start()
    {
        iconCoin.SetActive(false);
        iconDiamond.SetActive(false);
    }
    private void Update()
    {
        if(data!=null)
        {
            if (data.gold != 0)
            {
                iconCoin.SetActive(true);
            }
            else
            {
                iconDiamond.SetActive(true);
            }
        }
    }
    public void Set(SkillNodeData data)
    {
        this.data = data;
        image.sprite = data.icon;
        nameText.text = data.nameSkillNode;
        desText.text = data.description;
        if (data.gold !=0)
        {
            priceText.text = data.gold + "";
            iconCoin.SetActive(true);
        }
        else
        {
            priceText.text = data.diamond + "";
            iconDiamond.SetActive(true);
        }
    }
    public void Upgrade()
    {
        if (data != null)
        {
            if (data.gold != 0)
            {
                if(true)
                {
                    //tru gold
                    data.state = SkillNodeState.Taked;
                }
            }
            else
            {
                if(true)
                {
                    //tru diamond
                    data.state = SkillNodeState.Taked;
                }
            }
        }
    }
}
