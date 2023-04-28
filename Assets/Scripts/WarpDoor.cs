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
                Debug.Log(name);
                Utils.soundManager.PlayFX(warpSound);
                other.transform.position = warpPosition.position;
               // targetWarpPoint.CanEnter = true;
                CanEnter = false;
                waitForExit = false;
                targetWarpPoint.waitForExit = true;
                Invoke("Reset", 2);
                var orientation = LevelManager.Instance.Players[0].GetComponent<CharacterOrientation3D>();
                LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().Reset();
               
                orientation.Face(directionAfterWarp);
                LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
                MMAnimatorExtensions.UpdateAnimatorBoolIfExists(LevelManager.Instance.Players[0].GetComponentInChildren<Animator>(), "Walking", false);
            }
            
        }
    }
    private void Reset()
    {
        CanEnter = true;
        LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (waitForExit)
            CanEnter = true;
        //targetWarpPoint.CanEnter = true;
      //  CanEnter = true;
    }
}
