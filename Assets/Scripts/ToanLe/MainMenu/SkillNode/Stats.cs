using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public StatsData stats;
    private void Start()
    {
        stats.Set();
    }
}
