using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerATTACK2 : CPlayerState
{
    public override void BeginState()
    {
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
        _manager.Rigid.velocity = Vector3.zero;
    }

    protected override void Update()
    {
        Attack3();
        _manager.PlayerAttackRotate();

    }

    private void Attack3()
    {
        if (Input.GetKeyDown(KeyOption.AttackKey) && _manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("ATTACK2"))
        {
            _manager.SetState(PlayerState.ATTACK3);
            return;
        }
        else if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("ATTACK2") &&
            !(_manager.GetHorizontal == 0 && _manager.GetVertical == 0))
        {
            _manager.SetState(PlayerState.RUN);
            return;
        }
        else if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("ATTACK2"))
        {
            _manager.SetState(PlayerState.IDLE);
            return;
        }
        else if (Input.GetKeyDown(KeyOption.UltSkillKey) && _manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("ATTACK2") &&_manager.GetStat.CurSoul >= 100)
        {
            _manager._isUlt = true;
            _manager.SetState(PlayerState.ULTSKILL);
            return;
        }
        else if(Input.GetKeyDown(KeyOption.SlowSkill) && _manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("ATTACK2") &&_manager.GetStat.CurSoul >= 30)
        {
            _manager.SetState(PlayerState.SLOWSKILL);
            return;
        }
        else if(Input.GetKeyDown(KeyOption.RushSkillKey) && _manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("ATTACK2") && _manager.GetStat.CurSoul >= 10)
        {
            _manager.SetState(PlayerState.RUSHSKILL);
            return;
        }
    }

    private IEnumerator AttackEvent()
    {
        _manager.Rigid.AddForce(transform.forward * 500);
        yield return new WaitForSeconds(0.1f);
        _manager.Rigid.velocity = Vector3.zero;
    }
}
