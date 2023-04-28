using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UseItemListener : MonoBehaviour
{
    [SerializeField] private TouchInputManager controllerUi;
    [SerializeField] PowerUpManager powerUpManager;
  
    private bool hpUsed = false;
    private bool ressistanceUsed = false;

    [SerializeField] private GameObject popupFrame;
    [SerializeField] private TextMeshProUGUI titleTextField;
    [SerializeField] private TextMeshProUGUI detailTextField;
    [SerializeField] private string titleText;
    [SerializeField] private string detailText;


    [SerializeField] private GameObject resetBotObject;
    public AudioClip PopupSoundClip;
    [SerializeField] private GameObject potionUiEffect;
    void Start()
    {
        powerUpManager.onUseHPPotion += onUseHP;
        powerUpManager.onUseRessistancePotion += onResistanceUse;
    
    }

    private void onResistanceUse()
    {
        ressistanceUsed = true;
        CheckItemUsed();
    }

    private void onUseHP()
    {
        hpUsed = true;
        CheckItemUsed();
    }
    private void CheckItemUsed()
    {
        if (hpUsed && ressistanceUsed)
        {
            powerUpManager.onUseHPPotion -= onUseHP;
            powerUpManager.onUseRessistancePotion -= onResistanceUse;
            ShowPopup();
            potionUiEffect.SetActive(false);
        }
    }

    private void ShowPopup()
    {
        if (PopupSoundClip)
            Utils.soundManager.PlayFX(PopupSoundClip);
        controllerUi.canInteract = false;
        gameObject.SetActive(false);
        popupFrame.SetActive(true);
        titleTextField.text = titleText;
        detailTextField.text = detailText;
        popupFrame.GetComponent<ClickToCloseScript>().ActivateObject = resetBotObject;

    }
  

    private void OnDisable()
    {
        powerUpManager.onUseHPPotion -= onUseHP;
        powerUpManager.onUseRessistancePotion -= onResistanceUse;
    }
}
