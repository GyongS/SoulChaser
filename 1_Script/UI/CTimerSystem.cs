using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTimerSystem : MonoBehaviour
{
    private static CTimerSystem _instance;
    public static CTimerSystem Instance { get { return _instance; } }

    [SerializeField]
    private Text _TimerText;

    [SerializeField]
    private float _time;

    [SerializeField]
    private float _Sec = 0;

    [SerializeField]
    private int _Min = 0;

    public static float _HideScore = 0;
         
    
    private void Awake()
    {
        _instance = this;
        ScoreReset();
    }

    
    public string RankScore { get { return _TimerText.text; } }

    void ScoreReset()
    {
        _HideScore = 0;
    }

    private void Update()
    {

        if (CBossManager.Instance.CurrentState == BossState.DEAD | UIManager.isGameOver)
            return;
        if (CBossManager.Instance.CurrentState != BossState.READY && !UIManager.Instance.isPause)
        {
            _Sec += Time.deltaTime;
            _HideScore += Time.deltaTime;
        }
        _TimerText.text = string.Format("{0:D2}:{1:D2}", _Min, (int)_Sec);

        if (PauseManager.binittime)
        {
            _Sec = 0;
            _HideScore = 0;
            PauseManager.binittime = false;
        }
        else if ((int)_Sec > 59)
        {
            _Sec = 0;
            _Min++;
            _HideScore++;
        }
    }  
}
