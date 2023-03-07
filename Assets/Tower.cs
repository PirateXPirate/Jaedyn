using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
     public GameObject trunkObj;

    public int timeTochange;
    public int maxTime;
    public Material blueMat;
    public Renderer rend;
    public ActivateObject obj;
    public bool Complete = false;
    TowerManager manager;
   
    void Start()
    {
        manager = GetComponentInParent<TowerManager>();
        timeTochange = Random.Range(1, maxTime);
    }

    public void Perform()
    {
        timeTochange -= 1;
        if (timeTochange == 0)
        {
            Complete = true;
            rend.material = blueMat;
            if(obj)
            obj.Activate();
            if (manager)
                manager.CheckAll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
