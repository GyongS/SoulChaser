using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossHIT : CBossState
{
    public override void BeginState()
    {
        base.BeginState();
        //Courtin = MonsterHit(0.0f , 1f);
        _manager.PushCol.enabled = false;
        _manager.Rigid.isKinematic = true;
    }


    public override void EndState()
    {
        base.EndState();
       // StopCoroutine(Courtin);
        
    }

    protected override void Update()
    {
        if(_manager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("HIT") &&
            _manager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.70f)
        {
            _manager.GetAnim.speed = 0;
        }
        if (_manager.GetPlayer.Anim.GetCurrentAnimatorStateInfo(0).IsName("ULTSKILL") &&
            _manager.GetPlayer.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
        {
            _manager.GetAnim.speed = 1;
            _manager.SetState(BossState.HIT2);
            return;
        }
    }
}
