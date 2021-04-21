using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BossStat
{
    public float CurHP;
    public float MaxHP;
    public float CurMoveSpeed;
    public float MaxMoveSpeed;
    public float CurSlowTime;
    public float MaxSlowTime;
    //public int SkillCount;
}

public enum BossState
{
    READY = 0,
    IDLE, // 대기
    CHASE, // 추척 
    HIT, // HIT1 , 궁피격 일때 첫 모션
    HIT2, // 궁피격 끝날때 모션
    DEAD,
    SKILL,
    PUSH, // 밀쳐내기
    PHASE2,
}

public class CBossManager : MonoBehaviour
{
    private bool _bIsInit = false;

    private static CBossManager _instance;
    public static CBossManager Instance { get { return _instance; } }


    public Dictionary<BossState, CBossState>
           _BossState = new Dictionary<BossState, CBossState>();
    private GameObject[] RespawnPoint;
    public GameObject[] GetRespawnPoint
    { get { return RespawnPoint; } }

    private Animator _anim;
    public Animator GetAnim { get { return _anim; } }

    private BossState _currentState;
    public BossState CurrentState { get { return _currentState; } }

    private BossState _PrevState;
    public BossState PrevState { get { return _PrevState; } }

    [SerializeField]
    private BossStat _stat;
    public BossStat GetBStat { get { return _stat; } }

    public int _skillcount; 

    private Rigidbody _rigid;
    public Rigidbody Rigid { get { return _rigid; } }

    private Pathfinding _pathfinding;
    public Pathfinding GetPath { get { return _pathfinding; } }

    private Grid grid;
    public Grid GetGrid { get { return grid; } }
    private CPlayerManager _PlayerMgr;
    public CPlayerManager GetPlayer { get { return _PlayerMgr; } }
    int iButton = 0;
    int iHitButton = 0;
    private Transform pPlayerTransform;
    private float fTargetDistance = 0.0f;
    public float GetTargetDistance { get { return fTargetDistance; } }
    public GameObject Monster2;
    public GameObject RedMonster;

    public Transform MonsterParent;
    public Transform BossDeadEffect;
    public Transform ShaderTr;
    public Transform BossSpawnEffect;
    public Transform StageStartObj;
    public Transform WalkEffect;

    public Collider PushCol;

    public bool isChangePhase = false;
    public int TargetPhaseCount;

    private CCameraShake pCameraShake;
    private float DeadDelay;

    [SerializeField]
    GameObject _UICanvas;

    [SerializeField]
    GameObject _UIFxCanvas;
    
    public GameObject _PushWarning;

    public bool isDead;
    public bool isPlayerStop;
    private bool bSlow;

    public void SetState(BossState eState)
    {        
        if (_bIsInit)
        {
            _BossState[_currentState].enabled = false;
            _BossState[_currentState].EndState();
        }
        _currentState = eState;
        _BossState[_currentState].BeginState();
        _BossState[_currentState].enabled = true;

        _anim.SetInteger("BossState", (int)_currentState);
        _PrevState = _currentState;

    }

    private void Ready_BossState()
    {
        BossState[] eStateValue = (BossState[])System.Enum.GetValues(typeof(BossState));
        for (int i = 0; i < eStateValue.Length; i++)
        {
            System.Type FSMType = System.Type.GetType("CBoss" + eStateValue[i].ToString());
            CBossState pFSMState = (CBossState)GetComponent(FSMType);
            if (null == pFSMState)
            {
                pFSMState = (CBossState)gameObject.AddComponent(FSMType);
            }

            _BossState.Add(eStateValue[i], pFSMState);
            pFSMState.enabled = false;
        }
    }


    private void Awake()
    {
        _instance = this;
        _anim = GetComponentInChildren<Animator>();
        _stat.CurHP = _stat.MaxHP;
        _rigid = GetComponentInParent<Rigidbody>();
        _pathfinding = GameObject.Find("AStar").GetComponent<Pathfinding>();
        _PlayerMgr = GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayerManager>();
        pPlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        grid = GameObject.Find("AStar").GetComponent<Grid>();
        _stat.CurMoveSpeed = _stat.MaxMoveSpeed;
        RespawnPoint = GameObject.FindGameObjectsWithTag("RespawnPoint");
        Ready_BossState();
        pCameraShake = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CCameraShake>();

        DeadDelay = 4.5f;
    }

    private void Start()
    {
        _bIsInit = true;
        SetState(BossState.READY);
    }


    public void Player_Checking()
    {
        fTargetDistance = Vector3.Distance(pPlayerTransform.position, transform.position);
        transform.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", iButton);
        transform.GetComponentInChildren<Renderer>().material.SetFloat("_SickButton", iHitButton);

    }
    private void Update()
    {
        Player_Checking();
        IgnoreCollision();
        Dead();
        SlowDebuff();
    }

