using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ListTake")]
public class ListTakeData : ScriptableObject
{
    public int point;
    public List<TakedData> takedDatas = new List<TakedData>();
    public bool isTake;
}
