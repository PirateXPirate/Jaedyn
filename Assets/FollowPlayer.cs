using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float offsetY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = LevelManager.Instance.Players[0].transform.position;

        currentPosition.y += offsetY;
        transform.position = currentPosition;
       


    }
}
