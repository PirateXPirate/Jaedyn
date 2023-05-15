using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public Transform targetTransform;
    public float time;

    public int targetPuzzleValue = 1;
    int currentValue = 0;
    public void Activate()
    {
        currentValue += 1;
        if(currentValue == targetPuzzleValue)
             transform.DOMove(targetTransform.position, time);
    }


}
