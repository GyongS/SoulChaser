using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterREADY : CMonsterState
{
    [SerializeField] float fFiexd = 0.0f; 
    public override void BeginState()
    {
        base.BeginState();
        //_manager.MonsterUI.HPEnableFalse(gameObject);
    }

    public override void EndState()
    {
        base.EndState();
    }

    protected override void Update()
    {
        if ((transform.position.x < _BossTransform.position.x - fFiexd)
                || Vector3.Distance(_BossTransform.position, transform.position) < 1.5f)
        {
            _manager.SetState(MonsterState.APPEAR);
            return;
        }
    }
}
