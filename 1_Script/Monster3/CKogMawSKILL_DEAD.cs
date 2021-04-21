using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKogMawSKILL_DEAD : CKogMawState
{
    public override void BeginState()
    {
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }
    public void Update()
    {
        //if (_Manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("SKILLDEAD") &&
        //   _Manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        //{
        //    print(" skill 에 의해서 죽음 ");
        //    
        //}
        gameObject.SetActive(false);
    }
}
