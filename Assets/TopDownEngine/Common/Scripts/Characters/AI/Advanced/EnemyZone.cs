using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public delegate void OnTriggerPlayerDelegate(Transform target);
    public OnTriggerPlayerDelegate OnTriggerPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            OnTriggerPlayer?.Invoke(other.transform);
        }
    }
}
