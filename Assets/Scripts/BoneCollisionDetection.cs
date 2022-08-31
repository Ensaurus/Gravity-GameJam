using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCollisionDetection : MonoBehaviour
{
    private Joint myJoint;
    public float crackForce;

    private void Start()
    {
        myJoint = GetComponent<Joint>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (myJoint.currentForce.magnitude > crackForce)
            SoundManager.instance.PlayBoneCrack();
    }
}
