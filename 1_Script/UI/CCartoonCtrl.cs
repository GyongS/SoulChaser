using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;



public class CCartoonCtrl : MonoBehaviour
{
    [SerializeField]
    Image _StartCartoonImg;

    [SerializeField]
    Image _MouseButtonimg;

    [SerializeField]
    Image _BGroundImage;

    [SerializeField]
    GameObject[] _CartoonImg;

    [SerializeField]
    GameObject[] _CartoonImg2;

    [SerializeField]
    PlayableDirector _Timeline01;

    [SerializeField]
    PlayableDirector _Timeline02;

    bool _isCartoonStart;
    bool _isStart;
    bool _isNext;
    bool _isStop;

    float _fadetime;
    float _Mousefade;

    int _CartoonNum;

    private void Awake()
    {
        _Mousefade = 1;
        _fadetime = 0;
        _CartoonNum = 0;
        _isCartoonStart = false;
        _isStart = false;
        _isNext = false;
        _isStop = false;
        _Timeline02.Stop();
        for (int i = 0; i < _CartoonImg2.Length; i++)
        {
            _CartoonImg2[i].SetActive(false);
        }

    }

    private void Update()
    {
        CartoonCtrl();
        MouseButtonAlpha();
    }

    private void CartoonCtrl()
    {
        _MouseButtonimg.gameObject.SetActive(_isStop);

        if (Input.GetKeyDown(KeyCode.Mouse0) && _Timeline01.time >= 5.4f  && _CartoonNum <= 1)
        {
            for (int i = 0; i < _CartoonImg2.Length; i++)
            {
                _CartoonImg2[i].SetActive(true);
                for(int j = 0; j < _CartoonImg.Length; j++)
                {
                    _CartoonImg[j].SetActive(false);
                }
            }
            _Timeline02.Play();
            _isStop = false;
            _Timeline01.gameObject.SetActive(false);
            _CartoonNum = 1;

        }
        else if (_Timeline01.time >= 5.4f && _CartoonNum <= 0)
        {
            _isStop = true;            
        }
        else if(_Timeline02.time >= 6.4f)
        {
            _CartoonNum = 2;
            if (_CartoonNum >= 2)
            {
                _isStop = true;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    _isStart = true;
            }
            
        }





        if (Input.GetKeyDown(KeyOption.ESC))
        {
            _isStart = true;
        }
        else if (_isStart)
        {
            _fadetime += Time.deltaTime;
            _BGroundImage.color = new Color(0, 0, 0, _fadetime);
            if (_fadetime >= 1)
            {
                _fadetime = 1;
                SceneManager.LoadScene("Opening");
            }
        }
    }

    private void MouseButtonAlpha()
    {
        _MouseButtonimg.color = new Color(1, 1, 1, _Mousefade);
        if (!_isStop)
            return;

        if (!_isNext)
        {
            _Mousefade -= 2 * Time.deltaTime;
            if (_Mousefade <= 0)
            {
                _isNext = true;
            }
        }
        else if (_isNext)
        {
            _Mousefade += 2 * Time.deltaTime;
            if (_Mousefade >= 1)
            {
                _isNext = false;
            }
        }
    }
}
