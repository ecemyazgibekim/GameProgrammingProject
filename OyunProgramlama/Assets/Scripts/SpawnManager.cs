using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawnable Object List")]
    public List<GameObject> spawnableObjects; 

    [Header("Spawn Area")]
    public Collider spawnArea; 

    public List<GameObject> spawnedObjects = new List<GameObject>(); 

    private bool hasSpawned = false; 

    public GameObject borderArea;

    public void Start()
    {
        if (spawnableObjects == null || spawnableObjects.Count == 0)
        {
            Debug.LogError("Spawn edilecek nesneler listesi boş! Lütfen spawnableObjects listesine nesneler ekleyin.");
            return;
        }

        if (spawnArea == null)
        {
            Debug.LogError("Spawn alanı atanmadı! Lütfen spawnArea değişkenine bir collider atayın.");
            return;
        }

        SpawnObjects();
    }

    public void Update()
    {
        if (AllObjectsCleared() && !hasSpawned)
        {
            SpawnObjects();
            hasSpawned = true; 
        }
        else if (!AllObjectsCleared())
        {
            hasSpawned = false; 
        }
    }

    public void SpawnObjects()
    {
        foreach (GameObject obj in spawnableObjects)
        {
            for (int i = 0; i < 2; i++) 
            {
                SpawnObject(obj);
            }
        }

        borderArea.SetActive(true);
    }

    private void SpawnObject(GameObject obj)
    {
        Vector3 spawnPosition = GetRandomPointInBounds(spawnArea.bounds);
        GameObject spawnedObject = Instantiate(obj, spawnPosition, Quaternion.identity, this.transform);
        spawnedObjects.Add(spawnedObject); 
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }

    public void ClearSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        spawnedObjects.Clear();
        Debug.Log("Tüm spawn edilmiş nesneler temizlendi.");
    }

    public bool AllObjectsCleared()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                return false; 
            }
        }
        return true; 
    }
}
