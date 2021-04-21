using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterSLOW : CMonsterState
{

    float fDeleyTime = 1f;
    float fTime = 0.0f;
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
        // 임시
        //fTime += Time.deltaTime;
        //transform.GetComponentInChildren<Renderer>().material.SetFloat("_SickButton", 1); //hit시 껌빡거리는
        //if (fTime > fDeleyTime)
        //{
        //    fTime = 0;
        //}
        //if (fTime == 0)
        //{
        //    transform.GetComponentInChildren<Renderer>().material.SetFloat("_SickButton", 0);
        //}

        if (pPlayerMgr.CurrentState != PlayerState.ULTSKILL)
        {
            _manager.SetState(MonsterState.SKILL_DEAD);
            return;
        }
    }

}
