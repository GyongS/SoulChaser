using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossPUSH : CBossState
{
    public override void BeginState()
    {
        base.BeginState();
        _manager.Rigid.isKinematic = true;
        _manager.GetAnim.speed = 1.0f;
        _manager._PushWarning.SetActive(true);
    }

    public override void EndState()
    {
        base.EndState();
        _manager.Rigid.isKinematic = false;
        _manager._PushWarning.SetActive(false);
        if (_manager.TargetPhaseCount == 1)
        {
            _manager.GetAnim.speed = 1.3f;
        }
    }

    protected override void Update()
    {
        base.Update();
        if(_manager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f &&
            _manager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("PUSH"))
        {
            _manager.PushCol.enabled = true;
            if (_manager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f &&
            _manager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("PUSH"))
            {
                _manager.PushCol.enabled = false;
                if (_manager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
                        _manager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("PUSH"))
                {
                    _manager.SetState(BossState.CHASE);
                }
            }            
        }
    }

}
