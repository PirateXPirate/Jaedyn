using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform Target;
    public Transform TargetPosition;
    public ActivateObject TargetActivateObject;
    public MoveBoxManager moveManager;
    public bool isDrop = true;
    public bool isReset = false;
}
