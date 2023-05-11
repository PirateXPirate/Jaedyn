using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PowerUpUIManager powerUpUI;
    int hpPotionQuantity = 10;
    int resistancePotionQuantity = 10;

    [SerializeField] private JDHealth playerHealth;
    [SerializeField] private DamageResistance damageResistance;

    public delegate void OnUseHPPotion();
    public OnUseHPPotion onUseHPPotion;

    public delegate void OnUseRessistancePotion();
    public OnUseRessistancePotion onUseRessistancePotion;

    [SerializeField] private GameObject healthPotionFx;
    [SerializeField] private GameObject resistancePotionFx;
    [SerializeField] private GameObject shieldFloorFx;


    [SerializeField] private AudioClip useHpPotionSound;
    [SerializeField] private AudioClip useResistancePotionSound;

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
        if (hpPotionQuantity > 0 && playerHealth.CurrentHealth > 0)
        {
            onUseHPPotion?.Invoke();
            hpPotionQuantity -= 1;
            if (playerHealth.CurrentHealth + 20 < playerHealth.MaximumHealth)
                playerHealth.CurrentHealth += 20;
            else
                playerHealth.CurrentHealth = playerHealth.MaximumHealth;
            playerHealth.UpdateHealthBar(false);

            Utils.soundManager.PlayFX(useHpPotionSound, true);
            powerUpUI.SetHPItemQuantity(hpPotionQuantity);
            Instantiate(healthPotionFx, LevelManager.Instance.Players[0].transform.position, Quaternion.identity);
           
        }
    }
    public void UseResistancePotion()
    {
        if (resistancePotionQuantity > 0 && playerHealth.CurrentHealth > 0)
        {
            onUseRessistancePotion?.Invoke();
            resistancePotionQuantity -= 1;
            damageResistance.DamageMultiplier = .75f;          
            powerUpUI.SetResistanceItemQuantity(resistancePotionQuantity);
           
            Instantiate(resistancePotionFx, LevelManager.Instance.Players[0].transform.position, Quaternion.identity);
            Utils.soundManager.PlayFX(useResistancePotionSound,true);
            var shield = Instantiate(shieldFloorFx, LevelManager.Instance.Players[0].transform.position, Quaternion.identity);
            shield.transform.parent = LevelManager.Instance.Players[0].transform;
            StartCoroutine(CountDown(shield));
            IEnumerator  CountDown(GameObject aa)
            {
                yield return new WaitForSeconds(15);
                damageResistance.DamageMultiplier =1f;
                Destroy(aa);
            }
        }
       
    }

}
