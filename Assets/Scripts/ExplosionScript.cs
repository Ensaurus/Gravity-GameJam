using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpeed : MonoBehaviour
{
    public Rigidbody rb;
    public float maxSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var emiss = ps.emission;

        if (rb.velocity.magnitude >= maxSpeed){
            ps.Play();
        }
        else{
            ps.Stop();
        }
    }
}
