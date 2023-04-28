using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUIManager : MonoBehaviour
{
    [SerializeField] Text keyQuantityText;
    public void SetkeyQuantity(int quantity)
    {
        keyQuantityText.text = quantity.ToString() + " / 1";
    }
}
