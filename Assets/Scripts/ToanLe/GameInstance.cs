using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;

    private void Awake()
    {
        instance = this;

    }

    public ItemContainer itemContainer;
}
