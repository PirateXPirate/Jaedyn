using MoreMountains.TopDownEngine;
using Suriyun.MCS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
	public bool canInteract = true;
   
    public virtual void PressJumpDown()
	{
		if(canInteract)
		InputManager.Instance.JumpButtonDown();
	}
	public virtual void PressJumpUp()
	{
		if (canInteract)
			InputManager.Instance.JumpButtonUp();
	}
	public virtual void PressActionDown()
	{
		if (canInteract)
			InputManager.Instance.ShootButtonDown();
	}

	public virtual void PressActionUp()
	{
		if (canInteract)
			InputManager.Instance.ShootButtonUp();
	}

	public virtual void PressFunctionDown()
	{
		if (canInteract)
			InputManager.Instance.CrouchButtonDown();
	}

	public virtual void PressFunctionUp()
	{
		if (canInteract)
			InputManager.Instance.CrouchButtonUp();
	}


	public virtual void PressChangeCharDown()
	{
		if (canInteract)
			InputManager.Instance.SwitchCharacterButtonDown();
	}
}
