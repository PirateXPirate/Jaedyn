
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    [SerializeField] PowerUpManager powerUpManager;
    public int numPotion;
    public int numResistance;

    public bool isRandom = false;

    private void OnEnable()
    {
        if (isRandom)
        {
            numPotion = Random.Range(1, 3);
        }
        powerUpManager.AddHpPotion(numPotion);
        powerUpManager.AddResistancePotion(numResistance);
    }
    public void GetPotionChest()
    {
        powerUpManager.AddHpPotion(1);
    }

    internal void GetResistanceChest()
    {
        powerUpManager.AddResistancePotion(1);
    }
}
