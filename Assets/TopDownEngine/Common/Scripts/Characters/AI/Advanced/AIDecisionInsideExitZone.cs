using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecisionInsideExitZone : AIDecision
{
    [SerializeField] EnemyZone zoneCollider;
    bool isExit = false;
    public override void Initialization()
    {
        zoneCollider.OnExitTriggerPlayer += OnPlayerExit;
    }

    private void OnPlayerExit(Transform target)
    {
        isExit = true;
        _brain.Target = null;
    }

    public override bool Decide()
    {
        return isExit;
    }
    private void OnDestroy()
    {
        zoneCollider.OnExitTriggerPlayer -= OnPlayerExit;
    }
}
