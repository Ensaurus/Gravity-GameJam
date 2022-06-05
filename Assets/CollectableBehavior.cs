using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehavior : MonoBehaviour
{
    private float starty;
    // Start is called before the first frame update
    void Start()
    {
        starty = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        position = new Vector3(position.x, 
            starty + (Mathf.Sin(Time.frameCount * 0.02f) * 0.1f), 
            position.z);
        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO: add one to player's platform count
        Destroy(gameObject);
    }
}
