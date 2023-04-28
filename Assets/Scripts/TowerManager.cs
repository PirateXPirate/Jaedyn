using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public Tower[] TowerList = new Tower[0];
    bool allComplete = false;
    public ActivateObject obj;
    public WarpDoor warpDoor;
    public ParticleSystem[] particle;
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
        {
            if(obj)
                obj.Activate();

            if (warpDoor)
            {
                warpDoor.CanEnter = true;
            }

            if (particle.Length>0)
            {
                foreach (var par in particle)
                {
                    ParticleSystem.MainModule settings = par.GetComponent<ParticleSystem>().main;
                    settings.startColor = new ParticleSystem.MinMaxGradient(Color.green);
                }
              
            }
        }
           
    }
}
