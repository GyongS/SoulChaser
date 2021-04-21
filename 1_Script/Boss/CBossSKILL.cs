using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossSKILL : CBossState
{
    [SerializeField] private float fRespawnTime;
     private int iCount;
    IEnumerator RespawnCorutin;
    bool bIsStop = false;

    public override void BeginState()
    {
        base.BeginState();
        _manager.Rigid.isKinematic = true;
        iCount = 0;
        RespawnCorutin =Monster_Respawn(fRespawnTime);
        StartCoroutine(RespawnCorutin);
        bIsStop = false;
    }

    public override void EndState()
    {
        base.EndState();
    }

    protected override void Update()
    {
        base.Update();

        if(bIsStop && _manager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f &&
            _manager.GetAnim.GetCurrentAnimatorStateInfo(0).IsName("SKILL"))
        {
            _manager.Rigid.isKinematic = false;
            _manager.SetState(BossState.CHASE);
            return;
        }
    }


    IEnumerator Monster_Respawn(float iTime)
    {
        GameObject[] pArray = new GameObject[_manager.GetRespawnPoint.Length];

        while (iCount < _manager.GetRespawnPoint.Length)
        {
            if (iCount < _manager.GetRespawnPoint.Length - 5) // 0,1,2
                pArray[iCount] = Instantiate(_manager.Monster2, _manager.GetRespawnPoint[iCount].transform.position, transform.rotation);
            else if (iCount > 2)
                pArray[iCount] = Instantiate(_manager.RedMonster, _manager.GetRespawnPoint[iCount].transform.position, transform.rotation);

            yield return new WaitForSeconds(iTime);
            iCount++;
        }

        bIsStop = true;
    }
}

