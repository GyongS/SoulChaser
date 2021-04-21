using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CCamState : MonoBehaviour
{
    protected CCameraMgr _CamMgr;
    private Animator pPlayerAni;

    private Transform _pTarget;
    public Transform GetTarget { get { return _pTarget; } }
    private Transform _pBossMonster;
    public Transform GetBossPos { get { return _pBossMonster; } }
    public Animator GetPlayerAni { get { return pPlayerAni; } }
    
    public CPlayerManager player;
    
    private void Awake()
    {
        _pTarget = GameObject.Find("CameraLookAtPostion").GetComponent<Transform>();
        _pBossMonster = GameObject.FindGameObjectWithTag("Boss").GetComponent<Transform>();

        player = GameObject.FindWithTag("Player").GetComponent<CPlayerManager>();
        _CamMgr = GetComponent<CCameraMgr>();
    }

    public virtual void BeginState()
    {

    }
    public virtual void EndState()
    {

    }

   
}
