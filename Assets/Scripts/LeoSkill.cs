using DG.Tweening;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeoSkill : SkillActivator
{
    Transform targetObject;
    Transform targetPosition;
    ActivateObject activateObject;
    MoveBoxManager manager;
    CharacterOrientation3D rotation;
    TutorialMarker marker;
    bool isDrop = true;
    bool isReset = false;


    protected override void Perform()
    {
        skillUiManager.SetSkillCooldown(coolDown);
       
        if (inPoint)
        {
            if (marker)
            {
                marker.ShowPopup();
                marker = null;
                return;
            }
           
            var checkCollide = targetObject.GetComponent<ChekcCollide>();
            if (checkCollide)
            {
                if (checkCollide.hitObstacle != null)
                {
                    if (checkCollide.hitObstacle.activeSelf) return;
                }

            }
            base.Perform();
            movement.ScriptDrivenInput = true;
            targetObject.DOMove(targetPosition.position, 1).SetEase(Ease.Linear).OnComplete(Complete);
            LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
            LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
            LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
            LevelManager.Instance.Players[0].GetComponent<CharacterOrientation3D>().enabled = false;
            targetObject.transform.position = new Vector3(targetObject.position.x, transform.position.y, targetObject.position.z);
            transform.DOLookAt(targetObject.position, .5f);

            inPoint = false;
        }
    }
    void Complete()
    {
        if (activateObject)
            activateObject.Activate();

        if (manager)
            manager.OnMoveComplete();

        if (targetObject.GetComponent<Rigidbody>())
        {

            targetObject.GetComponent<Rigidbody>().isKinematic = !isDrop;

            if (isReset)
            {
                Destroy(targetObject.GetComponent<Collider>());
                targetObject.gameObject.AddComponent<BoxCollider>();
            }
        }
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
            if (other.GetComponent<MoveObject>() == null) return;
            targetObject = other.GetComponent<MoveObject>().Target;
            targetPosition = other.GetComponent<MoveObject>().TargetPosition;
            activateObject = other.GetComponent<MoveObject>().TargetActivateObject;
            manager  = other.GetComponent<MoveObject>().moveManager; 
            isDrop = other.GetComponent<MoveObject>().isDrop;
            isReset = other.GetComponent<MoveObject>().isReset;
            //  other.GetComponent<Collider>().enabled = false;
        }

        if (other.tag.Equals("Popup"))
        {
            inPoint = true;
            marker = other.GetComponent<TutorialMarker>();
          
        }
    }
}
