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
    public void Set(SkillNodeData data)
    {
        iconCoin.SetActive(false);
        iconDiamond.SetActive(false);
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
                if(data.gold <= MainMenuInstance.instance.inforMenu.money)
                {
                    MainMenuInstance.instance.inforMenu.money -= data.gold;
                    data.state = SkillNodeState.Taked;
                }
            }
            else
            {
                if(data.diamond <= MainMenuInstance.instance.inforMenu.money)
                {
                    MainMenuInstance.instance.inforMenu.diamond -= data.diamond;
                    data.state = SkillNodeState.Taked;
                }
            }
        }
    }
}
