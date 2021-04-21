using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterAPPEAR : CMonsterState
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
        if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f &&
              _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("APPEAR"))
        {

            GetComponent<CapsuleCollider>().isTrigger = false;
            GetComponent<Rigidbody>().useGravity = true;
            //CMonsterUI.Instance.HPEnableTrue();
            _manager.SetState(MonsterState.IDLE);
            return;
        }
    }
}
