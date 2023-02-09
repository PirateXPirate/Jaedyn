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

    bool isShowed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (!isShowed)
            {
                Character character = other.GetComponent<Character>();
                character.LinkedInputManager.InputDetectionActive = false;
                MMAnimatorExtensions.UpdateAnimatorBoolIfExists(character.GetComponentInChildren<Animator>(), "Walking", false);
                character.GetComponent<CharacterMovement>().enabled = false;
                character.GetComponent<TopDownController3D>().enabled = false;

                ShowPopup();
            }
         
        }
       
    }

    private void ShowPopup()
    {
        popupFrame.SetActive(true);
        titleTextField.text = titleText;
        detailTextField.text = detailText;
        isShowed = true;
       
    }
}
