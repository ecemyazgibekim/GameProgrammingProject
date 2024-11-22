using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawnable Object List")]
    public List<GameObject> spawnableObjects; 
    public int spawnCount; 

    [Header("Spawn Area")]
    public Collider spawnArea; 

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        if (spawnableObjects.Count == 0 || spawnArea == null || spawnCount <= 0) 
        {
            Debug.LogWarning("Spawn işlemi için gerekli ayarlar eksik.");
            return;
        }

        List<GameObject> objectsToSpawn = new List<GameObject>(spawnableObjects);

        for (int i = 0; i < spawnCount; i += 2)
        {
            if (objectsToSpawn.Count == 0) break;
            GameObject selectedObject = objectsToSpawn[Random.Range(0, objectsToSpawn.Count)];
            SpawnObject(selectedObject);
            SpawnObject(selectedObject);
        }
    }

    private void SpawnObject(GameObject obj)
    {
        Vector3 spawnPosition = GetRandomPointInBounds(spawnArea.bounds);
         GameObject spawnedObject = Instantiate(obj, spawnPosition, Quaternion.identity, this.transform);
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
}
