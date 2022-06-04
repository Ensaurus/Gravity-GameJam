using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NudgeHandler : MonoBehaviour
{
    private RagdollHandler ragdollHandler;
    public Rigidbody target;
    public float nudgeStrength;

    private enum direction
    {
        left,
        right
    }

    // Start is called before the first frame update
    void Start()
    {
        ragdollHandler = GetComponent<RagdollHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ragdollHandler.IsRagdoll)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Nudge(direction.left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Nudge(direction.right);
            }
        }
    }

    private void Nudge(direction dir)
    {
        switch (dir)
        {
            case direction.left:
                target.AddForce(Vector3.left, ForceMode.Impulse);
                break;
            case direction.right:
                target.AddForce(Vector3.right, ForceMode.Impulse);
                break;
        }
    }         
}
