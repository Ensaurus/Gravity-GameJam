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

    public bool goingRight = false;

    private void OnEnable()
    {
        state = 0;
        GetComponent<AudioSource>().volume = PersistentData.instance.gameVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            transform.Translate(0, 0, Time.deltaTime * speed, Space.World);
            if (transform.localPosition.z > -2)
            {
                if (goingRight)
                {
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                state = 1;
            }
        } else if (state == 1)
        {
            if (goingRight)
            {
                transform.Translate(Time.deltaTime * speed, 0, 0, Space.World);
                if (transform.localPosition.x >= tileWidth / 2)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    state = 2;
                }
            }
            else
            {
                transform.Translate(-Time.deltaTime * speed, 0, 0, Space.World);
                if (transform.localPosition.x <= -tileWidth / 2)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    state = 2;
                }
            }
        } else if (state == 2)
        {
            // keep this gull relative to currently active tile
            transform.parent = PlayerBounding.instance.activeTile.transform;
            transform.Translate(0, 0, Time.deltaTime * speed, Space.World);
            if (transform.localPosition.z >= 200)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
