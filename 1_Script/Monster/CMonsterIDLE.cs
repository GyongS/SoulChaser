using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterIDLE : CMonsterState
{
    public override void BeginState()
    {
        base.BeginState();
        _manager.pNavMesh.enabled = false;
        _manager.Rigid.isKinematic = false;
    }

    public override void EndState()
    {
        base.EndState();
    }

    protected override void Update()
    {
        _manager.Player_Checking();

        if (_manager.GetDistance < _manager.fDistanceMax)
        {
            _manager.SetState(MonsterState.CHASE);
            return;
        }

        if (_manager.GetDistance > _manager.fDistanceMax &&
           _manager.GetPlayer.transform.position.x > transform.position.x)
        {
            _manager.SetState(MonsterState.NavCHASE);
            return;
        }

    }

}
