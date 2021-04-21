using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CEnemyUI : MonoBehaviour
{
    private Image _HpGauge;
    private Image _HpBar;
    private GameObject pEnemys;
    private GameObject pCanvasUI;
    private CEnemyManager pEnemyManager;
    private void Awake()
    {
        pEnemys = this.gameObject;
        pCanvasUI = transform.Find("Canvas").gameObject;
        _HpBar = pCanvasUI.transform.GetChild(0).GetComponent<Image>();
        _HpGauge = pCanvasUI.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        pEnemyManager = GetComponent<CEnemyManager>();
    }

    private void Update()
    {
        _HpGauge.fillAmount = pEnemyManager.GetInfo.CurHP / pEnemyManager.GetInfo.MaxHP;
        _HpGauge.transform.rotation = Camera.main.transform.rotation;
        _HpBar.transform.rotation = Camera.main.transform.rotation;
    }
}
