using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSlide : MonoBehaviour
{
    public AudioClip rightSound;
    public GameObject SlideToShow;
    public void Perform()
    {

            //Complete = true;
            Utils.soundManager.PlayFX(rightSound);
           // transitionParticle.SetActive(true);
            Invoke("CompleteFunc", .5f);

    }
    void CompleteFunc()
    {
        SlideToShow.SetActive(true);
    }
}
