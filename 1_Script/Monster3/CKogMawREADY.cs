using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKogMawREADY : CKogMawState
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
        if (_Manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("READY") &&
            _Manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
            _Manager.Rigid.useGravity = true;
            _Manager.SetState(EnemyState.IDLE);
            return;
        }
    }
}
