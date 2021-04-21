using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlantStat
{
    public float HP;
    public float Damage;
}

public class PlantManager : MonoBehaviour
{    

    [SerializeField]
    private PlantStat _stat;
    public PlantStat GetStat { get { return _stat; } }

    private Animator _anim;

    [SerializeField]
    private GameObject _PlantEffect;

    private ObjetStat _Pstat;

    [SerializeField]
    private Collider _Bomb;

    [SerializeField]
    SkinnedMeshRenderer _skin;

    AudioSource _audio;

    [SerializeField]
    AudioClip _clip;

    private bool _bDead;
    private bool _bHit;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
  

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("PWeapon") ||
            col.CompareTag("PSkill") || 
            col.CompareTag("PSkillEvent") ||
            col.CompareTag("RushSkill"))
        {
            //_stat.HP = (_stat.HP - _Pstat.Damage) > 0 ? _Pstat.Damage : 0;
            StartCoroutine(isDead());
        }
    }

    public void PlantBombSound()
    {
        _audio.PlayOneShot(_clip, 1.0f);
    }

    private IEnumerator isDead()
    {
        _anim.SetInteger("PlantState", 1);
        _Bomb.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _Bomb.enabled = false;
        _skin.enabled = false;
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    public void PlantEffect()
    {
        GameObject Effect = Instantiate(_PlantEffect, transform.position, transform.rotation);
        Destroy(Effect, 2f);
    }
}
