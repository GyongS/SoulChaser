using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerDamageEvent : MonoBehaviour
{
    [SerializeField]
    private Collider[] _DamageCol;

    [SerializeField]
    private Collider _EventCol;

    [SerializeField]
    private Collider _SlowSkill;

    [SerializeField]
    private Collider _RushSkill;

    public void Attack1Event(int _Event)
    {
        switch (_Event)
        {
            case 0:
                break;
            case 1:
                CPlayerSound.Instance._PAttack1Sound();
                break;
            case 2:
                CPlayerManager.Instance.Rigid.AddForce(transform.forward * 450);
                break;
        }
    }

    public void Attack2Event(int _Event)
    {
        switch (_Event)
        {
            case 0:
                break;
            case 1:
                CPlayerSound.Instance._PAttack2Sound();
                break;
            case 2:
                CPlayerManager.Instance.Rigid.AddForce(transform.forward * 5, ForceMode.Impulse);
                break;
        }
    }

    public void Attack3Event(int _Event)
    {
        switch (_Event)
        {
            case 0:
                break;
            case 1:
                CPlayerSound.Instance._PAttack3Sound();
                break;
            case 2:
                CPlayerManager.Instance.Rigid.AddForce(transform.forward * 600);
                break;
        }
    }

    public void SkillEvent(int _Event)
    {
        switch (_Event)
        {
            case 0:
                _DamageCol[2].enabled = false;
                break;
            case 1:
                _DamageCol[2].enabled = true;
                break;
            case 2:
                _EventCol.enabled = true;
                break;
            case 3:
                _EventCol.enabled = false;
                break;
            case 4:
                CPlayerManager.Instance.UltMeshFalse();
                break;
            case 5:
                CPlayerManager.Instance.UltMeshTrue();
                break;            
        }
    }

    public void SlowSkillEvent(int _Event)
    {
        switch (_Event)
        {
            case 0:
                _SlowSkill.enabled = true;
                break;
            case 1:
                _SlowSkill.enabled = false;
                break;

        }
    }

    public void RushSkillEvent(int _Event)
    {
        switch (_Event)
        {
            case 0:
                _RushSkill.enabled = true;
                break;
            case 1:
                _RushSkill.enabled = false;
                break;
        }
    }
}

