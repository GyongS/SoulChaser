using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEndingScene : MonoBehaviour
{
    public Transform StageClearScene;

    public void StageClearEvent()
    {
        StageClearScene.gameObject.SetActive(true);
    }
}

