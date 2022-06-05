using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    /*
    private MeshRenderer myRend;
    public float speed = 0.5f;

    private void Start()
    {
        myRend = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        Vector2 offset = new Vector2(Time.deltaTime * speed, 0);

        myRend.material.mainTextureOffset = offset;
    }
    */

    public CharacterMovement characterController;
    public float speed = 0.5f;
    public Transform leftBound;
    public Transform rightBound;

    // Update is called once per frame
    void Update()
    {
        if (characterController.isMoving)
        {
            if (transform.position.x <= leftBound.position.x)
            {
                transform.position = new Vector3(rightBound.position.x, transform.position.y, transform.position.z);
            }
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
    
}
