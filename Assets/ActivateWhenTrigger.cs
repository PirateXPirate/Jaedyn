using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWhenTrigger : MonoBehaviour
{
    public ActivateObject obj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
         //   GetComponent<Collider>().enabled = false;
            obj.Activate();
        }
    }
}