    private void Dead()
    {
        if (PrevState != BossState.HIT)
        {
            if (_stat.CurHP <= 0 && UIManager.Instance.TargetHPCount <= 0)
            {
                SetState(BossState.DEAD);
                isPlayerStop = true;
                _rigid.isKinematic = true;
                _UICanvas.SetActive(false);
                _UIFxCanvas.SetActive(false);
                if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
                _anim.GetCurrentAnimatorStateInfo(0).IsName("DEAD"))
                {                    
                    DeadDelay -= Time.deltaTime;
                    ShaderTr.GetComponent<Renderer>().material.SetFloat("_Height", DeadDelay);
                    WalkEffect.position = new Vector3(0, WalkEffect.position.y - 1 * Time.deltaTime, 0);
                    if (DeadDelay <= 5f)
                    {
                        BossDeadEffect.gameObject.SetActive(true);
                        if (DeadDelay <= 0)
                        {
                            isDead = true;
                            gameObject.SetActive(false);
                            BossDeadEffect.gameObject.SetActive(false);
                            WalkEffect.gameObject.SetActive(false);
                        }
                    }

                }
            }
        }
    }
   


    public GameObject[] _EffectOBJ;
    public Transform _EffectManager;


    private void IgnoreCollision()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("MONSTER"), LayerMask.NameToLayer("BOSS"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("GROUND"), LayerMask.NameToLayer("BOSS"), true);
    }

    public void MHitEffect()
    {
        GameObject SkillEffect = Instantiate(_EffectOBJ[0], transform.position, transform.rotation);
        //SkillEffect.transform.parent = _EffectManager;
        Destroy(SkillEffect, 2f);
    }
    
   

    private void OnTriggerEnter(Collider col)
    {
        if (CurrentState == BossState.DEAD)
            return;

        switch (col.tag)
        {
            case "PWeapon":
                CPlayerManager.Instance.PlusSoul();
                CBossSound.Instance._BossHitSound();
                MHitEffect();
                pCameraShake.OnShaking(0.3f, 0.3f,
                   new Vector3(Random.insideUnitSphere.x, Random.insideUnitSphere.y, Random.insideUnitSphere.z), true);
                _stat.CurHP -= CPlayerManager.Instance.GetStat.Damage;
                StartCoroutine(HitShader());

                if (!FenceManager._isSlow)_skillcount++;                

                UIManager.Instance.TargetHitHpColor();
                break;

            case "PSkillEvent":
                if (PrevState != BossState.DEAD)
                {
                    SetState(BossState.HIT);
                }
                StartCoroutine(HitShader());
                break;

            case "PSkill":
                _stat.CurHP -= CPlayerManager.Instance.GetStat.SkillDamage;
                MHitEffect();
                CBossSound.Instance._BossHitSound();                
                StartCoroutine(HitShader());
                MHitEffect();
                UIManager.Instance.TargetHitHpColor();
                break;

            case "RushSkill":
                CBossSound.Instance._BossHitSound();
                pCameraShake.OnShaking(0.2f, 0.1f,
                   new Vector3(Random.insideUnitSphere.x, Random.insideUnitSphere.y, 0), true);
                MHitEffect();
                _stat.CurHP -= CPlayerManager.Instance.GetStat.RushDamage;
                break;

            case "SlowSkill":
                //StartCoroutine(HitSlowSkill());
                bSlow = true;
                _stat.CurSlowTime = _stat.MaxSlowTime;
                break;         
            case "GameOver":
                gameObject.SetActive(false);
                UIManager.isGameOver = true;
                break;
        }
    }


    private void SlowDebuff()
    {
        if (bSlow)
        {
            _stat.CurSlowTime -= Time.deltaTime;
            ShaderTr.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", 1);
            _stat.CurMoveSpeed = _stat.MaxMoveSpeed / 2;
            if (_stat.CurSlowTime <= 0)
            {
                ShaderTr.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", 0);
                _stat.CurMoveSpeed = _stat.MaxMoveSpeed;
                _stat.CurSlowTime = 0;
                bSlow = false;
            }
        }
    }


    //private IEnumerator HitSlowSkill()
    //{
    //    ShaderTr.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", 1);
    //    _stat.CurMoveSpeed = _stat.MaxMoveSpeed / 2;
    //    yield return new WaitForSeconds(_stat.SlowTime);
    //    ShaderTr.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", 0);
    //    _stat.CurMoveSpeed = _stat.MaxMoveSpeed;
    //}

    private IEnumerator HitShader()
    {
        ShaderTr.GetComponentInChildren<Renderer>().material.SetFloat("_SickButton", 1);
        yield return new WaitForSeconds(0.3f);
        ShaderTr.GetComponentInChildren<Renderer>().material.SetFloat("_SickButton", 0);

    }

    private IEnumerator MonsterStop()
    {
        _rigid.isKinematic = true;
        _anim.speed = 0;
        yield return new WaitForSeconds(3.5f);
        _rigid.isKinematic = false;
        _anim.speed = 1;
    }

    public void SlowSpeed()
    {
        _stat.CurMoveSpeed = 1.5f;
    }

    public void initSpeed()
    {
        _stat.CurMoveSpeed = _stat.MaxMoveSpeed;
    }

    public void UpSpeed()
    {
        _stat.CurMoveSpeed = 4;
        _stat.MaxMoveSpeed = 4;
        _anim.speed = 1.3f;
    }

    public void initTargetHP()
    {
        _stat.CurHP = _stat.MaxHP;
    }

   public void ZeroHP()
    {
        _stat.CurHP = 0;
    }
    
}
