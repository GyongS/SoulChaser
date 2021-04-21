using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossCHASE : CBossState
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
        base.Update();
        TargetSkill();
        _manager.GetPath.FindPathWithPriorityQueue(transform.position, DestPoint.position);
      
         iGridCount = _manager.GetGrid.path.Count; // 현재 경로의 카운트를 갖고온다.

        if (UIManager.Instance.TargetHPCount <= 20 && !_manager.isChangePhase && !FenceManager._isSlow)
        {
            _manager.SetState(BossState.PHASE2);
            return;
        }

        if (iGridCount > 1) //갖고온 경로가 존재 하면 카운트를 뺀다.
        {
            MoveToTarget(_manager.GetGrid.path[--iGridCount]);
            Vector3 vNormalize = (Destination.worldPosition - transform.position).normalized;
         
          
            bIsAStar = true;
            vNormalize.y = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(vNormalize), 0.3f);
            _manager.Rigid.velocity = vNormalize * _manager.GetBStat.CurMoveSpeed;
          
          
        }        
        else if(iGridCount <= 1)
        {
          
            _manager.SetState(BossState.IDLE);
            return;
        }

        
        
    }

    public void TargetSkill()
    {
        if (_manager._skillcount >= 20 && !FenceManager._isSlow)
        {
            _manager.SetState(BossState.PUSH);
            _manager._skillcount = 0;
        }
    }
}
