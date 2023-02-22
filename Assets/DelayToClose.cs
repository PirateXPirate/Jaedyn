using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayToClose : MonoBehaviour
{
    [SerializeField] private float delay;
    void OnEnable ()
    {
        Invoke("Delay", delay);
    }
    void Delay()
    {
        gameObject.SetActive(false);
        LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = true;
        LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = true;
        LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = true;
    }
}
