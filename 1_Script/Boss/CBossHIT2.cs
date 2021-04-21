using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossHIT2 : CBossState
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
        if (_manager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("HIT2") &&
             _manager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            switch(_manager.GetBStat.CurHP)
            {
                case 0:
                    _manager.SetState(BossState.DEAD);
                    break;
                default:
                    _manager.Rigid.isKinematic = false;
                    _manager.SetState(BossState.CHASE);
                    break;
            }
        }
    }
}
