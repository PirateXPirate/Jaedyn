using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxManager : MonoBehaviour
{
    public GameObject TargetObject;
    public MoveObject[] moveObjectList;
    int currentMoveObject =0;
 

    public void OnMoveComplete()
    {
        currentMoveObject += 1;
        Debug.Log(currentMoveObject);
        Debug.Log(moveObjectList.Length);
        if (currentMoveObject >= moveObjectList.Length)
        {
            TargetObject.SetActive(true);
        }
    }
}
