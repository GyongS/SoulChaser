using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerRUSHSKILL : CPlayerState
{
    
    public override void BeginState()
    {
        _manager.DashPower = 3500f;
        _manager.RushMinusSoul();
        transform.LookAt(_manager.MousePoint.position);
        StartCoroutine(RushOn());
    }

    public override void EndState()
    {
        _manager.Rigid.velocity = Vector3.zero;
    }


    protected override void Update()
    {
        if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
        _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("RUSHSKILL"))
        {
            _manager.SetState(PlayerState.IDLE);
            return;
        }        
        else if (Input.GetKeyDown(KeyOption.AttackKey) &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("RUSHSKILL"))
        {
            _manager.SetState(PlayerState.ATTACK1);
            return;
        }
    }

    




    private IEnumerator RushOn()
    {
        _manager.Rigid.AddForce(_manager.Direction.normalized * _manager.DashPower);
        yield return new WaitForSeconds(0.1f);
        _manager.Rigid.velocity = Vector3.zero;
    }
}
