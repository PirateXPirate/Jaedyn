using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource loop;
    public AudioSource fx;

    public void Start() {
        DontDestroyOnLoad(this);
        Utils.soundManager = this;
    }

    public void PlayLoop(AudioClip clip) {
        loop.clip = clip;
        loop.Play();
    }

    public void PlayFX(AudioClip clip) {
        fx.PlayOneShot(clip);
    }
}
