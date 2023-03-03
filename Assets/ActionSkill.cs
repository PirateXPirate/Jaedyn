using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSkill : MonoBehaviour
{
    protected bool inPoint = false;
    protected bool inTower = false;
    protected bool inChest = false;
    protected CharacterMovement movement;

    private TutorialMarker currentMaker;

    private Tower currentTower;

    private ChestBox currentChest;

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

        if (inTower)
        {
            if (currentTower == null) return;
            if (currentTower.trunkObj.activeSelf) return;

            currentTower.Perform();

        }

        if (inChest)
        {
            currentChest.Perform();

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
    }
}
