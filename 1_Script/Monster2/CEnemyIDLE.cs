using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyIDLE : CEnemyState
{
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
        _manager.Player_Checking();

        if (_manager.GetDistance < _manager.fDistanceMax)
        {
            _manager.SetState(EnemyState.CHASE);
            return;
        }
    }
}
