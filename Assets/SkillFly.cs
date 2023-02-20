using DG.Tweening;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFly : SkillActivator
{
    Transform target;

  

    protected override void Perform()
    {
        skillUiManager.SetSkillCooldown(coolDown);
        if (inPoint)
        {
            base.Perform();
            if (skillSound)
                Utils.soundManager.PlayFX(skillSound);
            movement.ScriptDrivenInput = true;
            transform.DOMove(target.position, 1).SetEase(Ease.Linear).OnComplete(Complete);
            LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
            LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
            LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
            LevelManager.Instance.Players[0].GetComponent<CharacterOrientation3D>().enabled = false;
            LevelManager.Instance.Players[0].GetComponent<Collider>().enabled = false;
            transform.DOLookAt(target.position, 1);
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
       // base.OnTriggerEnter(other);
       
        if (other.tag.Equals("Fly"))
        {
            inPoint = true;
            if (other.GetComponent<Warp>() == null) return;
            target = other.GetComponent<Warp>().Target;

        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (other.tag.Equals("Fly"))
        {
            inPoint = false;
        }
    }

}
