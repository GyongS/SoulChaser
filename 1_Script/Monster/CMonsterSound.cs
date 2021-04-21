using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterSound : MonoBehaviour
{

    private AudioSource _audio;

    [SerializeField]
    private AudioSource _MonsterHitSource;

    [SerializeField]
    private AudioClip[] _HitSound;

    [SerializeField]
    private AudioClip _AttackSound;

    [SerializeField]
    private AudioClip[] _KogmawAttack;

    [SerializeField]
    private AudioClip[] _SponSound;

    [SerializeField]
    private AudioClip _MonsterVoice;

    [SerializeField]
    private AudioClip _MonsterDead;

    float _Hit;
    bool _isHit;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _Hit = 0;
    }

    public void HitSound()
    {
        _MonsterHitSource.PlayOneShot(_HitSound[Random.Range(0, 5)], 0.4f);
    }

    public void UltHitSound()
    {
        _MonsterHitSource.clip = _HitSound[Random.Range(0, 5)];
        _MonsterHitSource.Play();
    }

    public void MonsterAttackSound()
    {
        _audio.pitch = Random.Range(1.01f, 1.10f);
        _audio.PlayOneShot(_AttackSound, 0.3f);
    }

    public void SponSound()
    {
        _audio.PlayOneShot(_SponSound[0]);        
    }

    public void MonsterVoice()
    {
        _audio.PlayOneShot(_MonsterVoice, 0.4f);
    }

    public void KogMawAttackSound()
    {
        _audio.PlayOneShot(_KogmawAttack[0], 0.5f);
    }

    public void KogMawAttackHitSound()
    {
        _audio.PlayOneShot(_KogmawAttack[1], 1.0f);
    }

    public void MonsterDeadSound()
    {
        _audio.PlayOneShot(_MonsterDead, 0.5f);
    }
}
