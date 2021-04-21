using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public struct MonsterStat
{
    public float CurHP;
    public float MaxHP;
    public float CurMoveSpeed;
    public float MaxMoveSpeed;
    public float AttackRange;
    public float ChaseRange;
    public float Damage;
    public float CurSlowTime;
    public float MaxSlowTime;
}

public enum MonsterState
{
    READY = 0,      // 0. 준비
    APPEAR,         // 1. 등장 
    IDLE,           // 2. 대기
    CHASE,          // 3. 추격
    ATTACK,         // 4. 공격
    ATTACK2,        // 5. 공격2
    DEAD,           // 6. 사망    
    HIT,            // 7. 피격
    SLOW,           // 8. 궁맞았을때 느리게 멈추는 상태
    SKILL_DEAD,     // 9. 막타 맞을때
    NavCHASE,       // 10. NAV
    DELEY           // 11. 대기
}



public class CMonsterManager : MonoBehaviour
{

    private CMonsterUI _MonsterUI;
    public CMonsterUI MonsterUI { get { return _MonsterUI; } }

    private bool _isinit = false;

    public MonsterState StartState = MonsterState.READY;
    private Dictionary<MonsterState, CMonsterState>
    _states = new Dictionary<MonsterState, CMonsterState>();

    private MonsterState _currentState;
    public MonsterState CurrentState { get { return _currentState; } }

    private int iEffectCnt = 0;

    private NavMeshAgent _pNavMesh;
    public NavMeshAgent pNavMesh { get { return _pNavMesh; } }

    private MonsterState _prevState;
    public MonsterState PrevState { get { return _prevState; } }


    public CMonsterState CurrentStateComponent
    { get { return _states[_currentState]; } }

    [SerializeField]
    private MonsterStat _Mstat;
    public MonsterStat GetMstat { get { return _Mstat; } set { _Mstat = value; } }

    private Animator _anim;
    public Animator Anim { get { return _anim; } }

    private Rigidbody _rigid;
    public Rigidbody Rigid { get { return _rigid; } }

    [SerializeField]
    private Vector3 _Direction;
    public Vector3 Direction { get { return _Direction; } set { _Direction = value; } }

    [SerializeField]
    private Transform _Target;
    public Transform Target { get { return _Target; } }

    [SerializeField]
    private Collider _MAttackCol;

    // 여기서 부턴 플레이어 체크를 위한 변수

    private GameObject _pPlayer;
    public GameObject GetPlayer { get { return _pPlayer; } }

    private Animator _pPlayerAnim;
    public Animator GetPlayerAnim { get { return _pPlayerAnim; } }


    private Vector3 _vNormalDirection;  // 노말라이즈화된 방향
    public Vector3 GetNormalizeDir { get { return _vNormalDirection; } }

    private Vector3 _vDirection;
    public Vector3 GetDirection { get { return _vDirection; } }

    CMonsterSound _cmonstersound;

    public float fAngleMax = 0;
    public float fDistanceMax = 0;

    [SerializeField] private float _fCurAngle;
    public float GetAngle { get { return _fCurAngle; } }

    [SerializeField] private float _fCurDistance;
    public float GetDistance { get { return _fCurDistance; } }

    private float DeadDelay;

    public Transform _SlowEffect;
    private bool bSlow;
    public Transform ShaderTr;
       

    // GameObject Instance 

    [Header("MonsterShader")]
    private float _fMonsterShader;


