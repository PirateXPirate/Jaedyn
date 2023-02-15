using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TutorialMarker : MonoBehaviour
{
    [SerializeField] private GameObject popupFrame;
    [SerializeField] private TextMeshProUGUI titleTextField;
    [SerializeField] private TextMeshProUGUI detailTextField;
    [SerializeField] private string titleText;
    [SerializeField] private string detailText;

    public AudioClip PopupSoundClip;

    public AudioClip TriggerSoundClip;

    public bool Activated = false;

    bool isShowed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            return;
            if (!isShowed)
            {
             //   ShowPopup();
            }
         
        }
       
    }

    public void ShowPopup()
    {
        if (PopupSoundClip)
            Utils.soundManager.PlayFX(PopupSoundClip);
        popupFrame.SetActive(true);
        titleTextField.text = titleText;
        detailTextField.text = detailText;
        isShowed = true;
       
    }
}
