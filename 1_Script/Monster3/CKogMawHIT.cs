using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKogMawHIT : CKogMawState
{
    private float fTime = 0.0f;
    [SerializeField] private float fDeleyTime = 0;
    [SerializeField] bool bIsNextState = false;
    public override void BeginState()
    {
        base.BeginState();
        fTime = 0.0f;
        bIsNextState = false;
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void Update()
    {
        fTime += Time.deltaTime;

        if (_Manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f &&
            _Manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("HIT"))
        {
            _Manager.Rigid.velocity = Vector3.zero;
            bIsNextState = true;
        }
        if (bIsNextState && fTime > fDeleyTime)
        {
            _Manager.SetState(EnemyState.DELEY);
            return;
        }
    }
}
