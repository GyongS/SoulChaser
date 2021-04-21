using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleScene : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField]
    private Image[] _Title;

    [SerializeField]
    private GameObject _TitleParticle;

    [SerializeField]
    private Animator _Logoanim;

    [SerializeField]
    AudioSource _audio;

    [SerializeField]
    AudioClip[] _clip;

    private float _fade = 1.0f;
    private bool bStart;
    private bool bExit;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
        
    private void Update()
    {
        StartGame();
        ExitGame();
    }

    private void StartGame()
    {
        if (bStart)
        {
            _fade -= Time.deltaTime;
            _Logoanim.enabled = false;
            for (int i = 0; i < _Title.Length; i++)
            {
                _Title[i].color = new Color(1, 1, 1, _fade);
                _TitleParticle.SetActive(false);
            }
            if (_fade <= 0)
            {
                SceneManager.LoadScene("CartoonScene");
                bStart = false;
            }
        }
    }


    private void ExitGame()
    {
        if (bExit)
        {
            _fade -= Time.deltaTime;
            _Logoanim.enabled = false;
            for (int i = 0; i < _Title.Length; i++)
            {
                _Title[i].color = new Color(1, 1, 1, _fade);
                _TitleParticle.SetActive(false);
            }
            if (_fade <= 0)
            {
                bExit = false;
                Application.Quit();
            }
        }
    }


    public void GameStart()
    {
        bStart = true;
    }


    public void GameExit()
    {
        bExit = true;
    }

    public void OnPointerClick(PointerEventData _event)
    {
        _audio.PlayOneShot(_clip[0]);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _audio.PlayOneShot(_clip[1]);
    }
}
