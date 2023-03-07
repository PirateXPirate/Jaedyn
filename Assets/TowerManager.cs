using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public Tower[] TowerList = new Tower[0];
    bool allComplete = false;
    public ActivateObject obj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void CheckAll()
    {
        allComplete = true;
        foreach (var tower in TowerList)
        {
            if (!tower.Complete)
                allComplete = false;
        }

        if (allComplete)
            obj.Activate();
    }
}
