using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawnable Object List")]
    public List<GameObject> spawnableObjects; 

    [Header("Spawn Area")]
    public Collider spawnArea; 

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        if (spawnableObjects.Count == 0 || spawnArea == null) 
        {
            Debug.LogWarning("Spawn işlemi için gerekli ayarlar eksik.");
            return;
        }

        foreach (GameObject obj in spawnableObjects)
        {
            for (int i = 0; i < 2; i++)
            {
                SpawnObject(obj);
            }
        }
    }

    private void SpawnObject(GameObject obj)
    {
        Vector3 spawnPosition = GetRandomPointInBounds(spawnArea.bounds);
        Instantiate(obj, spawnPosition, Quaternion.identity, this.transform);
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
}
