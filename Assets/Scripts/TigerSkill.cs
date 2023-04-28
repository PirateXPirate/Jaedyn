using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerSkill : SkillActivator
{
    protected CharacterHandleWeapon handleWeapon;
    [SerializeField] Weapon SecondaryWeapon;
    protected override void Start()
    {
        base.Start();
        handleWeapon = GetComponent<CharacterHandleWeapon>();
    }
    protected override void Perform()
    {
        base.Perform();
        handleWeapon.ChangeWeapon(SecondaryWeapon, SecondaryWeapon.WeaponID);

        handleWeapon.ShootStart();
    }
    protected override void Update()
    {
        base.Update();
        if (InputManager.Instance.JumpButton.State.CurrentState == MoreMountains.Tools.MMInput.ButtonStates.ButtonUp)
        {
         //   handleWeapon.ShootStop();
        }
    }

}
