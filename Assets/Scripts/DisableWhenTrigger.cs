using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhenTrigger : MonoBehaviour
{
    public GameObject[] obj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GetComponent<Collider>().enabled = false;
            foreach (var a in obj)
            {
                a.SetActive(false);
            }

        }
    }
}
