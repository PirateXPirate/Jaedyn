using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSkill : MonoBehaviour
{
    protected bool inPoint = false;
    protected bool inTower = false;
    protected bool inSlide = false;
    protected bool inChest = false;
    protected bool inFirefly = false;
    protected CharacterMovement movement;

    private TutorialMarker currentMaker;

    private Tower currentTower;
    private ActivateSlide currentSlide;

    private ChestBox currentChest;

    private FireflyManager currentFirefly;

    [SerializeField] private GameObject buttonEffect;
    void Start()
    {
        movement = GetComponent<CharacterMovement>();

        CharacterSwitchManager.SwitchCharEvent += OnSwitchChar;
    }

    private void OnSwitchChar()
    {
        inSlide = false;
        currentSlide = null;
    }

    private void OnDestroy()
    {
        CharacterSwitchManager.SwitchCharEvent -= OnSwitchChar;

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
            return;
            Character character = GetComponent<Character>();
            character.LinkedInputManager.InputDetectionActive = false;
            character.GetComponent<CharacterMovement>().enabled = false;
            character.GetComponent<TopDownController3D>().enabled = false;

            if(currentMaker)
            currentMaker.ShowPopup();
            inPoint = false;

     
        }

        if (inTower)
        {
            if (currentTower == null) return;
            if (currentTower.trunkObj)
                if (currentTower.trunkObj.activeSelf) return;
            if (currentTower)
                currentTower.Perform();

        }

        if (inFirefly)
        {
            if (currentFirefly == null) return;
           
            if (currentFirefly)
                currentFirefly.Perform();

        }

        if (inChest)
        {
            currentChest.Perform();

        }
        if (inSlide)
        {
            if (currentSlide)
                currentSlide.Perform();

        }
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("NotiJump0") || other.tag.Equals("SkillPosition"))
        {
            currentMaker = other.GetComponent<TutorialMarker>();
            if (currentMaker == null) return;
            buttonEffect.SetActive(true);
            inPoint = true;
           
            if (currentMaker == null) return;
            if (currentMaker.Activated) return;
            currentMaker.Activated = true;

            if (currentMaker.TriggerSoundClip)
            {
                Debug.Log(currentMaker.TriggerSoundClip.name);
                Utils.soundManager.PlayFX(currentMaker.TriggerSoundClip);
            }
        }

        if (other.tag.Equals("Tower"))
        {
            inTower = true;
            currentTower = other.GetComponent<Tower>();
        }

        if (other.tag.Equals("Chest"))
        {
            inChest = true;
            currentChest = other.GetComponent<ChestBox>();
        }

        if (other.tag.Equals("Firefly"))
        {
            inFirefly = true;
            currentFirefly = other.GetComponent<FireflyManager>();
        }
        if (other.tag.Equals("Slide"))
        {
            inSlide = true;
            currentSlide = other.GetComponent<ActivateSlide>();
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
        if (other.tag.Equals("Tower"))
        {
            inTower = false;
            currentTower = null;
        }
        if (other.tag.Equals("Chest"))
        {
            inChest = false;
            currentChest = null;
        }
        if (other.tag.Equals("Firefly"))
        {
            inFirefly = false;
            currentFirefly = null;
        }
        if (other.tag.Equals("Slide"))
        {
            inSlide = false;
            currentSlide = null;
        }
    }
}
