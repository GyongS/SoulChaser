using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossREADY : CBossState
{
    public override void BeginState()
    {
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    protected override void Update()
    {
        if(_manager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("READY") && 
            _manager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            CPlayerManager.Instance.StartTr.gameObject.SetActive(true);
            CPlayerManager.Instance.UltMeshTrue();
            _manager.BossSpawnEffect.gameObject.SetActive(false);
            _manager.StageStartObj.gameObject.SetActive(true);
            _manager.SetState(BossState.IDLE);
            return;
        }
    }
}
