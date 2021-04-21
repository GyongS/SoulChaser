using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKogMawCHASE : CKogMawState
{

    [SerializeField] float fChaseRotation = 0.0f;
    private float fMoveSpeed;

    public override void BeginState()
    {
        base.BeginState();
        fMoveSpeed = _Manager.GetInfo.CurMoveSpeed;
        _Manager.pNavMesh.enabled = false;
        _Manager.Rigid.isKinematic = false;
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void Update()
    {
        _Manager.Player_Checking();


        if (_Manager.GetDistance < _Manager.GetFixedAttackDistance)
        {
            _Manager.SetState(EnemyState.ATTACK);
            return;
        }
        else if (_Manager.GetDistance > _Manager.fDistanceMax)
        {
            _Manager.SetState(EnemyState.NavCHASE);
            return;
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_Manager.GetNormalizeDir), fChaseRotation);
            _Manager.Rigid.velocity = _Manager.GetNormalizeDir * fMoveSpeed;
        }
    }
}
