using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;


public class CPlayerEndingAnim : MonoBehaviour
{
    private static CPlayerEndingAnim _instance;
    public static CPlayerEndingAnim Instance { get { return _instance; } }

    private Animator _anim;
    public Animator Anim { get { return _anim; } }

    public GameObject _RankCanvas;
    public GameObject _ClearUI;

    public PlayableDirector[] _Playable;
    public TimelineAsset _Timeline;

    AudioSource _audio;

    [SerializeField]
    AudioClip[] _ClearVoice;

    public bool bInputObj;
    private void Awake()
    {
        _instance = this;
        bInputObj = false;
        _audio = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
        _anim.GetCurrentAnimatorStateInfo(0).IsName("EndingStart"))
        {
            _anim.SetInteger("EndingState", 1);
        }
        else if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1f &&
           _anim.GetCurrentAnimatorStateInfo(0).IsName("EndingIDLE"))
        {
            _RankCanvas.SetActive(true);
            if (_Playable[0].time >= 4.9f)
            {
                bInputObj = true;
                _ClearUI.SetActive(true);
            }
        }
    }

    public void _PClearVoice()
    {
        _audio.PlayOneShot(_ClearVoice[Random.Range(0, 2)]);
    }
}
