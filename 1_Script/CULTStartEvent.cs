using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CULTStartEvent : MonoBehaviour
{
    private static CULTStartEvent _instance;
    public static CULTStartEvent Instance { get { return _instance; } }

    Animator _anim;
    public bool _bUltEvent;
    private void Awake()
    {
        _instance = this;
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _UltEvent();
    }

    void _UltEvent()
    {
        if(_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0 &&
            _anim.GetCurrentAnimatorStateInfo(0).IsName("ULT"))
        {
            _bUltEvent = true;
            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            _anim.GetCurrentAnimatorStateInfo(0).IsName("ULT"))
            {
                _bUltEvent = false;
            }
        }
    }
}
