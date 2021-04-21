using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerState : MonoBehaviour
{
    protected CPlayerManager _manager;
    public void SetManager(CPlayerManager manager) { _manager = manager; }

   

    private void Awake()
    {
        _manager = GetComponent<CPlayerManager>();        
    }

    public virtual void BeginState(){}
    public virtual void EndState() { }
    protected virtual void Update(){}   
}
