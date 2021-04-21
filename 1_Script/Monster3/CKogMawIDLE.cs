using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKogMawIDLE : CKogMawState
{
    public override void BeginState()
    {
        base.BeginState();
        GetComponent<CapsuleCollider>().isTrigger = false;
        _Manager.Rigid.useGravity = true;
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void Update()
    {
        _Manager.Player_Checking();

        if (_Manager.GetDistance < _Manager.fDistanceMax)
        {
            _Manager.SetState(EnemyState.CHASE);
            return;
        }
        else if (_Manager.GetDistance < _Manager.GetFixedAttackDistance)
        {
            _Manager.SetState(EnemyState.ATTACK);
            return;
        }

    }
}
