using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DummyAttackListener : MonoBehaviour
{
    [SerializeField] JDHealth playerHealth;

    [SerializeField] private Character dummyCharacter;
    private AIBrain dummyBrain;

    [SerializeField] private GameObject popupFrame;
    [SerializeField] private TextMeshProUGUI titleTextField;
    [SerializeField] private TextMeshProUGUI detailTextField;
    [SerializeField] private string titleText;
    [SerializeField] private string detailText;
    [SerializeField] private TouchInputManager controllerUi;
    [SerializeField] private GameObject activateObject;
    public AudioClip PopupSoundClip;
    bool ishow = false;
    void Start()
    {
        playerHealth.onRecieveAttack += onRecieveAttack;
        dummyBrain = dummyCharacter.GetComponent<AIBrain>();
    }

    private void onRecieveAttack(int currenthealth)
    {
        if (currenthealth == 60)
        {
            dummyBrain.ResetBrain();
            dummyBrain.BrainActive = false;

          
            MMAnimatorExtensions.UpdateAnimatorBoolIfExists(LevelManager.Instance.Players[0].GetComponentInChildren<Animator>(), "Walking", false);
            LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
            LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
            ShowPopup();
        }
    }

    private void OnDisable()
    {
        playerHealth.onRecieveAttack -= onRecieveAttack;
    }
    private void ShowPopup()
    {
        if (PopupSoundClip)
            Utils.soundManager.PlayFX(PopupSoundClip);

        controllerUi.canInteract= false;
        popupFrame.SetActive(true);
        titleTextField.text = titleText;
        detailTextField.text = detailText;
        ishow = true;



    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ishow)
            {
                gameObject.SetActive(false);
                activateObject.SetActive(true);
               // dummyBrain.BrainActive = true;
               //  gameObject.SetActive(false);
            }
        }
        
    }
}
