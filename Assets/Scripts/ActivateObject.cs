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
    public GameObject activateParticleObject;
    public AudioClip activateSound;
    public void Activate()
    {
        currentValue += 1;
        if (currentValue == targetPuzzleValue)
        {
            activateParticleObject.SetActive(true);
            if(activateSound)
            Utils.soundManager.PlayFX(activateSound);
            transform.DOMove(targetTransform.position, time);
        }
    }


}
