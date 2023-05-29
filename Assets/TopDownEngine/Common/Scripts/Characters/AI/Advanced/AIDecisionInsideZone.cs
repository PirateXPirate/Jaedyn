using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecisionInsideZone : AIDecision
{
	[SerializeField] EnemyZone zoneCollider;
    bool isHit = false;
	public override void Initialization()
	{
		zoneCollider.OnTriggerPlayer += OnPlayerHit;
        zoneCollider.OnExitTriggerPlayer += OnPlayerExit;
    }
    private void OnPlayerExit(Transform target)
    {
        isHit = false;
        _brain.Target = null;
    }


    private void OnPlayerHit(Transform target)
    {
        isHit = true;
        _brain.Target = target;
    }

    public override bool Decide()
    {
       
        return isHit;
    }
    private void OnDestroy()
    {
        zoneCollider.OnTriggerPlayer -= OnPlayerHit;
        zoneCollider.OnExitTriggerPlayer -= OnPlayerExit;
    }
}
