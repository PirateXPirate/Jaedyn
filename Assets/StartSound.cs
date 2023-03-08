using Suriyun.MCS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSound : MonoBehaviour
{
    [SerializeField] private TouchInputManager controllerUi;

    [SerializeField] private AudioClip startAudioClip;

    [SerializeField] private AudioClip mapBgAudio;

    [SerializeField] private AudioClip startWalkAudioClip;

    AnalogStick analogStick;
    public bool isLock;
    void Start()
    {
        if(Utils.soundManager)
        Utils.soundManager.PlayLoop(mapBgAudio);

        if (isLock)
        {
            controllerUi.canInteract = false;
            analogStick = controllerUi.GetComponentInChildren<AnalogStick>();
            analogStick.canInteract = false;

            analogStick.onPointerDown.AddListener(delegate { OnStartWalk(0); });
            Utils.soundManager.PlayFX(startAudioClip);



            Invoke("WaitClipEnd", startAudioClip.length);
        }
      
    }
    private void OnEnable()
    {
       
    }
    private int OnStartWalk(int index)
    {
        if (analogStick.canInteract)
        {
            Utils.soundManager.PlayFX(startWalkAudioClip);
            analogStick.onPointerDown.RemoveAllListeners();
        }
       
        return index;
    }
    private void OnDisable()
    {
        if(analogStick)
        analogStick.onPointerDown.RemoveAllListeners();
    }

    private void WaitClipEnd()
    {
        controllerUi.canInteract = true;
        controllerUi.GetComponentInChildren<AnalogStick>().canInteract = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
