using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpDoor : MonoBehaviour
{
    [SerializeField] WarpDoor targetWarpPoint;

    [SerializeField] Transform warpPosition;
    [SerializeField] AudioClip warpSound;
    [SerializeField] Character.FacingDirections directionAfterWarp;
    public bool CanEnter;
    public bool waitForExit = false;
    void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (CanEnter && warpPosition!=null)
            {
                if (warpSound)
                    Utils.soundManager.PlayFX(warpSound);
                other.transform.position = warpPosition.position;
               
                CanEnter = false;
               // waitForExit = false;
                if(targetWarpPoint)
                targetWarpPoint.waitForExit = true;
                targetWarpPoint.CanEnter = false;
                var orientation = LevelManager.Instance.Players[0].GetComponent<CharacterOrientation3D>();
                orientation.Face(directionAfterWarp);
                Invoke("Reset", 1);
               
                LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().Reset();
               
              
                LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
                MMAnimatorExtensions.UpdateAnimatorBoolIfExists(LevelManager.Instance.Players[0].GetComponentInChildren<Animator>(), "Walking", false);
            }
            
        }
    }
    private void Reset()
    {
       // CanEnter = true;
        LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = true;
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (waitForExit)
        {
            targetWarpPoint.CanEnter = false;
            CanEnter = true;
        }
        //targetWarpPoint.CanEnter = true;
      //  CanEnter = true;
    }
}
