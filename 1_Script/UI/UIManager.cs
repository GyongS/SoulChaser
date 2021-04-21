using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CFillAmount
{
    public Image _PlayerHPBack;
    public Image _PlayerHP;
    public Image _TargetHPBack;
    public Image _TargetHP;

    [System.NonSerialized]
    public float _targetfill;
    public float _bossfill;

    public float _PlayerBackFill;
    public float _TargetBackFill;

    private float fill = 1;
    private float Targetfill = 1;

    public void PlayerBackHp(float _fill = 0f)
    {
        if (fill < (_fill - Time.deltaTime))
            fill += Time.deltaTime;
        else if (fill > (_fill + Time.deltaTime))
            fill -= Time.deltaTime;
        else
            fill = _fill;

        _PlayerHPBack.fillAmount = fill;
        _PlayerBackFill = _fill;
        _PlayerHP.fillAmount = Mathf.Lerp(_PlayerHP.fillAmount, _PlayerBackFill, Time.deltaTime * 1);
    }

    public void BossBackHp(float _bossfill = 0f)
    {
        if (Targetfill < (_bossfill - Time.deltaTime))
            Targetfill += Time.deltaTime;
        else if (Targetfill > (_bossfill + Time.deltaTime))
            Targetfill -= Time.deltaTime;
        else
            Targetfill = _bossfill;

        _TargetHPBack.fillAmount = Targetfill;
        _TargetBackFill = _bossfill;
        _TargetHP.fillAmount = Mathf.Lerp(_TargetHP.fillAmount, _TargetBackFill, Time.deltaTime * 1);
    }
}

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    public CFillAmount UIHP;

    [SerializeField]
    private Image _PlayerHP;
    public Image PlayerHP { get { return _PlayerHP; } }

    [SerializeField]
    private Image _PlayerSoul;
    public Image PlayerSoul { get { return _PlayerSoul; } }

    [SerializeField]
    private Image _BossHP;
    public Image BossHP { get { return _BossHP; } }

    [SerializeField]
    private Image _BossHPBack;

    [SerializeField]
    private Image _BossEmpty;

    [SerializeField]
    private Image _RushIcon;
    public Image RushIcon => _RushIcon;

    [SerializeField]
    private GameObject _RushActive;
    public GameObject RushActive => _RushActive;

    [SerializeField]
    private Image _SlowIcon;
    public Image SlowIcon => _SlowIcon;

    [SerializeField]
    private GameObject _SlowActive;
    public GameObject SlowActive => _SlowActive;

    [SerializeField]
    private Image _UltIcon;
    public Image UltIcon => _UltIcon;

    [SerializeField]
    private GameObject _UltActive;
    public GameObject UltActive => _UltActive;

    [SerializeField]
    private Image _DashGauge;
    public Image DashGauge { get { return _DashGauge; } }

    [SerializeField]
    private Image[] _GameOver;
    public Image[] GameOver { get { return _GameOver; } }

    [SerializeField]
    private Text _TatgetHPText;

    [SerializeField]
    private int _TatgetHPCount;
    public int TargetHPCount { get { return _TatgetHPCount; } set { _TatgetHPCount = value; } }

    [SerializeField]
    private GameObject _ESC;

    [SerializeField]
    private Image[] _TargetChangeHP;

    public GameObject TutorialUI;

    [SerializeField]
    GameObject _GameoverOBJ;

    [SerializeField]
    AudioClip _GameOverSound;

    [SerializeField]
    private Image _Fadein;
    private float _Fadetime = 1;
    private float _GameOverTime;
    private float _GameOverBG;

    private int _TargetHPColorCnt = 0;

    private int _TutorialCnt = 0;

    private bool _bFade;

    public static bool isGameOver;

    private void Awake()
    {
        _instance = this;
        TutorialUI.SetActive(false);
        isGameOver = false;
        UIHP._TargetHPBack.color = new Color(_BossHP.color.r, _BossHP.color.g, _BossHP.color.b, 0.7f);
        _TutorialCnt = 0;
    }

    private void Update()
    {

        // 플레이어 체력
        _PlayerHP.fillAmount = CPlayerManager.Instance.GetStat.CurHP / CPlayerManager.Instance.GetStat.MaxHP;
        UIHP.PlayerBackHp(_PlayerHP.fillAmount);

        // 플레이어 소울
        _PlayerSoul.fillAmount = CPlayerManager.Instance.GetStat.CurSoul / CPlayerManager.Instance.GetStat.MaxSoul;

        // 보스 체력
        _BossHP.fillAmount = CBossManager.Instance.GetBStat.CurHP / CBossManager.Instance.GetBStat.MaxHP;
        UIHP.BossBackHp(_BossHP.fillAmount);

        // 대쉬 게이지
        _DashGauge.fillAmount = CPlayerManager.Instance.GetStat.CurDashGauge / CPlayerManager.Instance.GetStat.MaxDashGauge;

        // 보스 HP 카운트
        _TatgetHPText.text = "x" + TargetHPCount;

        TargetHPColorChange();
        PauseUI();
        IngameFadein();
        IngameFadeOut();
        GameOverFade();
        MinusTargetHPCount();
        _GameOverBG = Mathf.Clamp(_GameOverBG, 0, 0.9f);
        _Fadetime = Mathf.Clamp(_Fadetime, 0, 1);
    }

    private bool _isPause;
    public bool isPause { get { return _isPause; } set { _isPause = false; } }
    public void PauseUI()
    {
        if (isGameOver || CBossManager.Instance.CurrentState == BossState.READY)
            return;
        if (Input.GetKeyDown(KeyOption.ESC) && CBossManager.Instance.CurrentState != BossState.DEAD
            && !CActionBossScene.btutorialUI)
        {
            _isPause = true;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else if (!_isPause)
        {
            _ESC.SetActive(false);
            Time.timeScale = 1;
        }
        else if (_isPause)
        {
            _ESC.SetActive(true);
        }
    }


    public void GameOverFade()
    {
        if (_GameOverTime >= 1 && _GameOverBG >= 0.9f)
        {
            _GameoverOBJ.SetActive(true);
            Cursor.visible = true;
            return;
        }
        else if (isGameOver)
        {
            _GameOver[1].gameObject.GetComponent<AudioSource>().PlayOneShot(_GameOverSound, 0.4f);
            _GameOverTime += 0.2f * Time.deltaTime;
            _GameOverBG += 0.3f * Time.deltaTime;
            _GameOver[1].color = new Color(1, 1, 1, _GameOverTime);
            _GameOver[0].color = new Color(0, 0, 0, _GameOverBG);
        }
    }

    private void IngameFadein()
    {
        if (_bFade)
            return;
        _Fadetime -= Time.deltaTime;
        _Fadein.color = new Color(0, 0, 0, _Fadetime);
        if (_Fadetime <= 0)
        {
            if (CActionBossScene.btutorialUI && _TutorialCnt <= 0)
            {
                Time.timeScale = 0;
                if (Input.GetKeyDown(KeyOption.ESC))
                {
                    Time.timeScale = 1;
                    _TutorialCnt = 1;
                    TutorialUI.SetActive(false);
                    CActionBossScene.btutorialUI = false;
                    _bFade = true;
                }
            }
        }

    }

    public void IngameFadeOut()
    {
        if (CBossManager.Instance.isDead)
        {
            _Fadetime += Time.deltaTime;
            _Fadein.color = new Color(0, 0, 0, _Fadetime);
            if (_Fadetime >= 1)
                SceneManager.LoadScene("StageClear");
            return;
        }

    }


    private void MinusTargetHPCount()
    {
        if (CBossManager.Instance.GetBStat.CurHP <= 0 && _TatgetHPCount <= 0)
            return;
        if (CBossManager.Instance.GetBStat.CurHP <= 0)
        {
            _TatgetHPCount -= 1;
            _TargetHPColorCnt += 1;
            CBossManager.Instance.initTargetHP();
            _BossHP.sprite = _BossEmpty.sprite;
            _BossHP.fillAmount = 1;
            _BossHPBack.fillAmount = 1;
            if (_TatgetHPCount <= 0)
            {
                _TatgetHPCount = 0;
                _TargetHPColorCnt = -1;
            }
        }
    }

    private void TargetHPColorChange()
    {
        switch (_TargetHPColorCnt)
        {
            case -1:
                _BossEmpty.color = new Color(0, 0, 0, 1);
                break;
            case 0:
                _BossEmpty.sprite = _TargetChangeHP[1].sprite;
                break;
            case 1:
                _BossEmpty.sprite = _TargetChangeHP[2].sprite;
                break;
            case 2:
                _BossEmpty.sprite = _TargetChangeHP[3].sprite;
                break;
            case 3:
                _BossEmpty.sprite = _TargetChangeHP[0].sprite;
                break;
            case 4:
                _TargetHPColorCnt = 0;
                break;
        }
    }

    public void TargetHitHpColor()
    {
        StartCoroutine(HitHPColor());
    }

    IEnumerator HitHPColor()
    {
        _BossHP.color = new Color(1, 1, 1, 0.3f);
        _BossEmpty.color = new Color(1, 1, 1, 0.3f);
        yield return new WaitForSeconds(0.1f);
        _BossHP.color = new Color(1, 1, 1, 1.0f);
        _BossEmpty.color = new Color(1, 1, 1, 1.0f);
    }

    public void PlayerHitHPColor()
    {
        StartCoroutine(PHitHPColor());
    }

    IEnumerator PHitHPColor()
    {
        _PlayerHP.color = new Color(1, 1, 1, 0.3f);
        yield return new WaitForSeconds(0.1f);
        _PlayerHP.color = new Color(1, 1, 1, 1);
    }

}
