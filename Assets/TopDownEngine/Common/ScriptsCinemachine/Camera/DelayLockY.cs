using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayLockY : MonoBehaviour
{
    [SerializeField] public CinemachineAxisLocker cinemachineAxisLocker;
    void Start()
    {
        cinemachineAxisLocker = GetComponent<CinemachineAxisLocker>();

        Invoke("EnableCinemahine", 1);
    }
    void EnableCinemahine()
    {
        cinemachineAxisLocker.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
