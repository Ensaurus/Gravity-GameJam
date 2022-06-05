using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public float remainingPlatforms = 5;
    public TextMeshProUGUI remainingPlatformsCounter;
    
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBounding.instance.boundIsActive && Input.GetKeyDown(KeyCode.Space) && remainingPlatforms > 0)
        {
            SpawnPlatform();
            remainingPlatforms--;
            remainingPlatformsCounter.text = "Platforms: " + remainingPlatforms;
        }
    }

    private void SpawnPlatform()
    {
        float lowerBound = PlayerBounding.instance.lowerBoundTransform.position.y;
        float spawnY = playerTransform.position.y - spawnDistance;
        if (spawnY < lowerBound)
        {
            float upperBound = PlayerBounding.instance.upperBoundTransform.position.y;
            float extraLength = lowerBound - spawnY;
            spawnY = upperBound - extraLength;
        }
        // just using upperBound Transform x as midpoint of tile
        Instantiate(platform, new Vector3(PlayerBounding.instance.upperBoundTransform.position.x, spawnY, playerTransform.position.z), Quaternion.Euler(-90, 0, 0));
    }
}
