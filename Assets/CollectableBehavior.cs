using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehavior : MonoBehaviour
{
    private float starty;
    private bool firstEntry = true;

    // Start is called before the first frame update
    void Start()
    {
        starty = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.localPosition;
        position = new Vector3(position.x, 
            starty + (Mathf.Sin(Time.frameCount * 0.02f) * 0.1f), 
            position.z);
        transform.localPosition = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && firstEntry)
        {
            firstEntry = false;
            FindObjectOfType<PlatformSpawner>().AddPlatformToAvailable();
            //TODO: add one to player's platform count
            Destroy(gameObject);
        }
    }
}
