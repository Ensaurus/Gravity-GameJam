using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner instance { get; private set; }
    public GameObject platform;
    // Note this should be in reference to the ragdoll, not the regular. 
    public Transform playerTransform;
    public float spawnDistance;
    public float platformSpeed = 1;
    public float platformLifeTime = 5;  // in seconds

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPlatform();
        }
    }

    private void SpawnPlatform()
    {
        Instantiate(platform);
        platform.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - spawnDistance, playerTransform.position.z);
    }
}
