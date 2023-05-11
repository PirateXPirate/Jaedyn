using MoreMountains.TopDownEngine;
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
        if (other.tag != "Player") return;
        objectToShow.SetActive(true);
        gameObject.SetActive(false);
        LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
        LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
        LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
    }
}
