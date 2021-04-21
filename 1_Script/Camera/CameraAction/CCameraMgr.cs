using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState { BossScene ,  Normal }
public class CCameraMgr : MonoBehaviour
{

    // 카메라가 움직일 수있는 모든 변수 값을 정의 해 놓는 곳.
    // 카메라의 Enum값과 클래스를 스트링 하여 카메라 상태를 변환 시켜 주는 역할을 한다.
    // 첫 상태 변환만 지정 해준다

    private bool _bIsInit = false;
    private Dictionary<CameraState, CCamState> _CameraState = new Dictionary<CameraState, CCamState>();
    [SerializeField] private static CCameraMgr _Instance;
    public static CCameraMgr Instance { get { return _Instance; } }
    [SerializeField]  private CameraState _eCameraID;
    public CameraState eCameraID
    {
        get { return _eCameraID; }
    }

    [SerializeField] private Vector3 pCameraRotation;
    public Vector3 GetRotation { get { return pCameraRotation; } }
    // 오프셋 포지션과 카메라 로테이션에 쓰일 변수
    public float targetHeight = 0;                       // Vertical offset adjustment
    public float distance = 0;                          // Default Distance
    public LayerMask collisionLayers;     // What the camera will collide with
    public float currentDistance;
    private float CollisionDistance;
    public float GetCollDistnace {  get { return CollisionDistance; } set { CollisionDistance = value; } }

    private Camera cam;

    public void CameraSetState(CameraState eState)
    {
        if (_bIsInit)
        {
            _CameraState[_eCameraID].enabled = false;
            _CameraState[_eCameraID].EndState();
        }

        _eCameraID = eState;
        _CameraState[_eCameraID].BeginState();
        _CameraState[_eCameraID].enabled = true;
    }
    private void Awake()
    {

        CameraState[] eStateValue =  (CameraState[])System.Enum.GetValues(typeof(CameraState));

        foreach (CameraState eState in eStateValue)
        {
            System.Type FSMType = System.Type.GetType("CAction" + eState.ToString());

            CCamState pFSMState = (CCamState)GetComponent(FSMType);

            if (null == pFSMState)
            {
                pFSMState = (CCamState)gameObject.AddComponent(FSMType);
            }

            _CameraState.Add(eState, pFSMState);
            pFSMState.enabled = false;
        }

        cam = GetComponent<Camera>();
        collisionLayers = LayerMask.NameToLayer("FixedCollision");
    }
    private void Start()
    {
        if (gameObject.name == "ChangeCamera")
        {
            CameraSetState(CameraState.Normal);
        }
        else
        {
            CameraSetState(CameraState.BossScene);
        }
        _bIsInit = true;
    }

   
}
