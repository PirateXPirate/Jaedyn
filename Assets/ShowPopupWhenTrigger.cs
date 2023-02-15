using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopupWhenTrigger : MonoBehaviour
{
    [SerializeField] GameObject objectToShow;
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        objectToShow.SetActive(true);
        gameObject.SetActive(false);
    }
}
