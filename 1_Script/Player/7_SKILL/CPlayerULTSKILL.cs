using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerULTSKILL : CPlayerState
{
    private CCameraShake pCameraShake;
    public override void BeginState()
    {
        base.BeginState();
        _manager.UltMinusSoul();
        _manager.UltStartScene.gameObject.SetActive(true);
        _manager.Rigid.isKinematic = true;
        //GetComponent<CapsuleCollider>().center = new Vector3(0, 10, 0);
        pCameraShake = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CCameraShake>();
        //_manager.Rigid.constraints = RigidbodyConstraints.FreezeAll;
    }

    public override void EndState()
    {
        base.EndState();
        //GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
        _manager.UltStartScene.gameObject.SetActive(false);
        _manager.Rigid.isKinematic = false;
        _manager.Rigid.constraints = RigidbodyConstraints.FreezeRotation;
        _manager._isUlt = false;
    }

    protected override void Update()
    {
        if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0 &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("ULTSKILL"))
        {
           
            //if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.78)
            //{
            //    pCameraShake.OnShaking(0.2f, 0.1f,
            //        new Vector3(0, Random.insideUnitSphere.y, 0), true);
            //}
            _manager.ChangeCamera[0].gameObject.SetActive(false);
            _manager.ChangeCamera[1].gameObject.SetActive(true);
            if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("ULTSKILL"))
            {
                _manager.ChangeCamera[0].gameObject.SetActive(true);
                _manager.ChangeCamera[1].gameObject.SetActive(false);
                _manager.SetState(PlayerState.IDLE);
                return;
            }

        }
    }

}
