using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossDEAD : CBossState
{

    public override void BeginState()
    {
        base.BeginState();
        _manager.Rigid.isKinematic = true;
        _manager.GetAnim.speed = 1.0f;
    }

    public override void EndState()
    {
        base.EndState();
    }

    protected override void Update()
    {
        base.Update();
        
    }
}
