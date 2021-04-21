using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CUserName : MonoBehaviour, IPointerClickHandler
{
    public static bool bNameCanvas;
    public GameObject[] _RankCelebrateObj;

    [SerializeField]
    Text NameInputText = null;

    public GameObject _InputField;



    public static int i_RankFx;

    private void Awake()
    {
        bNameCanvas = false;
    }    

    public void OnPointerClick(PointerEventData eventData)
    {
        
        CRankSystem.ScoreInput(NameInputText.text, CRankSystem._RankNum);
        CRankSystem.Instance._RankFxObj[CRankSystem._RankNum].SetActive(true);
        CRankSystem._bCheck = false;
        _RankObjActive();
    }

    public void _RankObjActive()
    {
        bNameCanvas = true;
        _InputField.SetActive(false);
        for (int i = 0; i < _RankCelebrateObj.Length; i++)
        {
            _RankCelebrateObj[i].SetActive(true);            
        }
    }    
}
