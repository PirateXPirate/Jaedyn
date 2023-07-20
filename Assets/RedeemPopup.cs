using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedeemPopup : MonoBehaviour
{
    [SerializeField] Button bonusButton;
    [SerializeField] Button okButton;
    [SerializeField] Button closePopupButton;

    [SerializeField] GameObject[] closingGameObject;
    [SerializeField] GameObject popUp;
    void Awake()
    {
      //  bonusButton.onClick.AddListener(OnToggle);
      //  okButton.onClick.AddListener(OnToggle);
       // closePopupButton.onClick.AddListener(OnToggle);
      //  popUp.SetActive(false);
    }

    void OnToggle()
    {
        foreach (GameObject go in closingGameObject)
        {
            go.SetActive(!go.activeSelf);
        }
        popUp.SetActive(!popUp.activeSelf);
    }
}
