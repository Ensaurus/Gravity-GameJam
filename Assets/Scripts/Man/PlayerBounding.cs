using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounding : MonoBehaviour
{
    public Transform upperBoundTransform;
    public Transform lowerBoundTransform;
    public Transform leftBoundTransform;
    public Transform rightBoundTransform;

    
    private float upperBound;
    private float lowerBound;
    private float leftBound;
    private float rightBound;

    private Transform myTransform;

    public float spawnBuffer;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        upperBound = upperBoundTransform.position.y;
        lowerBound = lowerBoundTransform.position.y;
        leftBound = leftBoundTransform.position.x;
        rightBound = rightBoundTransform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(myTransform.position.y < lowerBound)
        {
            myTransform.position = new Vector3(myTransform.position.x, upperBound - spawnBuffer, myTransform.position.z);
        }
        else if (myTransform.position.y > upperBound)
        {
            myTransform.position = new Vector3(myTransform.position.x, upperBound + spawnBuffer, myTransform.position.z);
        }
        if (myTransform.position.x < leftBound)
        {
            myTransform.position = new Vector3(rightBound-spawnBuffer, myTransform.position.y, myTransform.position.z);
        }
        else if (myTransform.position.x > rightBound)
        {
            myTransform.position = new Vector3(leftBound+spawnBuffer, myTransform.position.y, myTransform.position.z);
        }
    }
}
