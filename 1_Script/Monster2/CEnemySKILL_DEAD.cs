using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySKILL_DEAD : CEnemyState
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
        if (_manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("SKILLDEAD") &&
           _manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            print(" skill 에 의해서 죽음 ");
            gameObject.SetActive(false);
        }
    }
}
