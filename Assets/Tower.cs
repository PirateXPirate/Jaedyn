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
   
    void Start()
    {
        timeTochange = Random.Range(1, maxTime);
    }

    public void Perform()
    {
        timeTochange -= 1;
        if (timeTochange == 0)
        {
            rend.material = blueMat;
            obj.Activate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
