using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JDHealth : Health
{
    [SerializeField] private CharacterHealthUIManager characterHealthUIManager;

    public delegate void OnRecieveAttack(int currenthealth);
    public OnRecieveAttack onRecieveAttack;

    public override void UpdateHealthBar(bool show)
    {
        base.UpdateHealthBar(show);
        if (_character.CharacterType == Character.CharacterTypes.Player)
        {
            characterHealthUIManager.SetHP(Mathf.FloorToInt(CurrentHealth));
            onRecieveAttack?.Invoke(Mathf.FloorToInt(CurrentHealth));
        }
            
    }
}
