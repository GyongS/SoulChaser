using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKogMawDELEY : CKogMawState
{
    private float fTime = 0.0f;
    [SerializeField] private float fDeleyTime = 0.0f;

    public override void BeginState()
    {
        base.BeginState();
        _Manager.Rigid.velocity = Vector3.zero;
        fTime = 0.0f;
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void Update()
    {
        fTime += Time.deltaTime;
        _Manager.Player_Checking();

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_Manager.GetDirection), 0.5f);
        if (fTime > fDeleyTime)
        {
            _Manager.SetState(EnemyState.CHASE);
            return;
        }
    }
}
