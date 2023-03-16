using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpDoor : MonoBehaviour
{
    [SerializeField] WarpDoor targetWarpPoint;
    [SerializeField] AudioClip warpSound;
    public bool CanEnter;
    public bool waitForExit = false;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (CanEnter)
            {
                Utils.soundManager.PlayFX(warpSound);
                other.transform.position = targetWarpPoint.transform.position;
               // targetWarpPoint.CanEnter = true;
                CanEnter = false;
                waitForExit = false;
                targetWarpPoint.waitForExit = true;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (waitForExit)
            CanEnter = true;
        //targetWarpPoint.CanEnter = true;
      //  CanEnter = true;
    }
}
