using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterHIT : CMonsterState
{

    public override void BeginState()
    {
        base.BeginState();
        //   _manager.pNavMesh.isStopped = true;
     
    }

    public override void EndState()
    {
        base.EndState();
    }

    protected override void Update()
    {
        if(_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("HIT"))
        {
            _manager.Rigid.velocity = Vector3.zero;
            _manager.SetState(MonsterState.DELEY);
            return;
        }
        
            
            

            
    }
}
