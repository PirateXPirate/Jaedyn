using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public delegate void OnTriggerPlayerDelegate(Transform target);
    public OnTriggerPlayerDelegate OnTriggerPlayer;

    public delegate void OnExitTriggerPlayerDelegate(Transform target);
    public OnExitTriggerPlayerDelegate OnExitTriggerPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            OnTriggerPlayer?.Invoke(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            OnExitTriggerPlayer?.Invoke(other.transform);
        }
    }
}
