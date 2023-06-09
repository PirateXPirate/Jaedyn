using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TutorialMarker : MonoBehaviour
{
    [SerializeField] private TouchInputManager controllerUi;
    [SerializeField] private GameObject popupFrame;
    [SerializeField] private TextMeshProUGUI titleTextField;
    [SerializeField] private TextMeshProUGUI detailTextField;
    [SerializeField] private string titleText;
    [SerializeField] private string detailText;

    public AudioClip PopupSoundClip;

    public AudioClip TriggerSoundClip;

    public bool Activated = false;

    bool isShowed = false;
    bool canShow = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (!canShow) return;
            if (!isShowed)
            {
                Character character = other.GetComponent<Character>();
                character.LinkedInputManager.InputDetectionActive = false;
                character.GetComponent<CharacterMovement>().enabled = false;
                character.GetComponent<TopDownController3D>().enabled = false;
                   ShowPopup();
                canShow = false;
                Invoke("ResetCanShow",20);
            }
         
        }
       
    }
    void ResetCanShow()
    {
        canShow = true;
        isShowed = false;
    }
    public void ShowPopup()
    {
        if (PopupSoundClip)
            Utils.soundManager.PlayFX(PopupSoundClip);
        popupFrame.SetActive(true);

        controllerUi.canInteract = false;
        titleTextField.text = titleText;
        detailTextField.text = detailText;
        isShowed = true;
       
    }
}
