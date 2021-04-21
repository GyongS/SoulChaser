using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSoundOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(SceneManager.GetActiveScene().name == "Opening")
            UIManager.isGameOver = false;
    }

    private void Update()
    {
         //| SceneManager.GetActiveScene().name == "StageClear"
        if (SceneManager.GetActiveScene().name == "Title" || UIManager.isGameOver)
        {
            gameObject.SetActive(false);
        }
    }
}
