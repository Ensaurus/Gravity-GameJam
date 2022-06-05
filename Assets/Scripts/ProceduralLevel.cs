using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class ProceduralLevel : MonoBehaviour
{

    public int maxObjects = 100;
    public Transform playerTransform;
    public GameObject[] cliffObjects;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private float playerStartYPos = 0.0f;

    void SpawnObject(float ymin, float ymax, float startY)
    {
        var position = playerTransform.position;
        float newSpawnY = Random.Range(ymin, ymax);
        float sparsity = (-(newSpawnY - startY) + 100f) * 0.01f;
        float newSpawnPosX = Random.Range(position.x-(50.0f * sparsity), 
            position.x + (50.0f * sparsity));
        Vector3 newSpawnPos = new Vector3(newSpawnPosX, newSpawnY, position.z);
        
        //point towards center

        GameObject newObject = Instantiate(cliffObjects[Random.Range(0, cliffObjects.Length)], 
            newSpawnPos,
            Quaternion.identity);
        newObject.transform.SetParent(transform);
        newObject.AddComponent<MeshCollider>();
        //newObject.transform.LookAt(new Vector3(position.x, newObject.transform.position.y, position.z));
        //newObject.transform.localScale = Vector3.one * sparsity;
        //newObject.transform.rotation = Random.rotation;
        newObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
       
        // whyyyyyy
        if (Random.value > 0.5)
            newObject.transform.localScale = new Vector3(-100f, 100f, 100f);
        
        spawnedObjects.Add(newObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        var position = playerTransform.position;
        playerStartYPos = position.y;
        while (spawnedObjects.Count < maxObjects)
        {
            SpawnObject(position.y - 150, position.y - 50, 1.0f);
        }
    }

    void SpawnInBox(float xmin, float xmax, float ymin, float ymax, float sparsity)
    {
        if (spawnedObjects.Count > 0)
        {
            for (int i = spawnedObjects.Count; i >= 0; i++)
            {
                Destroy(spawnedObjects[i]);
                spawnedObjects.RemoveAt(i);
            }
        }

        /*while (spawnedObjects.Count < (maxObjects * sparsity))
        {
            Vector3 spawnPos = new Vector3(Random.Range(xmin, xmax), Random.Range(ymin, ymax), 0f);
            GameObject newObject = Instantiate(cliffObjects[Random.Range(0, cliffObjects.Length)], 
                spawnPos,
                Quaternion.identity);
            newObject.AddComponent<MeshCollider>();
            newObject.transform.SetParent(transform);
            newObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
       
            // whyyyyyy
            if (Random.value > 0.5)
                newObject.transform.localScale = new Vector3(-100f, 100f, 100f);
        
            spawnedObjects.Add(newObject);
        }*/
        UniformPoissonDiskSampler sampler = new UniformPoissonDiskSampler(xmax - xmin, ymax - ymin, 20);
        foreach (var sample in sampler.Samples())
        {
            Vector3 spawnPos = new Vector3(sample.x + xmin, sample.y + ymin, 0f);
            GameObject newObject = Instantiate(cliffObjects[Random.Range(0, cliffObjects.Length)], 
                spawnPos,
                Quaternion.identity);
            newObject.AddComponent<MeshCollider>();
            newObject.transform.SetParent(transform);
            newObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
       
            // whyyyyyy
            if (Random.value > 0.5)
                newObject.transform.localScale = new Vector3(-100f, 100f, 100f);
        
            spawnedObjects.Add(newObject);
        }
    }

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
}
