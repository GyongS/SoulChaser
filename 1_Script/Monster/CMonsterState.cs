using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterState : MonoBehaviour
{
    protected CMonsterManager _manager;
    protected Transform _BossTransform;
    public void SetManager(CMonsterManager manager) { _manager = manager; }

    protected CPlayerManager pPlayerMgr;

    private void Awake()
    {
        _BossTransform = GameObject.FindGameObjectWithTag("Boss").GetComponent<Transform>();
        pPlayerMgr = GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayerManager>();
        _manager = GetComponent<CMonsterManager>();
    }

    public virtual void BeginState() { }
    public virtual void EndState() {}

    protected virtual void Update()
    {
        
    }


}
