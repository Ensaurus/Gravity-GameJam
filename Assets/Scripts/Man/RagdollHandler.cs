using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    Collider[] allColliders;
    Rigidbody[] allRigidBodies;
    Animator myAnim;
    // body part in ragdoll used as reference point for snapping parent back into place
    public Transform refTransform;

    // Start is called before the first frame update
    void Start()
    {
        allColliders = gameObject.GetComponentsInChildren<Collider>();
        myAnim = gameObject.GetComponent<Animator>();
        allRigidBodies = gameObject.GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnableRagdoll();
        }
        else if (Input.GetKeyUp("space"))
        {
            DisableRagdoll();
        }
    }

    private void EnableRagdoll()
    {
        foreach (Collider c in allColliders)
        {
            if (c.gameObject != gameObject)
            {
                c.enabled = true;
            }
            else
            {
                c.enabled = false;
            }
        }
        foreach (Rigidbody r in allRigidBodies)
        {
            if (r.gameObject != gameObject)
            {
                r.isKinematic = false;
                r.detectCollisions = true;
            }
            else
            {
                r.isKinematic = true;
                r.detectCollisions = false;
            }
        }
        myAnim.enabled = false;
    }

    private void DisableRagdoll()
    {
        transform.position = refTransform.position;
        foreach (Collider c in allColliders)
        {
            if (c.gameObject != gameObject)
            {
                c.enabled = false;
            }
            else
            {
                c.enabled = true;
            }
        }
        foreach (Rigidbody r in allRigidBodies)
        {
            if (r.gameObject != gameObject)
            {
                r.isKinematic = true;
                r.detectCollisions = false;
            }
            else
            {
                r.isKinematic = false;
                r.detectCollisions = true;
            }
        }
        myAnim.enabled = true;
    }
}
