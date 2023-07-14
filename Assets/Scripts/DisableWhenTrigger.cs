using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhenTrigger : MonoBehaviour
{
    public GameObject[] obj;
    public bool KeepCollider = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GetComponent<Collider>().enabled = KeepCollider;
            foreach (var a in obj)
            {
                a.SetActive(false);
            }

        }
    }
}
