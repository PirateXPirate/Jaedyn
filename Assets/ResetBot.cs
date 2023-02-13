using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBot : MonoBehaviour
{
    [SerializeField] private Character dummyCharacter;
    private AIBrain dummyBrain;
    private void OnEnable()
    {
        dummyBrain = dummyCharacter.GetComponent<AIBrain>();
        Resetbot();
    }
    private void Resetbot()
    {
        dummyBrain.ResetBrain();
        dummyBrain.BrainActive = true;
        dummyCharacter.GetComponent<Health>().ImmuneToDamage = false;
    }
}
