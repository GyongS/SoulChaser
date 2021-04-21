using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningScene : MonoBehaviour
{
    [SerializeField]
    Image _ESCButtonimg;

    bool _isNext;

    float _ESCFade;

    private void Update()
    {
        MouseButtonAlpha();
    }

    private void MouseButtonAlpha()
    {
        _ESCButtonimg.color = new Color(1, 1, 1, _ESCFade);

        if (!_isNext)
        {
            _ESCFade -= 2 * Time.deltaTime;
            if (_ESCFade <= 0)
            {
                _isNext = true;
            }
        }
        else if (_isNext)
        {
            _ESCFade += 2 * Time.deltaTime;
            if (_ESCFade >= 1)
            {
                _isNext = false;
            }
        }
    }
}
