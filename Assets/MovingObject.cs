using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : ActivateObject
{
    public bool AutoActivate;
    public override void Activate()
    {
        transform.DOMove(targetTransform.position, time).SetLoops(-1,LoopType.Yoyo);
    }
    private void Start()
    {
        if (AutoActivate)
        {
            Activate();

        }
    }
}