    [SerializeField] private float _fMinAttackDistnace, _fMaxAttackDistance, _fFixedAttackDistance;
    public float GetMinAttackDistance { get { return _fMinAttackDistnace; } }
    public float GetMaxAttackDistance { get { return _fMaxAttackDistance; } }
    public float GetFixedAttackDistance { get { return _fFixedAttackDistance; } }
    // private LayerMask _LayerMask;
    // private string szLayerName;
    private CCameraShake pCameraShake;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _pPlayer = GameObject.FindGameObjectWithTag("Player");
        _fMonsterShader = transform.GetComponentInChildren<Renderer>().material.GetFloat("_SickButton");
        iEffectCnt = 0;
        _Mstat.CurHP = _Mstat.MaxHP;
        _Mstat.CurMoveSpeed = _Mstat.MaxMoveSpeed;
        _cmonstersound = GetComponent<CMonsterSound>();
        _pNavMesh = GetComponent<NavMeshAgent>();
        _pNavMesh.stoppingDistance = _fMaxAttackDistance;
        _MonsterUI = GameObject.Find("2_MONSTER").GetComponent<CMonsterUI>();
        //_LayerMask = gameObject.layer;
        //szLayerName = LayerMask.LayerToName(_LayerMask);
        DeadDelay = 0.5f;
        pCameraShake = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CCameraShake>(); 
        MonsterStateVal();
        _SlowEffect = transform.GetChild(4).GetComponent<Transform>();
    }

    private void Start()
    {
        _isinit = true;
        SetState(StartState);
       
    }

    private void Update()
    {
        SkillDead();
        Dead();
        GameOver();
        SlowDebuff();
        MonsterColActive();
    }

    void MonsterColActive()
    {
        if(CPlayerManager.Instance.CurrentState == PlayerState.ULTSKILL)
            _MAttackCol.enabled = false;
    }

    private void SlowDebuff()
    {
        if(bSlow)
        {
            _Mstat.CurSlowTime -= Time.deltaTime;
            transform.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", 1);
            _Mstat.CurMoveSpeed = _Mstat.MaxMoveSpeed / 2;
            if (_Mstat.CurSlowTime <= 0)
            {
                transform.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", 0);
                _Mstat.CurMoveSpeed = _Mstat.MaxMoveSpeed;
                _SlowEffect.gameObject.SetActive(false);
                _Mstat.CurSlowTime = 0;
                bSlow = false;
            }
        }
    }

    public void SetState(MonsterState newState)
    {
        if (_isinit)
        {
            _states[_currentState].enabled = false;
            _states[_currentState].EndState();
        }

        _currentState = newState;
        _states[_currentState].BeginState();
        _states[_currentState].enabled = true;
        _anim.SetInteger("MonsterState", (int)_currentState);
        _prevState = _currentState;
    }

 


    private void MonsterStateVal()
    {
        MonsterState[] stateValues = (MonsterState[])System.Enum.GetValues(typeof(MonsterState));
        for (int i = 0; i < stateValues.Length; i++)
        {
            System.Type FSMType = System.Type.GetType("CMonster" + stateValues[i].ToString());
            CMonsterState state = (CMonsterState)GetComponent(FSMType);
            if (null == state)
            {
                state = (CMonsterState)gameObject.AddComponent(FSMType);
            }

            state.SetManager(this);
            _states.Add(stateValues[i], state);
            state.enabled = false;
        }
    }

    public void MonsterAttackCol(int _Event)
    {
        switch (_Event)
        {
            case 0:
                _MAttackCol.enabled = false;

                break;
            case 1:
                _MAttackCol.enabled = true;
                break;
        }
    }

    private void GameOver()
    {
        if (CBossManager.Instance.CurrentState == BossState.DEAD || UIManager.isGameOver)
            _rigid.isKinematic = true;
    }

    private void Dead()
    {
            if (_Mstat.CurHP <= 0 &&
            CurrentState != MonsterState.SKILL_DEAD ||
            CBossManager.Instance.CurrentState == BossState.DEAD)
            {

            SetState(MonsterState.DEAD);
            _SlowEffect.gameObject.SetActive(false);
            if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0 &&
                    Anim.GetCurrentAnimatorStateInfo(0).IsName("DEAD"))
                {
                    DeadDelay -= 0.3f * Time.deltaTime;
                    ShaderTr.GetComponentInChildren<Renderer>().material.SetFloat("_Dissolve", DeadDelay);
                    if (DeadDelay <= -0.1f)
                        gameObject.SetActive(false);
                }
            }        
    }

    private void SkillDead()
    {
        if (_Mstat.CurHP <= 0 && CurrentState == MonsterState.SKILL_DEAD && CPlayerManager.Instance.CurrentState != PlayerState.ULTSKILL)
        {
            if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f &&
               Anim.GetCurrentAnimatorStateInfo(0).IsName("SKILL_DEAD"))
            {
                Debug.Log("스킬 ");
                _SlowEffect.gameObject.SetActive(false);
                DeadDelay -= 0.3f * Time.deltaTime;
                ShaderTr.GetComponent<Renderer>().material.SetFloat("_Dissolve", DeadDelay);
                if (DeadDelay <= -0.1f)
                    gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {

        if (CurrentState == MonsterState.READY)
            return;
        if (col.CompareTag("PWeapon") && CurrentState != MonsterState.DEAD)
        {
            _cmonstersound.HitSound();
            CPlayerManager.Instance.PlusSoul();
            //MHitEffect();
            iEffectCnt++;
            pCameraShake.OnShaking(0.1f, 0.1f,
                  new Vector3(Random.insideUnitSphere.x, Random.insideUnitSphere.y, Random.insideUnitSphere.z), true);
            LeftRightHitEffect();
            _Mstat.CurHP -= CPlayerManager.Instance.GetStat.Damage;
            SetState(MonsterState.HIT);
            StartCoroutine(HitShader());
            _rigid.AddForce(-transform.forward * 3, ForceMode.Impulse);

        }
        else if (col.CompareTag("Bomb") && (CurrentState != MonsterState.READY))
        {
            _rigid.AddForce(-transform.forward * 10, ForceMode.Impulse);
            _Mstat.CurHP -= GameObject.FindWithTag("Plantbomb").GetComponent<PlantManager>().GetStat.Damage;
        }
        else if (col.CompareTag("PSkill"))// 몬스터 쳐맞았을때
        {
            _cmonstersound.UltHitSound();
            LeftRightHitEffect();
            _Mstat.CurHP -= CPlayerManager.Instance.GetStat.SkillDamage;
        }
        else if (col.CompareTag("PSkillEvent"))
        {
            SetState(MonsterState.SLOW);
            //isHitSkill();
            StartCoroutine(MonsterStop());
        }
        else if (col.CompareTag("SlowSkill"))
        {
            //StartCoroutine(HitSlowSkill());
            _SlowEffect.gameObject.SetActive(true);
            _Mstat.CurSlowTime = _Mstat.MaxSlowTime;
            bSlow = true;
        }
        else if (col.CompareTag("RushSkill"))
        {
            _cmonstersound.HitSound();
            SetState(MonsterState.HIT);
            pCameraShake.OnShaking(0.3f, 0.3f,
              new Vector3(Random.insideUnitSphere.x, Random.insideUnitSphere.y,Random.insideUnitSphere.z), true);
            LeftRightHitEffect();
            //MHitEffect();
            _Mstat.CurHP -= CPlayerManager.Instance.GetStat.RushDamage;
        }
        else if (col.CompareTag("Search") && CurrentState != MonsterState.READY)
        {
            _cmonstersound.MonsterVoice();  //Debug.Log("몬스터 생성 사운드");
        }
    }

    //private IEnumerator HitSlowSkill()
    //{
    //    transform.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", 1);
    //    _Mstat.CurMoveSpeed = _Mstat.MaxMoveSpeed / 2;
    //    yield return new WaitForSeconds(_Mstat.SlowTime);
    //    transform.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", 0);
    //    _Mstat.CurMoveSpeed = _Mstat.MaxMoveSpeed;
    //}


    public GameObject[] _EffectOBJ , EffectLeftorRight;
    public Transform _WeaponTr;
    public Transform _EffectManager;

    public void MHitEffect()
    {
        GameObject Effet = Instantiate(_EffectOBJ[0], transform.position, transform.rotation);
        Effet.transform.parent = _EffectManager;
        Destroy(Effet, 1f);
    }

    public void MSlowDebuffEffect()
    {
        GameObject Effect = Instantiate(_EffectOBJ[1], transform.position, transform.rotation);
        Effect.transform.parent = this.transform;
        Destroy(Effect, _Mstat.CurSlowTime);
       
    }

    public void MonsterRushHit()
    {
        GameObject Effet = Instantiate(EffectLeftorRight[0], transform.position, transform.rotation);
        Effet.transform.parent = _EffectManager;
        Destroy(Effet, 1f);
    }

    public void LeftRightHitEffect()
    {
        if (iEffectCnt % 2 == 0)
        {
            GameObject Effet_Left = Instantiate(EffectLeftorRight[1], transform.position, transform.rotation);
            Effet_Left.transform.parent = _EffectManager;
            Destroy(Effet_Left, 1f);
        }
        else
        {
            GameObject Effet_Right = Instantiate(EffectLeftorRight[0], transform.position, transform.rotation);
            Effet_Right.transform.parent = _EffectManager;
            Destroy(Effet_Right, 1f);
        }

    }

    private bool bHitSkill;

    private IEnumerator MonsterStop()
    {
        bHitSkill = true;
        _rigid.isKinematic = true;
        yield return new WaitForSeconds(0.3f);
        bHitSkill = false;
        yield return new WaitForSeconds(3.5f);
        _rigid.isKinematic = false;
    }
    private IEnumerator HitShader()
    {
        transform.GetComponentInChildren<Renderer>().material.SetFloat("_SickButton", 1);
        yield return new WaitForSeconds(0.3f);
        transform.GetComponentInChildren<Renderer>().material.SetFloat("_SickButton", 0);
    }

    private void isHitSkill()
    {
        if (bHitSkill)
            _rigid.AddForce(_Target.position - transform.position, ForceMode.Acceleration);
    }

    public void Player_Checking()
    {
        _vDirection = _pPlayer.transform.position - transform.position;
        _vNormalDirection = _vDirection.normalized;
        float fDot = Vector3.Dot(transform.forward, _vNormalDirection);

        // 내적 각도를 디그리 형식으로 표현
        _fCurAngle = Mathf.Acos(fDot) * Mathf.Rad2Deg;
        _fCurDistance = Vector3.Distance(_pPlayer.transform.position, transform.position);
        _fFixedAttackDistance = _fMaxAttackDistance - _fMinAttackDistnace;
    }
}
