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
    private bool isRagdoll;
    public bool IsRagdoll { get { return isRagdoll; }
    }

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
            if (!isRagdoll)
                EnableRagdoll();
            else
                DisableRagdoll();
        }
    }

    private void EnableRagdoll()
    {
        isRagdoll = true;
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
        isRagdoll = false;
        // keep z axis constant cuz 2D
        transform.position = new Vector3 (refTransform.position.x, refTransform.position.y, transform.position.z);
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
