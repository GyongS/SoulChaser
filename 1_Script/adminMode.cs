using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adminMode : MonoBehaviour
{
    [SerializeField]
    GameObject RestartBGM;

    void Update()
    {
        //BossDead();
    }

    private void Awake()
    {
        //if(PauseManager.b_Restart)
        //{
        //    RestartBGM.SetActive(true);
        //}
        //else
        //{
        //    RestartBGM.SetActive(false);
        //}
    }
    private void BossDead()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            UIManager.Instance.TargetHPCount = 0;
            CBossManager.Instance.ZeroHP();
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.F10))
        {
            PlayerPrefs.DeleteAll();
        }
    }


}
