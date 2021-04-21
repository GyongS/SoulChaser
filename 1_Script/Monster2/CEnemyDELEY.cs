using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyDELEY : CEnemyState
{
    private float fTime = 0.0f;
    [SerializeField] private float fDeleyTime = 0.0f;

    public override void BeginState()
    {
        base.BeginState();
        _manager.Rigid.velocity = Vector3.zero;
        fTime = 0.0f; 
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void Update()
    {
        fTime += Time.deltaTime;
        if (fTime > fDeleyTime)
        {
            _manager.SetState(EnemyState.CHASE);
            return;
        }
    }
}
