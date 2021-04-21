using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyREADY : CEnemyState
{
    public override void BeginState()
    {
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void Update()
    {
        if(_manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("READY") && 
            _manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
            _manager.Rigid.useGravity = true;
            _manager.SetState(EnemyState.IDLE);
            return;
        }
    }
}
