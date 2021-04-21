using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerDASH : CPlayerState
{
    public override void BeginState()
    {
        base.BeginState();
        _manager.DashPower = 3200f;
        _manager.MinusDashGauge();
        StartCoroutine(DashOn());
        UIManager.Instance.UltIcon.color = new Color(1, 1, 1, 0.5f);
    }

    public override void EndState()
    {
        base.EndState();
        _manager.Rigid.velocity = Vector3.zero;
        UIManager.Instance.UltIcon.color = new Color(1, 1, 1, 1);
    }

    protected override void Update()
    {
        if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("DASH") &&
            Input.GetKeyDown(KeyOption.AttackKey))
        {
            _manager.SetState(PlayerState.ATTACK1);
            return;
        }
        else if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f &&
                    !(_manager.GetHorizontal == 0 &&
                    _manager.GetVertical == 0) &&
                    _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("DASH"))
        {
            _manager.SetState(PlayerState.RUN);
            return;
        }
        else if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("DASH"))
        {
            _manager.SetState(PlayerState.IDLE);
            return;
        }

    }

    private IEnumerator DashOn()
    {
        _manager.Rigid.AddForce(transform.forward * _manager.DashPower);
        yield return new WaitForSeconds(0.1f);
        _manager.Rigid.velocity = Vector3.zero;
    }

    private void ApplyGravity()
    {
        Vector3 velocity = _manager.Rigid.velocity;
        velocity.x = velocity.x + _manager.Gravity * Time.deltaTime;
        _manager.Rigid.velocity = velocity;
    }
}
