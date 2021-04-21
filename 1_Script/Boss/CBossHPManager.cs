using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CBossHPManager : MonoBehaviour
{
    private static CBossHPManager _instance;
    public static CBossHPManager Instance { get { return _instance; } }

    [SerializeField]
    private Transform BossHP;

    public Canvas _BossCanvas;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        BossHP.rotation = Camera.main.transform.rotation;
    }
}
