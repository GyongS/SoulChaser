using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CScoreText : MonoBehaviour
{
    //private static CScoreText _instance;
    //public static CScoreText Inst
    //{
    //    get
    //    {
    //        if (_instance == null)
    //            _instance = FindObjectOfType<CScoreText>();

    //        return _instance;
    //    }
    //}

    [SerializeField]
    private Text[] ScoreTextArr = null;

    [SerializeField]
    private Text[] NameTextArr = null;

    int[] _test = {0,0,0,0,0};

    public static bool _RankCheck;

    private void Awake()
    {
        _RankCheck = false;
        CRankSystem.ScoreLoad();
    }

    private void Update()
    {
        if (CRankSystem.ScoreArr.Count != ScoreTextArr.Length)
        {
            Debug.LogError("if (CRankSystem.ScoreArr.Count != ScoreTextArr.Length");
            return;
        }

        for (int i = 0; i < ScoreTextArr.Length; i++)
        {

            ScoreTextArr[i].text = CRankSystem.ScoreArr[i].RankStr;            
            if (CRankSystem.ScoreArr[i].Score == 0)
            {
                ScoreTextArr[i].text = "0";
            }
        }
        for (int j = 0; j < NameTextArr.Length; j++)
        {
            string _Name = CRankSystem.ScoreArr[j].Name;
            if (_Name == "")
            {
                _Name = "이름 없음";
            }
            NameTextArr[j].text = _Name;
        }
    }
}
