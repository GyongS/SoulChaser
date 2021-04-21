using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    private GameObject ResetObject;

    [SerializeField]
    GameObject RestartBGM;

    [SerializeField]
    AudioSource _audio;

    [SerializeField]
    AudioClip[] _ButtonSound;    
    

    public static bool b_Restart;
    
    public static bool binittime;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "StageClear")
        {
            b_Restart = false;
        }
    }

    private void Update()
    {
        Resume();
    }

    public void Resume()
    {
        if (Input.GetKeyDown(KeyOption.ESC) && SceneManager.GetActiveScene().name == "Game")
            UIManager.Instance.isPause = false;        
    }

    public void Continue()
    {
        UIManager.Instance.isPause = false;
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
        binittime = true;
        ResetObject = GameObject.FindGameObjectWithTag("BGM");
    }   

    public void ReStart()
    {
        SceneManager.LoadScene("Game");
        UIManager.Instance.isPause = false;
        binittime = true;
        b_Restart = false;
        ResetObject = GameObject.FindGameObjectWithTag("BGM");
        if (UIManager.isGameOver) //SceneManager.GetActiveScene().name == "StageClear" || 
        {
            RestartBGM.SetActive(true);
            b_Restart = true;
        }

    }

    public void IngameExit()
    {
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData _event)
    {
        _audio.PlayOneShot(_ButtonSound[0]);
    }

    public void OnPointerClick(PointerEventData _event)
    {
        _audio.PlayOneShot(_ButtonSound[1]);
    }
}
