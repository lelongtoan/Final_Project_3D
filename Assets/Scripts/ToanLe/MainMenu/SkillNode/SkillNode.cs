using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillNode : MonoBehaviour
{
    [SerializeField] Image iconNode;
    [SerializeField] TextMeshProUGUI textState;
    public SkillNodeData skillNodeData;
    public DetailSkillNode skillNode;
    private void Update()
    {
        Set();
    }
    public void Set()
    {
        iconNode.sprite = skillNodeData.icon;
        if (SetState() && skillNodeData.state == SkillNodeState.Lock)
        {
            skillNodeData.state = SkillNodeState.Open;
        }
        if (skillNodeData.state == SkillNodeState.Lock)
        {
            textState.text = "Lock";
            textState.color = Color.red;
        }
        else if (skillNodeData.state == SkillNodeState.Open)
        {
            textState.text = "Upgrade";
            textState.color = Color.white;
        }
        else
        {
            textState.text = "Upgraded";
            textState.color = Color.green;
        }
    }
    public bool SetState()
    {
        foreach(var i in skillNodeData.listNextSkillNode)
        {
            if(i.state != SkillNodeState.Taked)
            {
                return false;
            }
        }
        return true;
    }
    public void Upgrade()
    {
        if(skillNodeData.state == SkillNodeState.Open)
        {
            MainMenuManager.Instance.SetSkillNodeDetail();
            skillNode = FindAnyObjectByType<DetailSkillNode>();
            skillNode.Set(skillNodeData);
        }
    }
}
