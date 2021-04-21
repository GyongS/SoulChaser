using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyCHASE : CEnemyState
{
    [SerializeField] float fChaseRotation = 0.0f;
    float fMoveSpeed;

    public override void BeginState()
    {
        base.BeginState();
        fMoveSpeed = _manager.GetInfo.CurMoveSpeed;
        _manager.pNavMesh.enabled = false;
        _manager.Rigid.isKinematic = false;
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void Update()
    {
        _manager.Player_Checking();
        if (_manager.enemyCurrentState == EnemyState.CHASE)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_manager.GetNormalizeDir), fChaseRotation);
            _manager.Rigid.velocity = _manager.GetNormalizeDir * fMoveSpeed;
        }
        if (_manager.GetDistance < _manager.GetFixedAttackDistance)
        {
            _manager.SetState(EnemyState.ATTACK);
            return;
        }
        if (_manager.GetDistance > _manager.fDistanceMax)
        {
            _manager.SetState(EnemyState.NavCHASE);
            return;
        }
    }
}
