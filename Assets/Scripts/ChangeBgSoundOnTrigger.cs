using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBgSoundOnTrigger : MonoBehaviour
{
    public AudioClip audi;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Utils.soundManager.PlayLoop(audi);
        }
    }
}
