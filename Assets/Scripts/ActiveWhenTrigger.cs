using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWhenTrigger : MonoBehaviour
{
    public GameObject obj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GetComponent<Collider>().enabled = false;
            obj.SetActive(true);
        }
    }
}
