using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CMonsterNavCHASE : CMonsterState
{
    public override void BeginState()
    {
        base.BeginState();

        _manager.pNavMesh.enabled = true;
        _manager.Rigid.isKinematic = true;
    }

    public override void EndState()
    {
        base.EndState();
     
    }

    protected override void Update()
    {
        _manager.Player_Checking();

        _manager.pNavMesh.SetDestination(_manager.GetPlayer.transform.position);

        if (!_manager.pNavMesh.pathPending)
        {
            if (_manager.pNavMesh.remainingDistance <= _manager.pNavMesh.stoppingDistance)
            {
                if (!_manager.pNavMesh.hasPath || _manager.pNavMesh.velocity.sqrMagnitude == 0f)
                {
                    _manager.SetState(MonsterState.CHASE);
                    return;
                }
            }
        }
    }
   
}
