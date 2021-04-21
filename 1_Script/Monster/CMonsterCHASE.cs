using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterCHASE : CMonsterState
{
    [SerializeField] float fChaseRotation = 0.0f;
    float fMoveSpeed;
    private int iRandom;
    private int iGridCount;
    private Node Destination;
    private IEnumerator RandomTime()
    {
        while (true)
        {
            iRandom = Random.Range(0, 2);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public override void BeginState()
    {
        base.BeginState();
        fMoveSpeed = _manager.GetMstat.CurMoveSpeed;
        fMoveSpeed = _manager.GetMstat.CurMoveSpeed;
        _manager.pNavMesh.enabled = false;
        _manager.Rigid.isKinematic = false;
        StartCoroutine(RandomTime());
    }

    public override void EndState()
    {
        base.EndState();
        StopCoroutine(RandomTime());
    }

    protected override void Update()
    {
        _manager.Player_Checking();

        if (_manager.GetDistance < _manager.GetFixedAttackDistance)
        {
            switch (iRandom)
            {
                case 0:
                    _manager.SetState(MonsterState.ATTACK2);
                    break;
                case 1:
                    _manager.SetState(MonsterState.ATTACK);
                    break;

            }
        }

        if (_manager.CurrentState == MonsterState.CHASE)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_manager.GetDirection), fChaseRotation);
            _manager.Rigid.velocity = _manager.GetDirection.normalized * fMoveSpeed;
        }


        if (_manager.GetDistance > _manager.fDistanceMax)
        {
            _manager.SetState(MonsterState.NavCHASE);
            return;
        }


    }

  
}
