using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[System.Serializable]
public struct INFO
{
    public float CurHP;
    public float MaxHP;
    public float CurMoveSpeed;
    public float MaxMoveSpeed;
    public float Damage;
    public float CurSlowTime;
    public float MaxSlowTime;
}

public enum EnemyState
{
    READY = 0,      // 0. 준비
    IDLE,           // 1. 대기
    CHASE,          // 2. 추격
    ATTACK,         // 3. 공격
    DEAD,           // 4. 사망    
    HIT,            // 5. 피격
    SLOW,           // 6. 궁맞았을때 느리게 멈추는 상태
    SKILL_DEAD,     // 7. 막타 맞을때
    NavCHASE,       // 8. NAV
    DELEY ,          // 9.
    HIDE             //10.
}

public class CEnemyManager : MonoBehaviour
{

    private bool _isinit = false;

    private Dictionary<EnemyState, CEnemyState>
       _enemy = new Dictionary<EnemyState, CEnemyState>();

    private Dictionary<EnemyState, CKogMawState>
        _KogMaw = new Dictionary<EnemyState, CKogMawState>();


    private EnemyState _enemyCurrentState;
    public EnemyState enemyCurrentState { get { return _enemyCurrentState; } }

    private EnemyState _enemyPrevState;
    public EnemyState enemyPrevState { get { return _enemyPrevState; } }


    private int iEffectCnt = 0;

    private NavMeshAgent _pNavMesh;
    public NavMeshAgent pNavMesh { get { return _pNavMesh; } }


    [SerializeField]
    private INFO m_Info;
    public INFO GetInfo { get { return m_Info; } set { m_Info = value; } }

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
    private string szObjName;

    CMonsterSound _cmonstersound;

    public float fAngleMax = 0;
    public float fDistanceMax = 0;
    [SerializeField] private float _fCurAngle;
    public float GetAngle { get { return _fCurAngle; } }

    [SerializeField] private float _fCurDistance;
    public float GetDistance { get { return _fCurDistance; } }

    public Transform ShaderTr;
    private CCameraShake pCameraShake;

    public Transform _SlowEffect;
    private bool bSlow;
    // GameObject Instance 
    public GameObject _pMonsterBullet;

    [Header("MonsterShader")]
    private float _fMonsterShader;

    private float DeadDelay;
    [SerializeField] private float _fMinAttackDistnace, _fMaxAttackDistance, _fFixedAttackDistance;
    public float GetMinAttackDistance { get { return _fMinAttackDistnace; } }
    public float GetMaxAttackDistance { get { return _fMaxAttackDistance; } }
    public float GetFixedAttackDistance { get { return _fFixedAttackDistance; } }


    private void Ready_Enemy()
    {

        EnemyState[] stateValues = (EnemyState[])System.Enum.GetValues(typeof(EnemyState));
        for (int i = 0; i < stateValues.Length; i++)
        {
            System.Type FSMType = System.Type.GetType("CEnemy" + stateValues[i].ToString());
            CEnemyState state = (CEnemyState)GetComponent(FSMType);
            if (null == state)
            {
                state = (CEnemyState)gameObject.AddComponent(FSMType);
            }
            _enemy.Add(stateValues[i], state);
            state.enabled = false;
        }
    }
    private void Ready_Kogmaw()
    {
        EnemyState[] stateValues = (EnemyState[])System.Enum.GetValues(typeof(EnemyState));
        for (int i = 0; i < stateValues.Length; i++)
        {
            System.Type FSMType = System.Type.GetType("CKogMaw" + stateValues[i].ToString());
            CKogMawState state = (CKogMawState)GetComponent(FSMType);
            if (null == state)
            {
                state = (CKogMawState)gameObject.AddComponent(FSMType);
            }
            _KogMaw.Add(stateValues[i], state);
            state.enabled = false;
        }
    }

