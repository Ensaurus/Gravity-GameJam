using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpeed : MonoBehaviour
{
    //var FireEffect Transform;
    //public FireEffect Transform;
    //ParticleSystem fireSystem = FireEffect;
    

    // Start is called before the first frame update
    void Start()
    {
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
        
        if (Input.GetKey ("f")){
            emiss.enabled = true;
        }
        else{
            emiss.enabled = false;
        }   
    }
}
