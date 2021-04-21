using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterATTACK : CMonsterState
{

    public override void BeginState()
    {
        base.BeginState();
        _manager.Rigid.velocity = Vector3.zero;
    }

    public override void EndState()
    {
        base.EndState();
    }

    protected override void Update()
    {
        _manager.Player_Checking();
       // transform.LookAt(_manager.Target.position);

        if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98&&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
        {
            _manager.SetState(MonsterState.DELEY);
            return;
        }

    }
}
