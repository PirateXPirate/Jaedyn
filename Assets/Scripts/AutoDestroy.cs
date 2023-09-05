using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float Delay;
    void OnEnable()
    {
        Invoke("DeActivate", Delay);
    }
   
    void DeActivate()
    {
        Debug.Log("DEstroy");
        gameObject.SetActive(false);
    }
}
