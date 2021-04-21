using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyDEAD : CEnemyState
{
    private float DeadDelay = 0f;
    public override void BeginState()
    {
        base.BeginState();
        if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99 &&
                 _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("DEAD"))
        {
            _manager.Rigid.velocity = Vector3.zero;
        }
    }

    public override void EndState()
    {
        base.EndState();
    }

 
}
