using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossSound : MonoBehaviour
{
    private static CBossSound _instance;
    public static CBossSound Instance { get { return _instance; } }

    private AudioSource _audio;

    public AudioSource _Playaraudio;

    [SerializeField]
    private AudioClip[] _BossSound;

    [SerializeField]
    private AudioClip[] _HitSound;

    [SerializeField]
    private AudioClip _BossIntro;

    [SerializeField]
    private AudioClip _BossDEAD;

    [SerializeField]
    private AudioClip _BossStart;

    [SerializeField]
    private AudioClip _BossSkill;

    [SerializeField]
    AudioSource _PaudioSource;

    private void Awake()
    {
        _instance = this;
        _audio = GetComponent<AudioSource>();
    }

    public void _BossIntroSound()
    {
        _audio.PlayOneShot(_BossIntro, 1.0f);        
    }

    public void _BossRunSound()
    {
        _audio.PlayOneShot(_BossSound[0], 1.0f);
        _audio.pitch = Random.Range(1.01f, 1.05f);
    }

    public void _BossHitSound()
    {
        _PaudioSource.PlayOneShot(_HitSound[Random.Range(0,5)], 1f);
    }

    public void _BossDeadSound()
    {
        _audio.PlayOneShot(_BossDEAD, 0.7f);
    }

    public void _BossStartSound()
    {
        _Playaraudio.PlayOneShot(_BossStart, 1.0f);
    }

    public void _BossSkillSound()
    {
        _audio.PlayOneShot(_BossSkill, 1.0f);
    }
         
}
