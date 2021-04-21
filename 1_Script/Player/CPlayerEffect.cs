using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerEffect : MonoBehaviour
{
    private static CPlayerEffect _instance;
    public static CPlayerEffect Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }
    public GameObject[] _EffectOBJ;
    public Transform _EffectRot;
    public Transform _WeaponTr;
    public Transform _EffectManager;
    public Transform _SlowEffectManager;


    public void AttackEffect1()
    {
        GameObject SkillEffect = Instantiate(_EffectOBJ[0], _WeaponTr.position, _EffectRot.rotation);
        SkillEffect.transform.parent = _EffectManager;
        Destroy(SkillEffect, 1f);
    }

    public void AttackEffect2()
    {
        GameObject SkillEffect1 = Instantiate(_EffectOBJ[1], _WeaponTr.position, _EffectRot.rotation);
        SkillEffect1.transform.parent = _EffectManager;
        Destroy(SkillEffect1, 1f);
    }

    public void AttackEffect3()
    {
        GameObject SkillEffect2 = Instantiate(_EffectOBJ[2], _WeaponTr.position, _EffectRot.rotation);
        SkillEffect2.transform.parent = _EffectManager;
        Destroy(SkillEffect2, 1f);
    }
    public void DashEffect()
    {
        GameObject SkillEffect8 = Instantiate(_EffectOBJ[3], _WeaponTr.position, _EffectRot.rotation);
        SkillEffect8.transform.parent = _EffectManager;
        Destroy(SkillEffect8, 1f);
    }

    public void SkillEffect()
    {
        GameObject SkillEffect8 = Instantiate(_EffectOBJ[4], _WeaponTr.position, _EffectRot.rotation);
        //SkillEffect8.transform.parent = _EffectManager;
        Destroy(SkillEffect8, 4f);
    }

    public void UltEffect()
    {
        GameObject SkillEffect8 = Instantiate(_EffectOBJ[5], _WeaponTr.position, _EffectRot.rotation);
        //SkillEffect8.transform.parent = _EffectManager;
        Destroy(SkillEffect8, 4f);
    }

    public void SlowEffect()
    {
        GameObject SkillEffect = Instantiate(_EffectOBJ[6], _WeaponTr.position, _EffectRot.rotation);
        SkillEffect.transform.parent = _SlowEffectManager;
        Destroy(SkillEffect, 2f);
    }

    public void RushEffect()
    {
        GameObject SkillEffect = Instantiate(_EffectOBJ[7], _WeaponTr.position, _EffectRot.rotation);
        SkillEffect.transform.parent = _EffectManager;
        Destroy(SkillEffect, 2f);
    }  
}
