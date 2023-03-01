using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSkill : MonoBehaviour
{
    protected bool inPoint = false;
    protected CharacterMovement movement;

    private TutorialMarker currentMaker;

   [SerializeField] private GameObject buttonEffect;
    void Start()
    {
        movement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.CrouchButton.State.CurrentState == MoreMountains.Tools.MMInput.ButtonStates.ButtonDown)
        {
            Perform();
        }
    }

    private void Perform()
    {
        if (inPoint)
        {
            Character character = GetComponent<Character>();
            character.LinkedInputManager.InputDetectionActive = false;
            character.GetComponent<CharacterMovement>().enabled = false;
            character.GetComponent<TopDownController3D>().enabled = false;
            currentMaker.ShowPopup();
            inPoint = false;
        }
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("NotiJump0") || other.tag.Equals("SkillPosition"))
        {
            buttonEffect.SetActive(true);
            inPoint = true;
            currentMaker = other.GetComponent<TutorialMarker>();
            if (currentMaker == null) return;
            if (currentMaker.Activated) return;
            currentMaker.Activated = true;
           
            if (currentMaker.TriggerSoundClip)
            {
                Debug.Log(currentMaker.TriggerSoundClip.name);
                Utils.soundManager.PlayFX(currentMaker.TriggerSoundClip);
            }
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("NotiJump0") || other.tag.Equals("SkillPosition"))
        {
            buttonEffect.SetActive(false);
            inPoint = false;
            currentMaker = null; 
        }
    }
}
