using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossState : MonoBehaviour
{
    protected CBossManager _manager;
    protected float fTime  = 0.0f;
    protected float fPathDistance = 0.0f;
    public float GetDistance { get { return fPathDistance; } }
    [SerializeField] protected const float fStartTime = 0.0f;
     protected Transform DestPoint;
    //UI 패스 확인용
    protected bool bIsAStar = false;
    public bool GetbIsAStar { get { return bIsAStar; } }
    
    //
    protected int iGridCount = 0;
    protected Node Destination;

    public void MoveToTarget(Node pTarget)
    {
        Destination = pTarget;
    }
    private void Awake()
    {
        _manager = GetComponent<CBossManager>();
        DestPoint = GameObject.FindGameObjectWithTag("DestPoint").GetComponent<Transform>();


    }

    public virtual void BeginState() { }
    public virtual void EndState() { }

    protected virtual void Update()
    {
        fTime += Time.deltaTime;
      
    }

}
