using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private Image _Loadingbar;
    private float _Min = 0;
    
    [SerializeField]
    private float _Max = 3;


    private void Update()
    {
        Loading();
    }

    private void Loading()
    {   
        _Loadingbar.fillAmount = _Min / _Max;
        _Min += Time.deltaTime;
        if(_Min >= _Max)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
