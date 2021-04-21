using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerSound : MonoBehaviour
{
    private static CPlayerSound _instance;
    public static CPlayerSound Instance { get { return _instance; } }

    private AudioSource _audio;

    [SerializeField]
    private AudioSource _VoiceSource;

    [SerializeField]
    private AudioClip[] _AttackSound;

    [SerializeField]
    private AudioClip _DashSound;

    [SerializeField]
    private AudioClip[] _RushSound;

    [SerializeField]
    private AudioClip _SlowSound;

    [SerializeField]
    private AudioClip _UltSound;

    [SerializeField]
    private AudioClip[] _RunSound;

    [SerializeField]
    private AudioClip[] _AttackVoice;

    [SerializeField]
    private AudioClip[] _Attack3Voice;

    [SerializeField]
    private AudioClip[] _SkillVoice;

    [SerializeField]
    private AudioClip[] _HitVoice;

    [SerializeField]
    private AudioClip _GameoverVoice;

    private void Awake()
    {
        _instance = this;
        _audio = GetComponent<AudioSource>();
    }

    public void _PAttack1Sound()
    {
        _audio.PlayOneShot(_AttackSound[0], 0.8f);
        _audio.pitch = Random.Range(1.01f, 1.05f);
    }

    public void _PAttack2Sound()
    {
        _audio.PlayOneShot(_AttackSound[1], 0.8f);
        _audio.pitch = Random.Range(1.01f, 1.05f);
    }

    public void _PAttack3Sound()
    {
        _audio.PlayOneShot(_AttackSound[2], 0.8f);
        _audio.pitch = Random.Range(1.01f, 1.05f);
    }

    public void _PDashSound()
    {
        _audio.PlayOneShot(_DashSound);
        _audio.pitch = Random.Range(1.01f, 1.05f);
    }

    public void _PRushSound()
    {
        _VoiceSource.PlayOneShot(_RushSound[Random.Range(0, 2)], 0.7f);
    }
         
    public void _PSlowSound()
    {
        _audio.PlayOneShot(_SlowSound, 1.0f);
    }

    public void _PUltSound()
    {
        _audio.PlayOneShot(_UltSound, 1.0f);
    }

    public void _PRunSound()
    {
        _audio.PlayOneShot(_RunSound[0], 0.7f);
    }

    public void _PAttack12Voice()
    {
        _VoiceSource.PlayOneShot(_AttackVoice[Random.Range(0,5)]);
        _VoiceSource.pitch = 1;
    }

    public void _PAttack3Voice()
    {
        _VoiceSource.PlayOneShot(_Attack3Voice[Random.Range(0,3)]);
        _VoiceSource.pitch = 1;
    }

    public void _PRushVoice()
    {
        _VoiceSource.PlayOneShot(_SkillVoice[Random.Range(0, 2)]);
    }

    public void _PSlowVoice()
    {
        _VoiceSource.PlayOneShot(_SkillVoice[2]);
    }

    public void _PUltVoice()
    {
        _VoiceSource.PlayOneShot(_SkillVoice[Random.Range(3,5)]);
    }

    public void _PHitVoice()
    {
        _VoiceSource.PlayOneShot(_HitVoice[Random.Range(0, 3)]);
    }
    
    public void _PGameoverVoice()
    {
        _VoiceSource.PlayOneShot(_GameoverVoice);
    }

        
        

}
