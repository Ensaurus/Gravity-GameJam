using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    
    private AudioSource[] _sources;
    private List<AudioSource> _collisionSources = new List<AudioSource>();
    public AudioClip windSound;
    public AudioClip fireSound;
    public AudioClip fireStartSound;
    public AudioClip explosionSound;
    public AudioClip[] collisionSounds;

    public AudioClip bonkSound;
    public AudioClip boneCrackSound;
    private AudioSource bonkSource;
    private AudioSource boneCrackSource;

    public Rigidbody personRigidbody;

    public float maxWindVolume = 0.7f;
    public float fireVolume = 0.7f;
    public float explosionVolume = 0.5f;

    private float maxVol = 1;

    public Scrollbar slider;

    private bool initialized = false;

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
        if (!FindObjectOfType<PersistentData>())
        {
            Instantiate(new GameObject()).AddComponent<PersistentData>();
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

        foreach (AudioClip collisionSound in collisionSounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = collisionSound;
            _collisionSources.Add(source);
        }

        boneCrackSource = gameObject.AddComponent<AudioSource>();
        boneCrackSource.clip = boneCrackSound;

        bonkSource = gameObject.AddComponent<AudioSource>();
        bonkSource.clip = bonkSound;
        ChangeMaxVolume(PersistentData.instance.gameVolume);
        slider.value = maxVol;
        initialized = true;
    }

    public void PlayCollisionSound()
    {
        if (initialized)
        {
            int index = Random.Range(0, _collisionSources.Count);
            _collisionSources[index].Play();
        }
    }

    public void PlayBoneCrack()
    {
        boneCrackSource.Play();
    }

    public void PlayBonk()
    {
        // if (!bonkSource.isPlaying)
            bonkSource.Play();
    }

    public void Explode()
    {
        _sources[0].Stop();
        _sources[1].Stop();
        _sources[2].clip = explosionSound;
        _sources[2].volume = explosionVolume * maxVol;
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
            _sources[1].volume = fireVolume * maxVol;
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
        _sources[0].volume = Mathf.Lerp(0, maxWindVolume * maxVol, velocity / PlayerSpeedTracker.instance.criticalVelocity);
    }

    public void ChangeMaxVolume(float newMax)
    {
            maxVol = newMax;
            PersistentData.instance.gameVolume = maxVol;
            foreach (AudioSource source in GetComponents<AudioSource>())
            {
                source.volume = maxVol;
            }
    }
}
