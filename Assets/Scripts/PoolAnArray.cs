using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAnArray : MonoBehaviour
{
	[SerializeField] private GameObject[] objectsToPool;
	[SerializeField] private int poolDepth;
	[SerializeField] private bool canGrow = true;

	private List<GameObject>[] poolArray;  // array of the different pool lists, of length objectsToPool.Length  readonly

	private void Awake()
	{
		poolArray = new List<GameObject>[objectsToPool.Length];

		for (int i = 0; i < poolArray.Length; i++)
		{
			poolArray[i] = new List<GameObject>(); 

			for (int j = 0; j < poolDepth; j++)
			{
				GameObject pooledObject = Instantiate(objectsToPool[i], gameObject.transform);
				pooledObject.SetActive(false);
				poolArray[i].Add(pooledObject);
			}
		}
	}

	public GameObject GetRandomObject()
	{
		int listArrayIndex = Random.Range(0, poolArray.Length);     // get a random pool list of game objects from the poolArray 

		for (int listIndex = 0; listIndex < poolArray[listArrayIndex].Count; listIndex++)
		{
			if (poolArray[listArrayIndex][listIndex].activeInHierarchy == false)
			{   // if the list at the listArrayIndex has a gameobject at one of its indexes but isn't active
				return poolArray[listArrayIndex][listIndex];
			}
		}

		if (canGrow == true)
		{
			GameObject pooledObject = Instantiate(poolArray[listArrayIndex][0]);    // just took the first gameobject of the randomly chosen list
			pooledObject.SetActive(false);
			poolArray[listArrayIndex].Add(pooledObject);
			return pooledObject;
		}
		else
		{
			return null;
		}
	}

	public void Reset()
	{
		for (int listArrayIndex = 0; listArrayIndex < poolArray.Length; listArrayIndex++)
		{
			for (int listIndex = 0; listIndex < poolArray[listArrayIndex].Count; listIndex++)
			{
				if (poolArray[listArrayIndex][listIndex].activeInHierarchy)
				{
					poolArray[listArrayIndex][listIndex].SetActive(false);
				}
			}
		}
	}
}