using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjetStat
{
    public float CurHP;       // 현재 체력
    public float MaxHP;       // 최대 체력
    public float CurSoul;     // 현재 소울
    public float MaxSoul;     // 최대 소울
    public float MoveSpeed;   // 캐릭터 속도
    public float RotSpeed;    // 캐릭터 회전 속도
    public float DashSpeed;   // 대쉬 속도
    public float Damage;      // 데미지    
    public float SkillDamage; // 스킬 데미지
    public float RushDamage;  // 돌진 스킬 데미지
    public float DashDelay;   // 대쉬 딜레이
    public float CurDashGauge;// 현재 대쉬 게이지
    public float MaxDashGauge;// 최대 대쉬 게이지
    public float MaxSafeTime;   // 무적 시간
    // ... 
}
public enum PlayerState
{
    IDLE = 0,    //  0.아이들
    RUN,         //  1.런
    ATTACK1,     //  2. 일반공격1
    ATTACK2,     //  3. 일반공격2
    ATTACK3,     //  4. 일반공격3
    DASH,        //  5. 대쉬
    HIT,         //  6. 피격
    DEAD,        //  7. 사망
    ULTSKILL,    //  8. 스킬
    SLOWSKILL,   //  9. 슬로우 디버프
    RUSHSKILL,   // 10. 돌진 공격
}

public class CPlayerManager : MonoBehaviour
{
    private static CPlayerManager _instance;
    public static CPlayerManager Instance { get { return _instance; } }

    [SerializeField]
    private ObjetStat _stat;
    public ObjetStat GetStat { get { return _stat; } set { _stat = value; } }

    // boolean
    private bool _isinit = false;

    private bool _isGrounded = false;
    public bool isGrounded { get { return _isGrounded; } }

    public PlayerState StartState = PlayerState.IDLE;
    private Dictionary<PlayerState, CPlayerState>
    _states = new Dictionary<PlayerState, CPlayerState>();

    private PlayerState _currentState;
    public PlayerState CurrentState { get { return _currentState; } }

    public CPlayerState CurrentStateComponent
    { get { return _states[_currentState]; } }

    private Animator _anim;
    public Animator Anim { get { return _anim; } }

    private Rigidbody _rigid;
    public Rigidbody Rigid { get { return _rigid; } }

    private Transform _CameraTr;
    public Transform CameraTr { get { return _CameraTr; } }

    private Vector3 _moveDir;
    public Vector3 MoveDir { get { return _moveDir; } set { _moveDir = value; } }


    private Vector3 vNomalDirection;
    public Vector3 GetNomalDirection { get { return vNomalDirection; } }


    private bool _bDead;
    public bool BDead { get { return _bDead; } set { _bDead = true; } }
    private float _AttackDelay;
    public float AttackDelay
    {
        get { return _AttackDelay; }
        set { _AttackDelay = value; }
    }

    private int _AttackCount;
    public int AttackCount
    {
        get { return _AttackCount; }
        set { _AttackCount = value; }
    }

    private const int _MaxAttackCount = 3;
    public int MaxAttackCount
    {
        get { return _MaxAttackCount; }
    }

    private float _DashPower;
    public float DashPower { get { return _DashPower; } set { _DashPower = value; } }


    private float _Gravity;
    public float Gravity { get { return _Gravity; } set { _Gravity = value; } }

    public float GetHorizontal { get { return Input.GetAxisRaw("Horizontal"); } }
    public float GetVertical { get { return Input.GetAxisRaw("Vertical"); } }

    private bool _bAttackEnd = false;
    public bool BAttackEnd { get { return _bAttackEnd; } }

    private bool _bHitTargetSkill;
    public bool BHitTargetSkill => _bHitTargetSkill;

    private SkinnedMeshRenderer _MeshRenderer;
    public SkinnedMeshRenderer MeshRenderer { get { return _MeshRenderer; } }

    public Transform[] ShaderTr;

    public Transform MousePoint;

    public Transform AttackDirPoint;

    public Transform AttackDirCircle;

    public Transform[] ChangeCamera;

    public Transform StartTr;

    public Transform UltStartScene;

    public Transform TargetTr;

    public GameObject HitObj;

    public Collider[] _AttackCol;

    public bool _isUlt;

