using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float Delay;
    void Start()
    {
        Invoke("DeActivate", Delay);
    }
    void DeActivate()
    {
        Debug.Log("SSS");
        gameObject.SetActive(false);
    }
}
