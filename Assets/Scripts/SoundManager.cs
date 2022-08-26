using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    
    private AudioSource[] _sources;
    public AudioClip windSound;
    public AudioClip fireSound;
    public AudioClip fireStartSound;
    public AudioClip explosionSound;
    public AudioClip[] collisionSounds;
    public Rigidbody personRigidbody;

    public float maxWindVolume = 0.7f;
    public float fireVolume = 0.7f;
    public float explosionVolume = 0.5f;

    private void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponents<AudioSource>().Length != 3)
        {
            while (GetComponents<AudioSource>().Length < 3)
            {
                gameObject.AddComponent<AudioSource>();
            }
        }
        _sources = GetComponents<AudioSource>();
        _sources[0].clip = windSound;
        _sources[0].volume = 0.0f;
        _sources[0].loop = true;
        _sources[0].Play();
        _sources[1].clip = fireSound;
        _sources[1].loop = true;
    }

    public void Explode()
    {
        _sources[0].Stop();
        _sources[1].Stop();
        _sources[2].clip = explosionSound;
        _sources[2].volume = explosionVolume;
        _sources[2].Play();
    }
    
    public void StartFire()
    {
        if (!_sources[2].isPlaying)
        {
            _sources[2].clip = fireStartSound;
            _sources[2].Play();
        }
        if (!_sources[1].isPlaying)
            _sources[1].volume = fireVolume;
            _sources[1].Play();
    }

    public void StopFire()
    {
        _sources[1].Stop();
    }


    // Update is called once per frame
    void Update()
    {
        float velocity = Mathf.Abs(personRigidbody.velocity.y);
        _sources[0].volume = Mathf.Lerp(0, maxWindVolume, velocity / PlayerSpeedTracker.instance.criticalVelocity);
    }
}
