using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKogMawHIDE : CKogMawState
{
    [SerializeField] float fFiexd = 0.0f;
    public override void BeginState()
    {
        base.BeginState();
       transform.position = new Vector3(transform.position.x, 0.561486f, transform.position.z);
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
            gameObject.GetComponent<CKogMawHIDE>().enabled = false;
          
            _Manager.SetState(EnemyState.READY);
            return;
        }
    }
}
