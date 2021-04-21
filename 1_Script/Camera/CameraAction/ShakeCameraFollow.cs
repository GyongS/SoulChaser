using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCameraFollow : MonoBehaviour
{
    public float _fadeIn = 0.2f;
    public float _fadeOut = 0.1f;
    public float _row = 0.0f;
    public float _col = 0.0f;

    private float _shake = 0.0f;
    private float _amount = 0.05f;
    private Vector3 _oldPos;
    
    void Awake()
    {
        _oldPos = transform.localPosition;
    }

    private void LateUpdate()
    {
        //ShakeCameraInstance();
    }

    public void ShakeCamera()
    {
        //if (_shake > 0.0f)
        //{
        //    transform.localPosition = _oldPos + Random.insideUnitSphere * _amount;
        //    _shake -= Time.deltaTime;
        //}
        //else
        //{
        //    transform.localPosition = _oldPos;
        //}
        //if (_monsterstatectrl.BCameraShake)
        //{
        //    _shake = 0.1f;
        //}         
    }
    //public void ShakeCameraInstance()
    //{
    //    if (_monsterstatectrl.BCameraShake)
    //    {
    //        CameraShaker.Instance.ShakeOnce(
    //            _col, _row, _fadeIn, _fadeOut);
    //              //행 // 열
    //        // 상하  2f, 좌우 2f , 0.2f   0.1f
    //    }
    //}
    
}
