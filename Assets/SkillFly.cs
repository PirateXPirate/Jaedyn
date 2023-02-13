using DG.Tweening;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFly : SkillActivator
{

    CharacterMovement movement;
    Transform target;
    private void Start()
    {
        movement = GetComponent<CharacterMovement>();

    }

    protected override void Perform()
    {
        if (inPoint)
        {
            base.Perform();
            movement.ScriptDrivenInput = true;
            transform.DOMove(target.position, 1).SetEase(Ease.Linear).OnComplete(Complete);
            LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
            //LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
          //  LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
           // LevelManager.Instance.Players[0].GetComponent<CharacterOrientation3D>().enabled = false;
            LevelManager.Instance.Players[0].GetComponent<Collider>().enabled = false;
           // transform.DOLookAt(target.position, 1);
            inPoint = false;

        }
    }
    void Complete()
    {
        movement.ScriptDrivenInput = false;
        LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = true;
        LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = true;
        LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = true;
        LevelManager.Instance.Players[0].GetComponent<CharacterOrientation3D>().enabled = true;
        LevelManager.Instance.Players[0].GetComponent<Collider>().enabled = true;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
       
        if (other.tag.Equals("SkillPosition"))
        {
            inPoint = true;
            target = other.GetComponent<Warp>().Target;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

}
