using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerHIT : CPlayerState
{
    public override void BeginState()
    {
        base.BeginState();
        _manager.HitObj.SetActive(true);
        for (int i = 0; i < _manager._AttackCol.Length; i++)
        {
            _manager._AttackCol[i].enabled = false;
        }
    }

    public override void EndState()
    {
        base.EndState();
        _manager.HitObj.SetActive(false);
    }

    protected override void Update()
    {
        if(_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("HIT"))
        {
            _manager.SetState(PlayerState.IDLE);
            return;
        }
        else if (!(_manager.GetHorizontal == 0 && _manager.GetVertical == 0) &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("HIT"))
        {
            _manager.SetState(PlayerState.RUN);
            return;
        }
        else if (Input.GetKeyDown(KeyOption.UltSkillKey) && _manager.GetStat.CurSoul >= 100 &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("HIT"))
        {
            _manager._isUlt = true;
            _manager.SetState(PlayerState.ULTSKILL);
            return;
        }
        else if (Input.GetKeyDown(KeyOption.SlowSkill) && _manager.GetStat.CurSoul >= 30 &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("HIT"))
        {
            _manager.SetState(PlayerState.SLOWSKILL);
            return;
        }
        else if (Input.GetKeyDown(KeyOption.RushSkillKey) && _manager.GetStat.CurSoul >= 10 &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("HIT"))
        {
            _manager.SetState(PlayerState.RUSHSKILL);
            return;
        }
    }
}
