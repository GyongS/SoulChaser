using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _BossEffect;

    [SerializeField]
    private Transform[] _EffectManager;
        

    public void PushEffect()
    {
        GameObject effect = Instantiate(_BossEffect[0], _EffectManager[0].position, _EffectManager[0].rotation);
        effect.transform.parent = _EffectManager[0];
        Destroy(effect, 2);
    }

    public void DustEffect(int _Event)
    {
        switch (_Event)
        {
            case 0:
                GameObject effect = Instantiate(_BossEffect[1], _EffectManager[1].position, _EffectManager[1].rotation);
                effect.transform.parent = _EffectManager[1];
                Destroy(effect, 1.5f);
                break;
            case 1:
                GameObject effect2 = Instantiate(_BossEffect[1], _EffectManager[2].position, _EffectManager[2].rotation);
                effect2.transform.parent = _EffectManager[2];
                Destroy(effect2, 1.5f);
                break;
        }        
    }

}
