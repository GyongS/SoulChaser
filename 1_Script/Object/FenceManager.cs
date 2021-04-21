using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FenceStat
{
    public float HP;
    public AudioClip _HitSound;
    public GameObject[] _HitEffect;
    public Transform _EffectManager;
}
public class FenceManager : MonoBehaviour
{
    [SerializeField]
    private FenceStat _Fstat;

    [SerializeField]
    GameObject _BossObj;

    [SerializeField]
    GameObject _FenceDust;

    private AudioSource _audio;
    private Animator _anim;
    private float _EffectCnt = 0;

    private bool _isHit;
    public static bool _isSlow;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _isSlow = false;
    }

    private void Update()
    {
        FenceHitAni();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("PWeapon"))
        {
            _audio.pitch = Random.Range(1.0f, 1.10f);
            _audio.PlayOneShot(_Fstat._HitSound, 0.4f);
            FenceHitEffect();
            _EffectCnt++;
            _Fstat.HP--;
            StartCoroutine(HitShader());
            _isHit = true;           
        }    
    }

    private IEnumerator HitShader()
    {
        transform.GetComponentInChildren<Renderer>().material.SetFloat("_SickButton", 1);
        yield return new WaitForSeconds(0.1f);
        transform.GetComponentInChildren<Renderer>().material.SetFloat("_SickButton", 0);
    }

    private void OnTriggerStay(Collider col)
    {
        switch (col.tag)
        {
            case "Boss":
                _BossObj.SetActive(true);
                _isSlow = true;
                CBossManager.Instance.SlowSpeed();
                break;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        switch (col.tag)
        {
            case "Boss":
                _BossObj.SetActive(false);
                _isSlow = false;
                CBossManager.Instance.initSpeed();
                break;
        }
    }

    private void FenceHitAni()
    {
        _anim.SetBool("isHit", _isHit);
        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            _anim.GetCurrentAnimatorStateInfo(0).IsName("HIT"))
        {
            _isHit = false;
            _anim.SetBool("isHit", _isHit);            
        }
        else if (_Fstat.HP <= 0)
        {
            _anim.SetBool("isDead", true);
            _FenceDust.SetActive(true);
            CBossManager.Instance.initSpeed();
            _isSlow = false;
            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
                _anim.GetCurrentAnimatorStateInfo(0).IsName("DEAD"))
            {
                gameObject.SetActive(false);
                _FenceDust.SetActive(false);
            }
        }
    }

    private void FenceHitEffect()
    {
        if (_EffectCnt % 2 == 0)
        {
            GameObject Effect = Instantiate(_Fstat._HitEffect[0], _Fstat._EffectManager.position, _Fstat._EffectManager.rotation);

            Effect.transform.parent = _Fstat._EffectManager;

            Destroy(Effect, 1f);
        }
        else
        {
            GameObject Effect = Instantiate(_Fstat._HitEffect[1], _Fstat._EffectManager.position, _Fstat._EffectManager.rotation);

            Effect.transform.parent = _Fstat._EffectManager;

            Destroy(Effect, 1f);
        }
    }

}
