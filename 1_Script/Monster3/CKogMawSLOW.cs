using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKogMawSLOW : CKogMawState
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
        if (_pPlayerMgr.Anim.GetCurrentAnimatorStateInfo(0).IsName("ULTSKILL") &&
          _pPlayerMgr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
        {
            _Manager.SetState(EnemyState.SKILL_DEAD);
            return;
        }

    }
}
