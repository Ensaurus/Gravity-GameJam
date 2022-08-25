using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NudgeHandler : MonoBehaviour
{
    private RagdollHandler ragdollHandler;
    //public Rigidbody[] ragdollParts;
    public Rigidbody target;
    public float nudgeStrength = 50;
    public UIManager UIManager;
    public bool nudgeUsed = false;
    public float maxNudge;
    public float currentNudges;

    private enum direction
    {
        left,
        right,
        up,
        down
    }

    // Start is called before the first frame update
    void Start()
    {
        nudgeUsed = false;
        // ragdollParts = gameObject.GetComponentsInChildren<Rigidbody>();
        ragdollHandler = GetComponent<RagdollHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        nudgeUsed = false;
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
            else if (Input.GetKeyDown(KeyCode.W) && UIManager.currentNudges > 0 && UIManager.currentNudges <= UIManager.maxNudges)
            {
                Nudge(direction.up);
                nudgeUsed = true;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Nudge(direction.down);
            }
        }
    }

    private void Nudge(direction dir)
    {
        switch (dir)
        {
            case direction.left:
                target.AddForce(Vector3.left * nudgeStrength, ForceMode.Impulse);
                break;
            case direction.right:
                target.AddForce(Vector3.right * nudgeStrength, ForceMode.Impulse);
                break;
            case direction.up:
                target.AddForce(Vector3.up * nudgeStrength, ForceMode.Impulse);
                break;
            case direction.down:
                target.AddForce(Vector3.down * nudgeStrength, ForceMode.Impulse);
                break;
            default:
                break;
        }
    }         
}
