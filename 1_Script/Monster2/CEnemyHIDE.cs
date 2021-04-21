using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyHIDE : CEnemyState
{
    [SerializeField] float fFiexd = 0.0f;
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
        if ((transform.position.x < _BossTransform.position.x - fFiexd)
                || Vector3.Distance(_BossTransform.position, transform.position) < 1.5f)
        {
            gameObject.GetComponent<CEnemyHIDE>().enabled = false;
            _manager.SetState(EnemyState.READY);
            return;
        }
    }
}