    // 몬스터한테 공격 받았을 때    
    public float _safeTime;
    public bool bhit;

    public int layerMask;

    private Vector3 _Direction;
    public Vector3 Direction { get { return _Direction; } }

    private Vector3 _MousePos;
    public Vector3 MousePos { get { return _MousePos; } }

    [SerializeField]
    private GameObject _SlowDebuff;
    

    private void Awake()
    {
        transform.position = new Vector3(-188.47f, 0.7f, 3.41f);
        transform.eulerAngles = new Vector3(0.0f, 80.0f, 0.0f);
        _instance = this;
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _CameraTr = GameObject.FindWithTag("MainCamera").transform;
        _stat.CurHP = _stat.MaxHP;
        _stat.CurDashGauge = _stat.MaxDashGauge;
        PlayerStateVal();
        layerMask = (1 << 16);
        HitObj.SetActive(false);
        Cursor.visible = true; // 지우의 요청으로 커서 보이게함.
        UltMeshFalse();
    }

    private void Start()
    {
        _isinit = true;
        SetState(StartState);
    }

    private void Update()
    {
        CheckGround();
        IgnoreCollision();
        ChargingDashGauge();
        SetDeadState();
        RushDirection();
        SkillUIEnable();
        _stat.CurSoul = Mathf.Clamp(_stat.CurSoul, 0, _stat.MaxSoul);
        SafeDelay();
        _safeTime = Mathf.Clamp(_safeTime, 0, _stat.MaxSafeTime);
        GameClear();
    }

    private void GameClear()
    {
        if (CBossManager.Instance.isPlayerStop)
            SetState(PlayerState.IDLE);
    }

    private void SkillUIEnable()
    {
        UIManager.Instance.RushIcon.enabled = _stat.CurSoul >= 10 ? true : false;
        UIManager.Instance.RushActive.SetActive(_stat.CurSoul >= 10 ? true : false);
        UIManager.Instance.SlowIcon.enabled = _stat.CurSoul >= 30 ? true : false;
        UIManager.Instance.SlowActive.SetActive(_stat.CurSoul >= 30 ? true : false);
        UIManager.Instance.UltIcon.enabled = _stat.CurSoul >= 100 ? true : false;
        UIManager.Instance.UltActive.SetActive(_stat.CurSoul >= 100 ? true : false);        
    }

    private void SafeDelay()
    {
        if (!bhit)
        {
            _safeTime += Time.deltaTime;
            if (_safeTime >= _stat.MaxSafeTime)
            {
                bhit = true;
                _safeTime = 0;
            }
        }
    }

    public void UltMeshFalse()
    {
        for (int i = 0; i < ShaderTr.Length; i++)
        {
            _MeshRenderer = ShaderTr[i].GetComponent<SkinnedMeshRenderer>();
            _MeshRenderer.enabled = false;
        }
        AttackDirCircle.gameObject.SetActive(false);
        AttackDirPoint.gameObject.SetActive(false);
    }

    public void UltMeshTrue()
    {
        for (int i = 0; i < ShaderTr.Length; i++)
        {
            _MeshRenderer = ShaderTr[i].GetComponent<SkinnedMeshRenderer>();
            _MeshRenderer.enabled = true;
        }        
        AttackDirCircle.gameObject.SetActive(true);
        AttackDirPoint.gameObject.SetActive(true);
    }

