using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKogMawState : MonoBehaviour
{
    protected CEnemyManager _Manager;
    protected Transform _BossTransform;
    protected CPlayerManager _pPlayerMgr;

    private void Awake()
    {
        _BossTransform = GameObject.FindGameObjectWithTag("Boss").GetComponent<Transform>();
        _pPlayerMgr = GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayerManager>();
        _Manager = GetComponent<CEnemyManager>();
    }

    public virtual void BeginState()
    {
       
    }

    public virtual void EndState()
    {

    }

}
