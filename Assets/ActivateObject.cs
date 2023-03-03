using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public Transform targetTransform;
    public float time;
    public void Activate()
    {
        transform.DOMove(targetTransform.position, time);
    }
}
