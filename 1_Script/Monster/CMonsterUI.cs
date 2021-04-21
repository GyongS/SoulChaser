using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMonsterUI : MonoBehaviour
{

    private static CMonsterUI _instance;
    public static CMonsterUI Instance { get { return _instance; } }

    private Dictionary<GameObject, Image> _monsterdic = new Dictionary<GameObject, Image>();
    private Dictionary<GameObject, Image> _monstertest = new Dictionary<GameObject, Image>();


    private Image[] _MonsterHPBG;
    public Image[] MonsterHPBG { get { return _MonsterHPBG; } }

    private Image[] _MonsterHP;
    public Image[] MonsterHP { get { return _MonsterHP; } }

    private int _curCnt = 0;


    private void Awake()
    {
        _instance = this;
        _MonsterHPBG = new Image[transform.childCount];
        _MonsterHP = new Image[transform.childCount];
        for (int i = 0; i < _MonsterHPBG.Length; i++)
        {
            _MonsterHPBG[i] = transform.GetChild(i).GetChild(3).GetChild(0).GetComponent<Image>();
            _monsterdic.Add(transform.GetChild(i).gameObject, _MonsterHPBG[i]);
        }
        for (int j = 0; j < _MonsterHP.Length; j++)
        {
            _MonsterHP[j] = transform.GetChild(j).GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>();
        }
    }

    private void Update()
    {
        //MonsterHPbar();
    }

    private void MonsterHPbar()
    {
        for (int i = 0; i < _MonsterHP.Length; i++)
        {
            _MonsterHP[i].fillAmount = transform.GetChild(i).GetComponent<CMonsterManager>().GetMstat.CurHP / transform.GetChild(i).GetComponent<CMonsterManager>().GetMstat.MaxHP;

            _MonsterHP[i].transform.rotation = Camera.main.transform.rotation;
        }
        for (int j = 0; j < _MonsterHPBG.Length; j++)
        {
            _MonsterHPBG[j].transform.rotation = Camera.main.transform.rotation;
        }
    }
    public void HPEnableTrue()
    {
        _MonsterHPBG[_curCnt++].gameObject.SetActive(true);
    }

    public void HPEnableFalse(GameObject obj)
    {
        if (_monsterdic.ContainsKey(obj))
            _monsterdic[obj].gameObject.SetActive(false);
    }
}
