using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    Collider[] ragdollParts;

    // Start is called before the first frame update
    void Start()
    {
        ragdollParts = gameObject.GetComponentsInChildren<Collider>();
        DisableRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("space"))
        {
            EnableRagdoll();
        }
        else if (Input.GetButtonUp("space"))
        {
            DisableRagdoll();
        }
    }

    private void EnableRagdoll()
    {
        foreach (Collider c in ragdollParts)
        {
            c.enabled = true;
        }
    }

    private void DisableRagdoll()
    {
        foreach (Collider c in ragdollParts)
        {
            c.enabled = false;
        }
    }
}