    public void SetState(EnemyState newState)
    {
        switch (szObjName)
        {
            case "KogMaw":
                if (_isinit)
                {
                    _KogMaw[_enemyCurrentState].enabled = false;
                    _KogMaw[_enemyCurrentState].EndState();
                }

                _enemyCurrentState = newState;
                _KogMaw[_enemyCurrentState].BeginState();
                _KogMaw[_enemyCurrentState].enabled = true;
                _anim.SetInteger("EnemyState", (int)_enemyCurrentState);
                _enemyPrevState = _enemyCurrentState;
                break;

            default:
                if (_isinit)
                {
                    _enemy[_enemyCurrentState].enabled = false;
                    _enemy[_enemyCurrentState].EndState();
                }

                _enemyCurrentState = newState;
                _enemy[_enemyCurrentState].BeginState();
                _enemy[_enemyCurrentState].enabled = true;
                _anim.SetInteger("EnemyState", (int)_enemyCurrentState);
                _enemyPrevState = _enemyCurrentState;
                break;
        }
       
    }

  
    private void Dead()
    {        
            if (m_Info.CurHP <= 0 && enemyPrevState != EnemyState.SKILL_DEAD ||
            CBossManager.Instance.CurrentState == BossState.DEAD)
            {
                SetState(EnemyState.DEAD);
            _SlowEffect.gameObject.SetActive(false);
                if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f &&
                   Anim.GetCurrentAnimatorStateInfo(0).IsName("DEAD"))
                {
                    DeadDelay -= 0.3f * Time.deltaTime;
                    ShaderTr.GetComponent<Renderer>().material.SetFloat("_Dissolve", DeadDelay);
                  //  Debug.Log(DeadDelay);
                    if (DeadDelay <= -0.1f)
                        gameObject.SetActive(false);
                }
            }        
    }

    private void SkillDead()
    {
            if (m_Info.CurHP <= 0 && enemyPrevState != EnemyState.DEAD)
            {
                if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f &&
                   Anim.GetCurrentAnimatorStateInfo(0).IsName("SKILL_DEAD"))
                {
                _SlowEffect.gameObject.SetActive(false);
                DeadDelay -= 0.3f * Time.deltaTime;
                    ShaderTr.GetComponent<Renderer>().material.SetFloat("_Dissolve", DeadDelay);
                    //Debug.Log(DeadDelay);
                    if (DeadDelay <= -0.1f)
                        gameObject.SetActive(false);
                }
            }        
    }


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _pPlayer = GameObject.FindGameObjectWithTag("Player");

        iEffectCnt = 0;
        DeadDelay = 0.5f;
        m_Info.CurHP = m_Info.MaxHP;
        m_Info.CurMoveSpeed = m_Info.MaxMoveSpeed;
        _cmonstersound = GetComponent<CMonsterSound>();
        _pNavMesh = GetComponent<NavMeshAgent>();
        _pNavMesh.stoppingDistance = _fMaxAttackDistance;
        szObjName = gameObject.tag;
        pCameraShake = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CCameraShake>();

        _SlowEffect = transform.GetChild(4).GetComponent<Transform>();
    }

    private void Start()
    {
        _isinit = true;
        switch (szObjName)
        {
            case "Enemy":
                _fMonsterShader = transform.GetComponentInChildren<Renderer>().material.GetFloat("_SickButton");
                Ready_Enemy();
                SetState(EnemyState.READY);
                break;
            case "GreenEnemy":
                _fMonsterShader = transform.GetComponentInChildren<Renderer>().material.GetFloat("_SickButton");
                Ready_Enemy();
                SetState(EnemyState.HIDE);
                break;
            case "KogMaw":
                Ready_Kogmaw();
                SetState(EnemyState.HIDE);
                break;
        }
    }

    private void Update()
    {
        SkillDead();
        Dead();
        isHitSkill();
        GameOver();
        SlowDebuff();
        MonsterColActive();
    }

    void MonsterColActive()
    {
        if (CPlayerManager.Instance.CurrentState == PlayerState.ULTSKILL)
            _MAttackCol.enabled = false;
    }

    private void SlowDebuff()
    {
        if (bSlow)
        {
            m_Info.CurSlowTime -= Time.deltaTime;
            transform.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", 1);
            m_Info.CurMoveSpeed = m_Info.MaxMoveSpeed / 2;
            if (m_Info.CurSlowTime <= 0)
            {
                _SlowEffect.gameObject.SetActive(false);
                transform.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", 0);
                m_Info.CurMoveSpeed = m_Info.MaxMoveSpeed;
                m_Info.CurSlowTime = 0;
                bSlow = false;
            }
        }
    }

    private void GameOver()
    {
        if (CBossManager.Instance.CurrentState == BossState.DEAD || UIManager.isGameOver)
            _rigid.isKinematic = true;
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

    private void OnTriggerEnter(Collider col)
    {
        if (enemyPrevState == EnemyState.HIDE || enemyCurrentState == EnemyState.HIDE)
            return;
        if (enemyCurrentState == EnemyState.DEAD)
            return;
     //   print(" 들어옴 ");
        if (col.CompareTag("PWeapon") && (enemyCurrentState != EnemyState.READY)
            && enemyCurrentState != EnemyState.DEAD)
        {
            _cmonstersound.HitSound();
            CPlayerManager.Instance.PlusSoul();
            //MHitEffect();
            iEffectCnt++;
            LeftRightHitEffect();
            m_Info.CurHP -= CPlayerManager.Instance.GetStat.Damage;
            pCameraShake.OnShaking(0.3f, 0.3f,
                   new Vector3(Random.insideUnitSphere.x, Random.insideUnitSphere.y, Random.insideUnitSphere.z), true);
            SetState(EnemyState.HIT);
            StartCoroutine(HitShader());
            _rigid.AddForce(-transform.forward * 3, ForceMode.Impulse);

        }
        else if (col.CompareTag("Bomb") && (enemyCurrentState != EnemyState.READY))
        {
            _rigid.AddForce(-transform.forward * 10, ForceMode.Impulse);
            m_Info.CurHP -= GameObject.FindWithTag("Plantbomb").GetComponent<PlantManager>().GetStat.Damage;
        }
        else if (col.CompareTag("PSkill"))// 몬스터 쳐맞았을때
        {
            _cmonstersound.UltHitSound();
            LeftRightHitEffect();
            m_Info.CurHP -= CPlayerManager.Instance.GetStat.SkillDamage;
        }
        else if (col.CompareTag("PSkillEvent"))
        {
            SetState(EnemyState.SLOW);
            isHitSkill();
            StartCoroutine(MonsterStop());
        }
        else if (col.CompareTag("SlowSkill"))
        {
            _SlowEffect.gameObject.SetActive(true);
            bSlow = true;
            m_Info.CurSlowTime = m_Info.MaxSlowTime;
            //StartCoroutine(HitSlowSkill());
        }
        else if (col.CompareTag("RushSkill"))
        {
            _cmonstersound.HitSound();
            SetState(EnemyState.HIT);
            pCameraShake.OnShaking(0.2f, 0.1f,
              new Vector3(Random.insideUnitSphere.x, Random.insideUnitSphere.y, Random.insideUnitSphere.z), true);
            LeftRightHitEffect();
            //MHitEffect();
            m_Info.CurHP -= CPlayerManager.Instance.GetStat.RushDamage;
        }
        else if (col.CompareTag("Search") && enemyCurrentState != EnemyState.READY)
        {
            _cmonstersound.MonsterVoice();  //Debug.Log("몬스터 생성 사운드");
        }
    }

    //private IEnumerator HitSlowSkill()
    //{
    //    transform.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", 1);
    //    m_Info.CurMoveSpeed = m_Info.MaxMoveSpeed / 2;
    //    yield return new WaitForSeconds(m_Info.SlowTime);
    //    transform.GetComponentInChildren<Renderer>().material.SetFloat("_SlowButton", 0);
    //    m_Info.CurMoveSpeed = m_Info.MaxMoveSpeed;
    //}

    //for 이펙트
    public GameObject[] _EffectOBJ, EffectLeftorRight;
    public Transform _EffectRot;
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
        Destroy(Effect, m_Info.CurSlowTime);
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
    //END
    // for. Skill hit 유무를 불값으로 확인 
    private bool bHitSkill;
    private IEnumerator MonsterStop()
    {
        bHitSkill = true;
        yield return new WaitForSeconds(0.2f);
        _rigid.isKinematic = true;
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
    // end
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
