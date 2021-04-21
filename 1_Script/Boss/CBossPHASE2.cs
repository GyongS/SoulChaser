using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossPHASE2 : CBossState
{
    public override void BeginState()
    {
        base.BeginState();
        _manager.isChangePhase = true;
        _manager.Rigid.isKinematic = true;
        _manager.TargetPhaseCount = 1;
    }

    public override void EndState()
    {
        base.EndState();
        _manager.Rigid.isKinematic = false;
    }

    protected override void Update()
    {
        if (_manager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("PHASE2") &&
            _manager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            Debug.Log("체이스");            
            _manager.UpSpeed();
            _manager.SetState(BossState.CHASE);
            return;
        }
    }
}
