using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossIDLE : CBossState
{
    public override void BeginState()
    {
        base.BeginState();
        fTime = 0.0f;
        _manager.WalkEffect.gameObject.SetActive(true);
        // _manager.Rigid.velocity = Vector3.zero;
    }

    public override void EndState() 
    {
        base.EndState();
    }

    protected override void Update()
    {
        base.Update();
       _manager.Player_Checking();

       if(_manager.CurrentState == BossState.IDLE)
        _manager.SetState(BossState.CHASE);
       
    }
}
