using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpeed : MonoBehaviour
{
    //var FireEffect Transform;
    //public FireEffect Transform;
    //ParticleSystem fireSystem = FireEffect;
    //public GameObject bodyPart;
    public Rigidbody rb;
    public float maxSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //rb = bodyPart.GetComponent<Rigidbody>();
        ParticleSystem ps = GetComponent<ParticleSystem>();
        //var emiss = fireSystem.emission;
        var emiss = ps.emission;
        emiss.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        //var emiss = fireSystem.emission;
        var emiss = ps.emission;

        /*
        if (Input.GetKey ("f")){
            emiss.enabled = true;
        }
        else{
            emiss.enabled = false;
        }   
        */
        Debug.Log(rb.velocity.magnitude); 

        if (rb.velocity.magnitude >= maxSpeed){
            emiss.enabled = true;
        }
        else{
            emiss.enabled = false;
        }
    }
}
