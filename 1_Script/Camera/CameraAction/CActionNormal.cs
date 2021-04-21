using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CActionNormal : CCamState
{
    private Vector3 position;
    private Vector3 vNextPostion;
    private Quaternion QuaternionRot;
    [SerializeField] private float fTime, fDelayTime;
    CCameraShake pCameraShake = null;
    private Quaternion rotation;

    [SerializeField] private Vector3 FixedPos;
    private Camera pCamera;
    public Quaternion GetQuaternion
    {
        get { return QuaternionRot; }
    }
    public Vector3 GetNextPostion
    {
        get { return vNextPostion; }
    }
    private Quaternion qNextRotation;
    public Quaternion GetNextQuaternion
    {
        get { return qNextRotation; }
    }

    private void Start()
    {
        
    }

    public override void BeginState()
    {
        base.BeginState();
        _CamMgr.currentDistance = _CamMgr.distance;
        transform.rotation = Quaternion.Euler(_CamMgr.GetRotation);
    }

    public override void EndState()
    {
        base.EndState();
       
    }

    public void Update()
    {
        fTime += Time.deltaTime;

        position = (GetTarget.position + FixedPos) - (transform.rotation * Vector3.forward * _CamMgr.currentDistance);
        transform.rotation = Quaternion.Euler(_CamMgr.GetRotation);
        transform.position = position;
     
      
    }

}
