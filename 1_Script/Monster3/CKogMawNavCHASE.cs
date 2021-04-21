using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKogMawNavCHASE : CKogMawState
{
    public override void BeginState()
    {
        base.BeginState();

        _Manager.pNavMesh.enabled = true;
        _Manager.Rigid.isKinematic = true;
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void Update()
    {
        _Manager.Player_Checking();
        _Manager.pNavMesh.SetDestination(_Manager.GetPlayer.transform.position);

        if (!_Manager.pNavMesh.pathPending)
        {
            if (_Manager.pNavMesh.remainingDistance <= _Manager.pNavMesh.stoppingDistance)
            {
                if (!_Manager.pNavMesh.hasPath || _Manager.pNavMesh.velocity.sqrMagnitude == 0f)
                {
                    _Manager.SetState(EnemyState.CHASE);
                    return;
                }
            }
        }
    }
}
