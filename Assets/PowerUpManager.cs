using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PowerUpUIManager powerUpUI;
    int hpPotionQuantity = 0;
    int resistancePotionQuantity = 0;

    [SerializeField] private JDHealth playerHealth;
    [SerializeField] private DamageResistance damageResistance;

    public delegate void OnUseHPPotion();
    public OnUseHPPotion onUseHPPotion;

    public delegate void OnUseRessistancePotion();
    public OnUseRessistancePotion onUseRessistancePotion;

    void Start()
    {
        powerUpUI.SetHPItemQuantity(hpPotionQuantity);
        powerUpUI.SetResistanceItemQuantity(resistancePotionQuantity);
    }

    public void AddHpPotion(int quantity)
    {
        hpPotionQuantity += quantity;
        powerUpUI.SetHPItemQuantity(hpPotionQuantity);
      
    }

    public void AddResistancePotion(int quantity)
    {
        resistancePotionQuantity += quantity;
        powerUpUI.SetResistanceItemQuantity(resistancePotionQuantity);
    }

    public void UseHPPotion()
    {
        if (hpPotionQuantity > 0)
        {
            onUseHPPotion?.Invoke();
            hpPotionQuantity -= 1;
            if (playerHealth.CurrentHealth + 10 < playerHealth.MaximumHealth)
                playerHealth.CurrentHealth += 10;
            else
                playerHealth.CurrentHealth = playerHealth.MaximumHealth;
            playerHealth.UpdateHealthBar(false);
            powerUpUI.SetHPItemQuantity(hpPotionQuantity);
        }
    }
    public void UseResistancePotion()
    {
        if (resistancePotionQuantity > 0)
        {
            onUseRessistancePotion?.Invoke();
            resistancePotionQuantity -= 1;
            damageResistance.DamageMultiplier = .75f;          
            powerUpUI.SetResistanceItemQuantity(resistancePotionQuantity);
            StartCoroutine(CountDown());

            IEnumerator  CountDown()
            {
                yield return new WaitForSeconds(15);
                damageResistance.DamageMultiplier =1f;
            }
        }
       
    }

}
