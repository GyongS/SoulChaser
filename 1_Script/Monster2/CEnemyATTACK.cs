using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyATTACK : CEnemyState
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

    public void Update()
    {
        _manager.Player_Checking();

        if (_manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("ATTACK") &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98)
        {
            _manager.SetState(EnemyState.DELEY);
            return;
        }
    }
}
