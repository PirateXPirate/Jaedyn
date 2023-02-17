using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActivator : MonoBehaviour
{
    protected bool inPoint = false;
    protected  CharacterMovement movement;

    [SerializeField] protected SkillUiManager skillUiManager;
    [SerializeField] protected float coolDown;
    protected virtual  void Start()
    {
        movement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (InputManager.Instance.JumpButton.State.CurrentState == MoreMountains.Tools.MMInput.ButtonStates.ButtonDown)
        {
            Perform();
        }
    }

    protected virtual void Perform()
    {
      

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("SkillPosition"))
        {           
            inPoint = true;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("SkillPosition"))
        {
            inPoint = false;
        }
    }
}