    private void IgnoreCollision()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("MONSTER"), LayerMask.NameToLayer("PLAYER"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("ENEMY"), LayerMask.NameToLayer("PLAYER"), true);
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("BOSS"), LayerMask.NameToLayer("PLAYER"), true);
    }

    public void SetState(PlayerState newState)
    {
        if (_isinit)
        {
            _states[_currentState].enabled = false;
            _states[_currentState].EndState();
        }
        _currentState = newState;
        _states[_currentState].BeginState();
        _states[_currentState].enabled = true;
        _anim.SetInteger("PlayerState", (int)_currentState);
    }

    private void PlayerStateVal()
    {
        PlayerState[] stateValues = (PlayerState[])System.Enum.GetValues(typeof(PlayerState));
        for (int i = 0; i < stateValues.Length; i++)
        {
            System.Type FSMType = System.Type.GetType("CPlayer" + stateValues[i].ToString());
            CPlayerState state = (CPlayerState)GetComponent(FSMType);
            if (null == state)
            {
                state = (CPlayerState)gameObject.AddComponent(FSMType);
            }

            state.SetManager(this);
            _states.Add(stateValues[i], state);
            state.enabled = false;
        }
    }
    public void NextState(int _event)
    {
        if (_event == 1)
            _bAttackEnd = true;
    }

    public void SetDeadState()
    {
        if (_stat.CurHP <= 0 && CBossManager.Instance.CurrentState != BossState.DEAD)
        {
            UIManager.isGameOver = true;
            SetState(PlayerState.DEAD);
        }
        else if (UIManager.isGameOver)
            _stat.MoveSpeed = 0;
        else
            UIManager.isGameOver = false;
    }

    public void SlowSpeed(bool _Event)
    {
        if (_Event)
            _stat.MoveSpeed = 3;
        else if (!_Event)
            _stat.MoveSpeed = 10;
    }

    public void ChargingDashGauge()
    {
        if (_stat.CurDashGauge < 3)
            _stat.CurDashGauge += 0.5f * Time.deltaTime;
    }

    public void MinusDashGauge()
    {
        _stat.CurDashGauge -= 1;
    }
    public void CheckGround()
    {
        RaycastHit _hit;
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * 0.3f, Color.red);

        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out _hit, 0.3f))
        {
            _isGrounded = true;
            return;
        }

        _isGrounded = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "Bomb":
                if (CurrentState != PlayerState.ULTSKILL &&
                    CurrentState != PlayerState.RUSHSKILL &&                                        
                    !_isUlt)
                {
                    _rigid.AddForce(-transform.forward * 6, ForceMode.Impulse);
                    StartCoroutine(PlayerHit());
                    StartCoroutine(HitTargetSkill());
                    _stat.CurHP -= GameObject.FindWithTag("Plantbomb").GetComponent<PlantManager>().GetStat.Damage;
                    bhit = false;
                    SetState(PlayerState.HIT);
                    UIManager.Instance.PlayerHitHPColor();
                }
                break;
            case "MWeapon":
                //_stat.CurHP -= (_stat.CurHP - CMonsterManager.Instance.GetMstat.Damage) < 0 ? 0 : CMonsterManager.Instance.GetMstat.Damage;
                if (CurrentState != PlayerState.ULTSKILL &&
                    CurrentState != PlayerState.SLOWSKILL &&
                    CurrentState != PlayerState.RUSHSKILL &&
                    bhit &&
                    _safeTime == 0 &&
                    !_isUlt)
                {
                    _stat.CurHP -= GameObject.FindWithTag("Monster").GetComponent<CMonsterManager>().GetMstat.Damage;
                    StartCoroutine(PlayerHit());
                    SetState(PlayerState.HIT);
                    bhit = false;
                    UIManager.Instance.PlayerHitHPColor();
                }
                break;
            case "MWeapon2":
                if (CurrentState != PlayerState.ULTSKILL &&
                    CurrentState != PlayerState.SLOWSKILL &&
                    CurrentState != PlayerState.RUSHSKILL &&
                    bhit &&
                    _safeTime == 0 &&
                    !_isUlt)
                {
                    _stat.CurHP -= GameObject.FindWithTag("GreenEnemy").GetComponent<CEnemyManager>().GetInfo.Damage;
                    StartCoroutine(PlayerHit());
                    bhit = false;
                    _safeTime = 0;
                    SetState(PlayerState.HIT);
                    UIManager.Instance.PlayerHitHPColor();
                }
                break;

            case "Bullet":
                if (CurrentState != PlayerState.ULTSKILL &&
                   CurrentState != PlayerState.SLOWSKILL &&
                   CurrentState != PlayerState.RUSHSKILL &&
                   bhit &&
                    _safeTime == 0 &&
                    !_isUlt)
                {
                    _stat.CurHP -= 1.5f;
                    bhit = false;
                    StartCoroutine(PlayerHit());
                    UIManager.Instance.PlayerHitHPColor();
                }
                break;
            case "BossPush":
                if (!_isUlt &&
                    CurrentState != PlayerState.ULTSKILL &&
                    CurrentState != PlayerState.SLOWSKILL &&
                    CurrentState != PlayerState.RUSHSKILL)
                {
                    //_rigid.AddForce((transform.position - TargetTr.position).normalized * 10,ForceMode.Impulse);
                    _rigid.AddRelativeForce((transform.position - TargetTr.position).normalized * 500);
                    bhit = false;
                    _stat.CurHP -= 3;
                    SetState(PlayerState.HIT);
                    StartCoroutine(PlayerHit());
                    StartCoroutine(HitTargetSkill());
                    UIManager.Instance.PlayerHitHPColor();
                }
                break;
        }
    }

    private IEnumerator PlayerHit()
    {
        for (int i = 0; i < ShaderTr.Length; i++)
        {
            ShaderTr[i].GetComponent<Renderer>().material.SetFloat("_SickButton", 1);
        }
        HitObj.SetActive(true);
        yield return new WaitForSeconds(0.15f);

        for (int i = 0; i < ShaderTr.Length; i++)
        {
            ShaderTr[i].GetComponent<Renderer>().material.SetFloat("_SickButton", 0);
        }
        yield return new WaitForSeconds(0.5f);
        HitObj.SetActive(false);
    }

    public IEnumerator HitTargetSkill()
    {
        _bHitTargetSkill = true;
        _rigid.constraints = RigidbodyConstraints.FreezeRotation |
            RigidbodyConstraints.FreezePositionY;
        yield return new WaitForSeconds(1f);
        _bHitTargetSkill = false;
        _rigid.constraints = RigidbodyConstraints.FreezeRotation;
    }


    public void PlayerAttackRotate()
    {
        _Direction = MousePoint.position - transform.position;
        if (BDead || UIManager.Instance.isPause || CBossManager.Instance.isPlayerStop || CActionBossScene.btutorialUI)
            return;
        if (Input.GetKeyDown(KeyOption.AttackKey))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit, 1000, layerMask))
            {
                if (hit.transform.gameObject.layer == 16)
                {
                    MousePoint.position = hit.point;
                    vNomalDirection = (MousePoint.position - transform.position).normalized;
                    vNomalDirection.y = 0; // y값안죽이면 회전함.
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vNomalDirection), 1f);
                }
            }
        }
    }

    Vector3 AttackDir;

    public void RushDirection()
    {
        AttackDir = MousePoint.position - AttackDirPoint.position;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (UIManager.Instance.isPause || 
            UIManager.isGameOver ||
            CActionBossScene.btutorialUI || CBossManager.Instance.isPlayerStop)
            return;
        if (Physics.Raycast(r, out hit, 1000, layerMask))
        {
            if (hit.transform.gameObject.layer == 16)
            {
                MousePoint.position = hit.point;
                AttackDirPoint.eulerAngles = new Vector3(0, MousePoint.rotation.y, 0);
                AttackDirPoint.rotation = Quaternion.Slerp(AttackDirPoint.rotation, Quaternion.LookRotation(AttackDir), 1f);

                AttackDirCircle.position = transform.position;
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        switch (col.tag)
        {
            case "GROUND":
                SlowSpeed(_Event: false);
                _anim.speed = 1;
                _SlowDebuff.SetActive(false);
                break;
        }
    }

    private void OnCollisionStay(Collision col)
    {
        switch (col.collider.tag)
        {
            case "SlowFloor":
                _SlowDebuff.SetActive(true);
                if (CurrentState == PlayerState.RUN || CurrentState == PlayerState.IDLE)
                {
                    SlowSpeed(_Event: true);
                    _anim.speed = 0.5f;
                }
                break;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        switch (col.collider.tag)
        {
            case "SlowFloor":
                SlowSpeed(_Event: false);
                _anim.speed = 1;
                _SlowDebuff.SetActive(false);
                break;
        }
    }

    public void RushEnd()
    {
        _rigid.AddForce(transform.forward * 5, ForceMode.Impulse);
    }

    public void PlusSoul()
    {
        _stat.CurSoul += 1.5f;
    }

    public void UltMinusSoul()
    {
        _stat.CurSoul -= 100;
    }

    public void SlowMinusSoul()
    {
        _stat.CurSoul -= 30;
    }

    public void RushMinusSoul()
    {
        _stat.CurSoul -= 10;
    }

}
