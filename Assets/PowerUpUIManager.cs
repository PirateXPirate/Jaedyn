using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUIManager : MonoBehaviour
{
    [SerializeField] Text HPText;
    [SerializeField] Text ResistanceText;
    public void SetHPItemQuantity(int quantity)
    {
        HPText.text = "x " + quantity.ToString();
    }

    public void SetResistanceItemQuantity(int quantity)
    {
        ResistanceText.text = "x " + quantity.ToString();
    }
}
