using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionBossScene : CCamState
{
    private Vector3 Position , initializePos;
    [SerializeField] private Vector3 FixedPos;
    [SerializeField] private float fLerp_OneMax;
    [SerializeField] private float fZAixsDamping;
    private float fTime = 0;
    [SerializeField] private float fDeleyTime = 0.0f;

    public static bool btutorialUI;
    private void Start()
    {
        btutorialUI = false;
    }

    public override void BeginState()
    {
        base.BeginState();
        _CamMgr.currentDistance = _CamMgr.distance;
        transform.rotation = Quaternion.Euler(_CamMgr.GetRotation);

        initializePos = (GetBossPos.position + FixedPos) - (transform.rotation * Vector3.forward * (_CamMgr.currentDistance + fZAixsDamping));
        transform.position = initializePos;
        transform.rotation = Quaternion.Euler(_CamMgr.GetRotation);

    }

    public override void EndState()
    {
        base.EndState();
    }

    public void Update()
    {
        fTime += Time.deltaTime;

        if (fDeleyTime < fTime)
        {
            Position = (GetTarget.position + new Vector3(0.0f , 1.1f, 0.0f)) - (transform.rotation * Vector3.forward * 
                (_CamMgr.currentDistance));
            transform.position = Vector3.Lerp(transform.position, Position, fLerp_OneMax);
            transform.rotation = Quaternion.Euler(_CamMgr.GetRotation);

            if(Vector3.Distance(transform.position, Position) < 0.1f)
            {
                btutorialUI = true;
                UIManager.Instance.TutorialUI.SetActive(true);
                _CamMgr.CameraSetState(CameraState.Normal);
                return;
            }
        }
    }
}
