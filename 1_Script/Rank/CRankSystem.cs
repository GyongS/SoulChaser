using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 2019 09 06 ing~ RankSystem _Gyong
public class CRankSystem : MonoBehaviour
{
    private static CRankSystem _instance;
    public static CRankSystem Instance => _instance;

    [SerializeField]
    GameObject _NameInputObj = null;

    [SerializeField]
    GameObject[] _RankObj = null;

    [SerializeField]
    Text _CurrenScoreText = null;

    [SerializeField]
    Text _CurrentTimeText = null;

    [SerializeField]
    public GameObject[] _RankFxObj = null;
        
    AudioSource _audio;

    [SerializeField]
    AudioClip[] _clip;


    // 컴포넌트
    private Animator _anim;

    // 1 ~ 8 위의 순위
    public static int _RankNum;

    public bool _bInputTextActive;
    public bool _bCheckData;
    public static bool _bCheck;

    // 점수 데이터 : 이름 / 점수
    public class ScoreData
    {
        public string Name;
        public string RankStr;
        public int Score;
        public int RankFxNum;
        public ScoreData(string _Name, string _RankStr, int _Score, int _RankFxNum)
        {
            Name = _Name;
            Score = _Score;
            RankStr = _RankStr;
            RankFxNum = _RankFxNum;
        }
    }

    [SerializeField]
    private static List<ScoreData> m_ScoreArray;
    public static List<ScoreData> ScoreArr { get { return m_ScoreArray; } }

    private void Awake()
    {
        _instance = this;
        _audio = GetComponent<AudioSource>();
        _audio.clip = _clip[Random.Range(0, 2)];
        _audio.Play();
        CUserName.bNameCanvas = false;
        _bCheckData = false;
        _bInputTextActive = false;
        _anim = GetComponentInChildren<Animator>();
        _CurrenScoreText.text = CTimerSystem.Instance.RankScore;
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "StageClear" &&
            CPlayerEndingAnim.Instance.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f &&
            CPlayerEndingAnim.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("EndingIDLE"))
        {
            _CurrenScoreText.gameObject.SetActive(true);

            if (true == ScoreCheck() &&
                !_bInputTextActive &&
                !CUserName.bNameCanvas &&
                CPlayerEndingAnim.Instance.bInputObj)
            {
                _NameInputObj.SetActive(true);
                _CurrentTimeText.text = CTimerSystem.Instance.RankScore;
            }
            else if (CUserName.bNameCanvas)
            {
                _NameInputObj.SetActive(false);              
            }
            else if (_bInputTextActive)
            {
                for (int i = 0; i < _RankObj.Length; i++)
                {
                    _RankObj[i].SetActive(true);
                }
            }
            else if(!_bInputTextActive)
            {
                for (int i = 0; i < _RankObj.Length; i++)
                {
                    _RankObj[i].SetActive(true);
                }
            }
        }
    }

    public static void ScoreLoad()
    {
        if (PlayerPrefs.HasKey("Name0") == true)
        {
            m_ScoreArray = new List<ScoreData>();

            // 시작 할 때 기존 데이터를 불러온다.

            for (int i = 0; i < 5; i++)
            {
                ScoreData NewScore = new ScoreData(PlayerPrefs.GetString("Name" + i, "-"),
                                PlayerPrefs.GetString("RankStr" + i, "-"),
                                PlayerPrefs.GetInt("Score" + i, 0),
                                i);
                m_ScoreArray.Add(NewScore);
            }
            return;
        }

        // 처음 시작 했을 때 List 생성
        m_ScoreArray = new List<ScoreData>();
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetString("Name" + i, "-");
            PlayerPrefs.SetString("RankStr" + i, "-");
            PlayerPrefs.SetInt("Score" + i, 0);
            ScoreData NewScore = new ScoreData("-", "-", 0, 0);
            m_ScoreArray.Add(NewScore);
        }
    }


    public static bool ScoreCheck()
    {
        for (int i = 0; i < ScoreArr.Count; i++)
        {
            if (0 == ScoreArr[i].Score)
            {
                return true;
            }
            else if (PlayerPrefs.HasKey("Name0") == true)
            {
                if ((int)CTimerSystem._HideScore <= ScoreArr[0].Score)
                {
                    return true;
                }
                else if ((int)CTimerSystem._HideScore < ScoreArr[i].Score)
                {
                    return true;
                }
                else if ((int)CTimerSystem._HideScore == ScoreArr[i].Score)
                {
                    return true;
                }
                else if ((int)CTimerSystem._HideScore == ScoreArr[4].Score &&
                    (int)CTimerSystem._HideScore > ScoreArr[4].Score)
                {
                    Instance._bInputTextActive = true;
                    return false;
                }
            }
        }
        return false;
    }


    // 게임 결과 저장
    public static void ScoreInput(string _Name, int _RankFxNum)
    {
        ScoreData CheckData = new ScoreData(_Name, CTimerSystem.Instance.RankScore, (int)CTimerSystem._HideScore, _RankFxNum);

        // 1 ~ 8위 점수 비교 
        for (int i = 0; i < ScoreArr.Count; i++)
        {
            if (ScoreArr[i].Score == 0)
            {
                if (CheckData.Score < ScoreArr[i].Score)
                {
                    ScoreData TempScore = ScoreArr[i];

                    // 새로운 데이터 체크
                    ScoreArr[i] = CheckData;

                    // 원래 데이터가 새로운 데이터가 됨
                    CheckData = TempScore;

                    if(!_bCheck)
                        _RankNum = TempScore.RankFxNum;                    

                    Instance._bCheckData = true;
                    _bCheck = true;
                }
                else
                {
                    ScoreData TempScore = ScoreArr[i];

                    // 새로운 데이터 체크
                    ScoreArr[i] = CheckData;

                    // 원래 데이터가 새로운 데이터가 됨
                    CheckData = TempScore;

                    if (!_bCheck)
                        _RankNum = TempScore.RankFxNum;

                    Instance._bCheckData = true;
                    _bCheck = true;
                }
            }
            else if (ScoreArr[i].Score != 0)
            {
                if (CheckData.Score < ScoreArr[i].Score)
                {
                    ScoreData TempScore = ScoreArr[i];

                    // 새로운 데이터 체크
                    ScoreArr[i] = CheckData;

                    // 원래 데이터가 새로운 데이터가 됨
                    CheckData = TempScore;

                    if (!_bCheck)
                        _RankNum = TempScore.RankFxNum;

                    Instance._bCheckData = true;
                    _bCheck = true;
                }                
            }
        }


        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetString("Name" + i, m_ScoreArray[i].Name);
            PlayerPrefs.SetString("RankStr" + i, m_ScoreArray[i].RankStr);
            PlayerPrefs.SetInt("Score" + i, m_ScoreArray[i].Score);
        }
    }
}


