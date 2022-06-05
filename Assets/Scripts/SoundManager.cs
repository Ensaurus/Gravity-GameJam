using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] _sources;
    public AudioClip windSound;
    public AudioClip fireSound;
    public AudioClip fireStartSound;
    public AudioClip[] collisionSounds;
    public Rigidbody personRigidbody;
    private bool hasStartedOnFire = false;

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

    // Update is called once per frame
    void Update()
    {
        float velocity = personRigidbody.velocity.magnitude;
        float targetVolume = 0f;
        float currentVolume = _sources[0].volume;
        Debug.Log(velocity);
        if (velocity <= 20f)
        {
            targetVolume = 0f;
        } else if (velocity > 20f && velocity < 100f)
        {
            targetVolume = (velocity - 20f) / 80f;
        }
        else
        {
            targetVolume = 1f;
        }
        _sources[0].volume = targetVolume;

        if (velocity > 40f && hasStartedOnFire == false) 
        {
            hasStartedOnFire = true;
            if (!_sources[2].isPlaying)
            {
                _sources[2].clip = fireStartSound;
                _sources[2].Play();
            }
            if (!_sources[1].isPlaying)
                _sources[1].Play();
        }


        /*if (Mathf.Abs(targetVolume - currentVolume) > Time.deltaTime)
        {
            if (targetVolume > currentVolume)
                _source.volume += Time.deltaTime;
            else
                _source.volume -= Time.deltaTime;
        }
        else
        {
            _source.volume = targetVolume;
        }*/
    }
}