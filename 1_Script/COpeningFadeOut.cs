using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class COpeningFadeOut : MonoBehaviour
{

    private Animator _anim;

    [SerializeField]
    private Image _FadeOut;
    private float _Fadetime = 0;

    private bool FadeOutStart;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }


    private void Update()
    {
        OpeningSkip();
        IngameFadeOut();
    }


    public void IngameFadeOut()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            _anim.GetCurrentAnimatorStateInfo(0).IsName("OPENING"))
        {
            _Fadetime += Time.deltaTime;
            _FadeOut.color = new Color(0, 0, 0, _Fadetime);
            if (_Fadetime >= 1)
                SceneManager.LoadScene("Game");
            return;
        }
    }

    void OpeningSkip()
    {
        if (Input.GetKeyDown(KeyOption.ESC))
            FadeOutStart = true;        
        else if (FadeOutStart)
        {
            GetComponent<AudioSource>().mute = true;
            _Fadetime += Time.deltaTime;
            _FadeOut.color = new Color(0, 0, 0, _Fadetime);
            if (_Fadetime >= 1)
                SceneManager.LoadScene("Game");
            return;
        }
    }
}
