using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhenTrigger : MonoBehaviour
{
    public AudioClip audi;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Utils.soundManager.PlayFX(audi);
        }
    }
}
