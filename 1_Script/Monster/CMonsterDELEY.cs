using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterDELEY : CMonsterState
{
    private float fTime = 0.0f;
    [SerializeField] private float fDeleyTime = 0.0f;
    
    public override void BeginState()
    {
        base.BeginState();
      
        fTime = 0.0f;
    }

    public override void EndState()
    {
        base.EndState();
    }

    protected override void Update()
    {
        fTime += Time.deltaTime;
        if (fTime > fDeleyTime)
        {
            // done
            _manager.SetState(MonsterState.IDLE);
            return;
        }
    }
}
