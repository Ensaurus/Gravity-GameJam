using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class ProceduralLevel : MonoBehaviour
{
    public int maxObjects = 100;
    public Transform playerTransform;
    public float spawnRadius = 10;

    public GameObject seagull;
    // public GameObject[] cliffObjects;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private float playerStartYPos = 0.0f;

    private PoolAnArray cliffObjectsPool;

    public Transform upperBoundTransform;
    public Transform lowerBoundTransform;
    public Transform leftBoundTransform;
    public Transform rightBoundTransform;

    public float offset = 0;

    void SpawnObject(float ymin, float ymax, float startY)
    {
        var position = playerTransform.position;
        float newSpawnY = Random.Range(ymin, ymax);
        float sparsity = (-(newSpawnY - startY) + 100f) * 0.01f;
        float newSpawnPosX = Random.Range(position.x-(50.0f * sparsity), 
            position.x + (50.0f * sparsity));
        Vector3 newSpawnPos = new Vector3(newSpawnPosX, newSpawnY, position.z);

        //point towards center

        // GameObject newObject = Instantiate(cliffObjects[Random.Range(0, cliffObjects.Length)], 
        //    newSpawnPos,
        //    Quaternion.Euler(-90f, 0f, 0f), transform);
        GameObject newObject = cliffObjectsPool.GetRandomObject();
        newObject.transform.position = newSpawnPos;
        newObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        newObject.AddComponent<MeshCollider>();
       
        // whyyyyyy
        if (Random.value > 0.5)
            newObject.transform.localScale = new Vector3(-100f, 100f, 100f);
        newObject.SetActive(true);
        spawnedObjects.Add(newObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        cliffObjectsPool = gameObject.GetComponent<PoolAnArray>();
        SpawnInBox(leftBoundTransform.position.x,
            rightBoundTransform.position.x,
            lowerBoundTransform.position.y,
            upperBoundTransform.position.y);
        PlayerBounding.instance.SwitchedActiveTile.AddListener(SwitchedTileListener);
        /*
        var position = playerTransform.position;
        playerStartYPos = position.y;
        while (spawnedObjects.Count < maxObjects)
        {
            SpawnObject(position.y - 150, position.y - 50, 1.0f);
        }
        */
    }

    private void SwitchedTileListener(GameObject formerActive)
    {
        if (formerActive == gameObject)
        {
            SpawnInBox(leftBoundTransform.position.x,
                rightBoundTransform.position.x,
                lowerBoundTransform.position.y,
                upperBoundTransform.position.y);
        }
    }

    void SpawnInBox(float xmin, float xmax, float ymin, float ymax)
    {
        if (spawnedObjects.Count > 0)
        {
            for (int i = spawnedObjects.Count - 1; i >= 0; i--)
            {
                spawnedObjects[i].SetActive(false);
                spawnedObjects.RemoveAt(i);
                /*
                Destroy(spawnedObjects[i]);
                spawnedObjects.RemoveAt(i);
                */
            }
        }
        UniformPoissonDiskSampler sampler = new UniformPoissonDiskSampler(xmax - xmin - offset, ymax - ymin - offset, spawnRadius);     //  Mathf.Abs(xmax - xmin), Mathf.Abs(ymax - ymin), 20);
        foreach (var sample in sampler.Samples())
        {
            Vector3 spawnPos = new Vector3(sample.x + xmin + offset/2, sample.y + ymin + offset/2, playerTransform.position.z);
            /*
            GameObject newObject = Instantiate(cliffObjects[Random.Range(0, cliffObjects.Length)], 
                spawnPos,
                Quaternion.identity);
            newObject.transform.SetParent(transform);
            newObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            */
            GameObject newObject = cliffObjectsPool.GetRandomObject();
            newObject.transform.position = spawnPos;
            newObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            newObject.AddComponent<MeshCollider>();
            
            // whyyyyyy
            // for flipping orientation :)
            if (Random.value > 0.5)
                newObject.transform.localScale = new Vector3(-newObject.transform.localScale.x, newObject.transform.localScale.y, newObject.transform.localScale.z);
            newObject.SetActive(true);
            spawnedObjects.Add(newObject);
        }

        if (Random.value > 0.3)
        {
            Instantiate(seagull, transform);
        }
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
        var position = playerTransform.position;

        List<GameObject> objToRemove = new List<GameObject>();
        // remove objects out of range
        foreach (var obj in spawnedObjects)
        {
            if (obj.transform.position.y > playerTransform.position.y + 50)
            {
                objToRemove.Add(obj);
            }
        }
        foreach (var obj in objToRemove)
        {
            Destroy(obj);
            spawnedObjects.Remove(obj);
        }
        
        // spawn in some new objects
        if (spawnedObjects.Count < maxObjects)
        {
            while (spawnedObjects.Count < maxObjects)
            {
                SpawnObject(position.y - 100, position.y - 50, playerStartYPos);
            }
        }
    }
    */
}
