using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{
    public Transform[] spawnPoints;
    private List<GameObject> objectsOnCylinder = new List<GameObject>();
    private bool[] spawnPointOccupied;
    private string objectTypeOnCylinder = null;
    public int maxObjects = 3;
    public float rejectionForce = 200f; 
    public AudioClip rejectionSFX; 
    private AudioSource audioSource; 
    public AudioClip positiveSFX; 
    public AudioClip duplicateTagSFX; 
    public GameObject duplicateTagVFX; 

    void Start()
    {
        spawnPointOccupied = new bool[spawnPoints.Length];
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (objectsOnCylinder.Contains(other.gameObject))
        {
            return;
        }

        if (objectsOnCylinder.Count >= maxObjects)
        {
            Debug.Log("Silindir dolu, daha fazla nesne eklenemez!");
            RejectObject(other.gameObject);
            return;
        }

        if (objectsOnCylinder.Count == 0)
        {
            objectTypeOnCylinder = other.gameObject.tag;
            MoveToSpawnPoint(other.gameObject);
        }
        else
        {
            if (other.gameObject.tag == objectTypeOnCylinder)
            {
                MoveToSpawnPoint(other.gameObject);

                if (objectsOnCylinder.FindAll(obj => obj.tag == objectTypeOnCylinder).Count == 2)
                {
                    PlayDuplicateTagEffect();
                }
            }
            else
            {
                Debug.Log("Farklı tipte bir nesne eklenemez!");
                RejectObject(other.gameObject);
            }
        }
    }

    private void MoveToSpawnPoint(GameObject obj)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!spawnPointOccupied[i])
            {
                Transform spawnPoint = spawnPoints[i];
                obj.transform.position = spawnPoint.position;
                obj.transform.rotation = spawnPoint.rotation;
                obj.GetComponent<Rigidbody>().isKinematic = true;

                if (obj.TryGetComponent(out DragObject dragScript))
                {
                    Destroy(dragScript); 
                }

                if (positiveSFX != null && audioSource != null)
                {
                    audioSource.PlayOneShot(positiveSFX); 
                }

                spawnPointOccupied[i] = true;
                objectsOnCylinder.Add(obj);

                Debug.Log($"Nesne {spawnPoint.name} noktasına yerleştirildi: {obj.name}");
                return;
            }
        }

        Debug.Log("Uygun bir spawn noktası bulunamadı.");
    }

    private void RejectObject(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 rejectionDirection = (obj.transform.position - transform.position).normalized; 
            rb.AddForce(rejectionDirection * rejectionForce);
            Debug.Log($"Nesne geriye itildi: {obj.name}");
            
            if (rejectionSFX != null && audioSource != null)
            {
                audioSource.PlayOneShot(rejectionSFX); 
            }
        }
    }

    private void PlayDuplicateTagEffect()
    {
        if (duplicateTagSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(duplicateTagSFX);
            Debug.Log("Aynı tag'e sahip 2 nesne silindir üzerinde!");
        }

        if (duplicateTagVFX != null)
        {
            Instantiate(duplicateTagVFX, transform.position, Quaternion.identity);
            Debug.Log("Duplicate tag VFX oynatıldı.");
        }
    }
}
