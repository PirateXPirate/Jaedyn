using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : ActivateObject
{
    public bool AutoActivate;

    bool isMoving = false;
    public override void Activate()
    {
        if (isMoving) return;
        currentValue += 1;

        if (currentValue == targetPuzzleValue)
        {
            isMoving = true;
            transform.DOMove(targetTransform.position, time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
          
    }
    private void Start()
    {
        if (AutoActivate)
        {
            Activate();

        }
    }
}
