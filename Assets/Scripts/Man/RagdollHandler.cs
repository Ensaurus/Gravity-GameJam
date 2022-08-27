using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    Collider[] allColliders;
    Rigidbody[] allRigidBodies;
    private Joint[] allJoints;
    Animator myAnim;
    // body part in ragdoll used as reference point for snapping parent back into place
    public Transform refTransform;
    private bool isRagdoll;
    public bool IsRagdoll { get { return isRagdoll; } }

    public float initialForceStrength = 15;

    // Start is called before the first frame update
    void Start()
    {
        allColliders = gameObject.GetComponentsInChildren<Collider>();
        myAnim = gameObject.GetComponent<Animator>();
        allRigidBodies = gameObject.GetComponentsInChildren<Rigidbody>();
        allJoints = gameObject.GetComponentsInChildren<Joint>();
        PlayerSpeedTracker.instance.gameOverEvent.AddListener(OnGameOver);
        DisableRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isRagdoll)
                EnableRagdoll();
            else
                DisableRagdoll();
        }
        */
    }

    // Called by IntroManager
    public void InitialRagdoll()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(1, 1, 0) * initialForceStrength, ForceMode.Impulse);
        //EnableRagdoll();
    }

    public void LaunchDown()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        // rb.AddForce(new Vector3(0, -3, 0) * initialForceStrength, ForceMode.Impulse);
        EnableRagdoll();
        // StartCoroutine(EnableAfterSec());
    }

    IEnumerator EnableAfterSec()
    {
        yield return new WaitForSeconds(0.5f);
        EnableRagdoll();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RagdollEnabler")
            EnableRagdoll();
    }

    public void EnableRagdoll()
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
        // transform.position = new Vector3 (refTransform.position.x, refTransform.position.y, transform.position.z);
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

    private void OnGameOver()
    {
        foreach (var joint in allJoints)
        {
            if (joint != null)
            {
                joint.breakForce = 0;
            }
        }
        
        foreach (var rb in allRigidBodies)
        {
            rb.AddExplosionForce(100f, rb.position, 10f);
        }
    }
}
