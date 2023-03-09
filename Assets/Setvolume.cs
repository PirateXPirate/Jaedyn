using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setvolume : MonoBehaviour
{
    float currentVolume;
    private void OnEnable()
    {
        if (Utils.soundManager)
        {
            currentVolume = Utils.soundManager.loop.volume;
            GetComponent<AudioSource>().volume = currentVolume;
        }
    }
}
