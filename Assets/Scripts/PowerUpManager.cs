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
            playerHealth.CurrentHealth = playerHealth.MaximumHealth;
            playerHealth.UpdateHealthBar(false);

            Utils.soundManager.PlayFX(useHpPotionSound, true);
            powerUpUI.SetHPItemQuantity(hpPotionQuantity);
            var pos = LevelManager.Instance.Players[0].transform.position + (Vector3.up * 1.5f);
            Instantiate(healthPotionFx, pos, Quaternion.identity);

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
            var pos = LevelManager.Instance.Players[0].transform.position + (Vector3.up * 1.5f);
            Instantiate(resistancePotionFx, pos, Quaternion.identity);
            Utils.soundManager.PlayFX(useResistancePotionSound, true);
            var pos2 = LevelManager.Instance.Players[0].transform.position + (Vector3.up * -0.20f);
            var shield = Instantiate(shieldFloorFx, pos2, Quaternion.Euler(-90, 0, 0));
            shield.transform.parent = LevelManager.Instance.Players[0].transform;
            StartCoroutine(CountDown(shield));
            IEnumerator CountDown(GameObject aa)
            {
                yield return new WaitForSeconds(15);
                damageResistance.DamageMultiplier = 1f;
                Destroy(aa);
            }
        }

    }

}
