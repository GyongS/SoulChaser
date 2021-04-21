using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class GameStart : MonoBehaviour
{

    public Transform _GameStartTr;

    public PlayableDirector _playable;


    private static bool _bGameStart;
    public static bool BGameStart { get { return _bGameStart; } set { _bGameStart = false; } }

    private void Awake()
    {
        _bGameStart = false;
        _playable.time = 0;
    }

    private void Update()
    {
        //Debug.Log(BGameStart);
        //Debug.Log(_playable.time);
        if (_playable.time >= 1.99f)
        {
            _bGameStart = true;
        }
    }
}
