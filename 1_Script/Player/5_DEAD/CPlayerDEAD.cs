using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerDEAD : CPlayerState
{
    public override void BeginState()
    {
        base.BeginState();
        _manager.BDead = true;
        _manager.AttackDirCircle.gameObject.SetActive(false);
        _manager.AttackDirPoint.gameObject.SetActive(false);
    }

    public override void EndState()
    {
        base.EndState();
    }

    protected override void Update()
    {

    }
}
