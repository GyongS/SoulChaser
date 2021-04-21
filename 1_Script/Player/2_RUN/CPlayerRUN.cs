using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerRUN : CPlayerState
{
    public override void BeginState()
    {
        base.BeginState();
        _manager.Rigid.useGravity = true;
    }

    public override void EndState()
    {
        base.EndState();
        _manager.Rigid.velocity = Vector3.zero;
    }

    protected override void Update()
    {
        Move();
        Dash();
        Attack();
    }

    private void Attack()
    {
        if (!GameStart.BGameStart ||
            UIManager.Instance.isPause ||
            UIManager.isGameOver || CBossManager.Instance.CurrentState == BossState.READY ||
            _manager.BHitTargetSkill || CBossManager.Instance.isPlayerStop)
            return;
        if (Input.GetKeyDown(KeyOption.AttackKey) ||
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
        else if (Input.GetKeyDown(KeyOption.SlowSkill) && _manager.GetStat.CurSoul >= 30)
        {
            _manager.SetState(PlayerState.SLOWSKILL);
            return;
        }
        else if (Input.GetKeyDown(KeyOption.RushSkillKey) && _manager.GetStat.CurSoul >= 10)
        {
            _manager.SetState(PlayerState.RUSHSKILL);
            return;
        }
    }

    private void Move()
    {
        Vector3 cameraForward = _manager.CameraTr.forward;
        cameraForward.y = 0f;
        Vector3 cameraRight = _manager.CameraTr.right;
        cameraRight.y = 0f;

        Vector3 moveForward = cameraForward.normalized * _manager.GetVertical;
        Vector3 moveRight = cameraRight.normalized * _manager.GetHorizontal;

        Vector3 movement = moveForward + moveRight;
        movement.Normalize();


        Rotate(movement);
        movement *= _manager.GetStat.MoveSpeed;
        movement.y = _manager.Rigid.velocity.y;

        _manager.Rigid.velocity = movement;

        if (!(_manager.GetHorizontal == 0 && _manager.GetVertical == 0))
        {
            _manager.MoveDir = moveForward * Input.GetAxis("Vertical");
            _manager.MoveDir = moveRight * Input.GetAxis("Horizontal");
        }
        else
        {
            _manager.SetState(PlayerState.IDLE);
            return;
        }
    }

    private void Rotate(Vector3 movement)
    {
        if (movement.Equals(Vector3.zero))
            return;

        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(movement),
            _manager.GetStat.RotSpeed);
    }


    private void Dash()
    {
        if (Input.GetKeyDown(KeyOption.DashKey) && _manager.GetStat.CurDashGauge > 1)
        {
            _manager.SetState(PlayerState.DASH);
        }
    }
}
