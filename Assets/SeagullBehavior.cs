using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SeagullBehavior : MonoBehaviour
{
    public float tileWidth;
    private int state = 0;
    public float tileEdge;

    private float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3((tileWidth / 2), 
            transform.localPosition.y, 10);
        if (Random.value > 0.7)
        {
            GetComponent<AudioSource>().time = Random.Range(0f, 15f);
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            transform.Translate(0, 0, -Time.deltaTime * speed, Space.World);
            if (transform.localPosition.z <= -2)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                state = 1;
            }
        } else if (state == 1)
        {
            transform.Translate(-Time.deltaTime * speed, 0, 0, Space.World);
            if (transform.localPosition.x <= -tileWidth/2)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                state = 2;
            }
        } else if (state == 2)
        {
            transform.Translate(0, 0, Time.deltaTime * speed, Space.World);
            if (transform.localPosition.z >= 10)
            {
                Destroy(gameObject);
            }
        }
    }

    /*private void OnCollisionStay(Collision collisionInfo)
    {
        Debug.Log("Collision!");
        Destroy(gameObject);
    }*/
}
