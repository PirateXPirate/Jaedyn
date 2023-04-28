using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoudWhenActive : MonoBehaviour
{
    public AudioClip audi;
    private void OnEnable()
    {
        Utils.soundManager.PlayLoop(audi);
    }
}
