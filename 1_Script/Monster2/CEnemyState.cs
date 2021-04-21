using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyState : MonoBehaviour
{
    protected CEnemyManager _manager;
    protected Transform _BossTransform;
    protected CPlayerManager pPlayerMgr;
    private void Awake()
    {
        _BossTransform = GameObject.FindGameObjectWithTag("Boss").GetComponent<Transform>();
        pPlayerMgr = GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayerManager>();
        _manager = GetComponent<CEnemyManager>();
    }

    public virtual void BeginState()
    {
       
    }

    public virtual void EndState()
    {

    }
}
