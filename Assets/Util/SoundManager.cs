using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource loop;
    public AudioSource fx;

    public void Start() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            Utils.soundManager = this;
            Utils.soundManager.loop.volume = PlayerPrefs.GetFloat("loop",.5f);
            Utils.soundManager.fx.volume = PlayerPrefs.GetFloat("fx", .5f);
        }
        DontDestroyOnLoad(this);

        
        
    
    }

    public void PlayLoop(AudioClip clip) {
        loop.clip = clip;
        loop.Play();
    }

    public void PlayFX(AudioClip clip,bool append =false) {
        if (fx.isPlaying && !append)
            fx.Stop();
        fx.PlayOneShot(clip);
    }
}
