using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProgressManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _BossPro;

    [SerializeField]
    private RectTransform _PlayerPro;

    [SerializeField]
    private RectTransform _StartPro;

    [SerializeField]
    private RectTransform _EndPro;

    public Transform _Player;
    public Transform _Boss;
    public Transform _Start; // 플레이어가 Start 지점에서 멀어 질 수록 Progressbar가 앞으로 가까워 질 수록 뒤로
    public Transform _End; // 보스가 End 지점에 가까워 질 수록 Progressbar가 앞으로


    private void Awake()
    {
        _BossPro = _BossPro.GetComponent<RectTransform>();
        _PlayerPro = _PlayerPro.GetComponent<RectTransform>();
        _EndPro = _EndPro.GetComponent<RectTransform>();
    }


    private void Update()
    {
        PlayerProgress();
        BossProgress();
    }

    private void PlayerProgress()
    {
        if (_PlayerPro.anchoredPosition.x >= _EndPro.anchoredPosition.x)
            return;

        if (CPlayerManager.Instance.CurrentState != PlayerState.RUN ||
            CPlayerManager.Instance.CurrentState == PlayerState.IDLE)
            return;
        if (Vector3.Distance(_Start.position, _Player.position) > 1)
        {
            _PlayerPro.anchoredPosition =
                new Vector3(_PlayerPro.anchoredPosition.x + 0.1f,
                            _PlayerPro.anchoredPosition.y);
            if (Vector3.Distance(_Start.position, _Player.position) < 2)
            {
                _PlayerPro.anchoredPosition =
                    new Vector3(_PlayerPro.anchoredPosition.x - 0.2f,
                                _PlayerPro.anchoredPosition.y);
            }
        }
        
    }

    private void BossProgress()
    {
        if (_BossPro.anchoredPosition.x >= _EndPro.anchoredPosition.x)
        {
            UIManager.isGameOver = true;
            return;
        }
        if (CBossManager.Instance.CurrentState == BossState.SKILL ||
            CBossManager.Instance.CurrentState == BossState.READY ||
            CBossManager.Instance.CurrentState == BossState.DEAD ||
            CPlayerManager.Instance.CurrentState == PlayerState.ULTSKILL)
            return;
        else if (Vector3.Distance(_End.position, _Boss.position) > 1)
        {
            _BossPro.anchoredPosition =
                new Vector3(_BossPro.anchoredPosition.x + 0.1f,
                _BossPro.anchoredPosition.y);

        }
    }

}
