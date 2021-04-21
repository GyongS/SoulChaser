using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterEffect : MonoBehaviour
{
    private static CMonsterEffect _instance;
    public static CMonsterEffect Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }

    public GameObject[] _EffectOBJ;
    public Transform _EffectRot;
    public Transform _WeaponTr;
    public Transform _EffectManager;


   public void MSponeEffect()
    {
        GameObject SponeEffet = Instantiate(_EffectOBJ[1], transform.position, transform.rotation);
        SponeEffet.transform.parent = _EffectManager;
        Destroy(SponeEffet, 1f);
    }
   
}
