using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropToRiver : MonoBehaviour
{
    [SerializeField] Transform warpPosition;
    [SerializeField] AudioClip warpSound;
    [SerializeField] Character.FacingDirections directionAfterWarp;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (warpPosition != null)
            {
                if (warpSound)
                    Utils.soundManager.PlayFX(warpSound);
                other.transform.position = warpPosition.position;


                var orientation = LevelManager.Instance.Players[0].GetComponent<CharacterOrientation3D>();
                LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().Reset();
                Invoke("Reset", .5f);
                orientation.Face(directionAfterWarp);
                LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
                MMAnimatorExtensions.UpdateAnimatorBoolIfExists(LevelManager.Instance.Players[0].GetComponentInChildren<Animator>(), "Walking", false);
                if (LevelManager.Instance.Players[0].GetComponent<CharacterJump3D>())
                    LevelManager.Instance.Players[0].GetComponent<CharacterJump3D>().JumpStop();
                LevelManager.Instance.Players[0].MovementState.ChangeState(CharacterStates.MovementStates.Idle);
            }

        }
    }
    private void Reset()
    {
        LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = true;
    }
}
