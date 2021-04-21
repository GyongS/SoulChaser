using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySLOW : CEnemyState
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
        if (pPlayerMgr.Anim.GetCurrentAnimatorStateInfo(0).IsName("ULTSKILL") &&
          pPlayerMgr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
        {
            _manager.SetState(EnemyState.SKILL_DEAD);
            return;
        }

    }
}
