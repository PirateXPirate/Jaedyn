using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JDHealth : Health
{
    public CharacterHealthUIManager characterHealthUIManager;

    [SerializeField] GameObject Particle;

    public delegate void OnRecieveAttack(int currenthealth);
    public OnRecieveAttack onRecieveAttack;

    public override void UpdateHealthBar(bool show)
    {
        base.UpdateHealthBar(show);
        Debug.Log(name);
        if (_character != null )
        {
            characterHealthUIManager.SetHP(Mathf.FloorToInt(MasterHealth.CurrentHealth));
            onRecieveAttack?.Invoke(Mathf.FloorToInt(MasterHealth.CurrentHealth));
        }
        
            
    }
    public override void DamageDisabled()
    {
        base.DamageDisabled();
        Instantiate(Particle, transform.position, Quaternion.Euler(0,0,0));
    }
  
}
