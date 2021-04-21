using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerIDLE : CPlayerState
{
    public override void BeginState()
    {
        base.BeginState();
        _manager.Rigid.useGravity = true;
        Vector3 velocity = _manager.Rigid.velocity;
        velocity.y = 0f;
        _manager.Rigid.velocity = velocity;


    }

    public override void EndState()
    {
        base.EndState();
    }

    protected override void Update()
    {
        PlayerChangeState();
        _manager.PlayerAttackRotate();
    }


    private void PlayerChangeState()
    {
        if (!GameStart.BGameStart ||
            UIManager.Instance.isPause ||
            UIManager.isGameOver || CBossManager.Instance.CurrentState == BossState.READY ||
            _manager.BHitTargetSkill || CBossManager.Instance.isPlayerStop)
            return;
        if (!(_manager.GetHorizontal == 0 && _manager.GetVertical == 0))
        {
            _manager.SetState(PlayerState.RUN);
            return;
        }
        else if (Input.GetKeyDown(KeyOption.AttackKey) ||
            _manager.AttackDelay > 0f)
        {
            _manager.SetState(PlayerState.ATTACK1);
            return;
        }
        else if (Input.GetKeyDown(KeyOption.UltSkillKey) && _manager.GetStat.CurSoul >= 100)
        {
            _manager._isUlt = true;
            _manager.SetState(PlayerState.ULTSKILL);
            return;
        }
        else if(Input.GetKeyDown(KeyOption.SlowSkill) && _manager.GetStat.CurSoul >= 30)
        {
            _manager.SetState(PlayerState.SLOWSKILL);
            return;
        }
        else if(Input.GetKeyDown(KeyOption.RushSkillKey) && _manager.GetStat.CurSoul >= 10)
        {
            _manager.SetState(PlayerState.RUSHSKILL);
            return;
        }
    }

   
}
