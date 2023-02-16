using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
	/// <summary>
	/// Add this class to a character so it can use weapons
	/// Note that this component will trigger animations (if their parameter is present in the Animator), based on 
	/// the current weapon's Animations
	/// Animator parameters : defined from the Weapon's inspector
	/// </summary>
	[AddComponentMenu("TopDown Engine/Character/Abilities/Character Handle Secondary Weapon")]
	public class CharacterHandleSecondaryWeapon : CharacterHandleWeapon
	{
		/// the ID / index of this CharacterHandleWeapon. This will be used to determine what handle weapon ability should equip a weapon.
		/// If you create more Handle Weapon abilities, make sure to override and increment this  
		public override int HandleWeaponID { get { return 2; } }
        
		/// <summary>
		/// Gets input and triggers methods based on what's been pressed
		/// </summary>
		protected override void HandleInput()
		{
			if (!AbilityAuthorized
			    || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal)
			    || (CurrentWeapon == null))
			{
				return;
			}

			bool inputAuthorized = true;
			if (CurrentWeapon != null)
			{
				inputAuthorized = CurrentWeapon.InputAuthorized;
			}
			
			if (inputAuthorized && ((_inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonDown) || (_inputManager.SecondaryShootAxis == MMInput.ButtonStates.ButtonDown)))
			{
				ShootStart();
			}
			
			bool buttonPressed =
				(_inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed) ||
				(_inputManager.SecondaryShootAxis == MMInput.ButtonStates.ButtonPressed); 
            
			if (inputAuthorized && ContinuousPress && (CurrentWeapon.TriggerMode == Weapon.TriggerModes.Auto) && buttonPressed)
			{
				ShootStart();
				Debug.Log(CurrentWeapon.WeaponName);
			}

			if (_inputManager.ReloadButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
			{
				Reload();
			}

			if (inputAuthorized && ((_inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonUp) || (_inputManager.SecondaryShootAxis == MMInput.ButtonStates.ButtonUp)))
			{
				ShootStop();
			}
			
			if ((CurrentWeapon.WeaponState.CurrentState == Weapon.WeaponStates.WeaponDelayBetweenUses)
			    && ((_inputManager.SecondaryShootAxis == MMInput.ButtonStates.Off) && (_inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.Off))
			    && !(UseSecondaryAxisThresholdToShoot && (_inputManager.SecondaryMovement.magnitude > _inputManager.Threshold.magnitude)))
			{
				CurrentWeapon.WeaponInputStop();
			}

			if (inputAuthorized && UseSecondaryAxisThresholdToShoot && (_inputManager.SecondaryMovement.magnitude > _inputManager.Threshold.magnitude))
			{
				ShootStart();
			}
		}
        public override void ShootStart()
        {
			// if the Shoot action is enabled in the permissions, we continue, if not we do nothing.  If the player is dead we do nothing.
			if (!AbilityAuthorized
				|| (CurrentWeapon == null)
				|| (_condition.CurrentState != CharacterStates.CharacterConditions.Normal))
			{
				return;
			}
			if (skillUiManager)
				skillUiManager.SetSkillCooldown(coolDown);
			//  if we've decided to buffer input, and if the weapon is in use right now
			if (BufferInput && (CurrentWeapon.WeaponState.CurrentState != Weapon.WeaponStates.WeaponIdle))
			{
				// if we're not already buffering, or if each new input extends the buffer, we turn our buffering state to true
				ExtendBuffer();
			}

			if (BufferInput && RequiresPerfectTile && (_characterGridMovement != null))
			{
				if (!_characterGridMovement.PerfectTile)
				{
					ExtendBuffer();
					return;
				}
				else
				{
					_buffering = false;
				}
			}
			PlayAbilityStartFeedbacks();
			CurrentWeapon.WeaponInputStart();
		}
    }
}