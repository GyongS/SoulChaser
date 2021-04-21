using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKogMawDEAD : CKogMawState
{
    private float DeadDelay = 0f;
    public override void BeginState()
    {
        base.BeginState();
        if (_Manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99 &&
               _Manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("DEAD"))
        {
            _Manager.Rigid.velocity = Vector3.zero;
        }
    }

    public override void EndState()
    {
        base.EndState();
    }
}
