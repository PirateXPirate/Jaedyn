using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    [SerializeField] PowerUpManager powerUpManager;

    private void OnEnable()
    {
        powerUpManager.AddHpPotion(1);
        powerUpManager.AddResistancePotion(1);
    }
}
