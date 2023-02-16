using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
	public virtual void PressJumpDown()
	{
		InputManager.Instance.JumpButtonDown();
	}
	public virtual void PressJumpUp()
	{
		InputManager.Instance.JumpButtonUp();
	}
	public virtual void PressActionDown()
	{
		InputManager.Instance.ShootButtonDown();
	}

	public virtual void PressActionUp()
	{
		InputManager.Instance.ShootButtonUp();
	}

	public virtual void PressFunctionDown()
	{
		InputManager.Instance.CrouchButtonDown();
	}

	public virtual void PressFunctionUp()
	{
		InputManager.Instance.CrouchButtonUp();
	}


	public virtual void PressChangeCharDown()
	{
		InputManager.Instance.SwitchCharacterButtonDown();
	}
}
