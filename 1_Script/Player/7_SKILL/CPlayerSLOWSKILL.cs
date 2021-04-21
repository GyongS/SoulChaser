using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerSLOWSKILL : CPlayerState
{
    public override void BeginState()
    {
        base.BeginState();
        _manager.SlowMinusSoul();
        StartCoroutine(UltClick());
    }

    public override void EndState()
    {
        base.EndState();
    }

    protected override void Update()
    {
        if (_manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            _manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("SLOWSKILL"))
        {
            _manager.SetState(PlayerState.IDLE);
            return;
        }
    }

    private IEnumerator UltClick()
    {
        UIManager.Instance.UltIcon.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.UltIcon.color = new Color(1, 1, 1, 1);
    }
}
