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
    public float rejectionForce = 200f; // Kuvvetin büyüklüğü

    void Start()
    {
        spawnPointOccupied = new bool[spawnPoints.Length];
    }

    void OnTriggerEnter(Collider other)
    {
        // Eğer nesne zaten listede varsa, işlemi sonlandır
        if (objectsOnCylinder.Contains(other.gameObject))
        {
            return;
        }

        // Tüm spawn noktaları doluysa, nesneyi reddet
        if (objectsOnCylinder.Count >= maxObjects)
        {
            Debug.Log("Silindir dolu, daha fazla nesne eklenemez!");
            RejectObject(other.gameObject);
            return;
        }

        // İlk nesneyi yerleştirirken türünü belirle
        if (objectsOnCylinder.Count == 0)
        {
            objectTypeOnCylinder = other.gameObject.tag;
            MoveToSpawnPoint(other.gameObject);
        }
        else
        {
            // Nesne türü eşleşiyorsa ekle, aksi takdirde reddet
            if (other.gameObject.tag == objectTypeOnCylinder)
            {
                MoveToSpawnPoint(other.gameObject);
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
                    Destroy(dragScript); // Sürükleme işlemini devre dışı bırak
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
            Vector3 rejectionDirection = (obj.transform.position - transform.position).normalized; // Silindirin merkezinden uzağa doğru yön
            rb.AddForce(rejectionDirection * rejectionForce);
            Debug.Log($"Nesne geriye itildi: {obj.name}");
        }
    }
}
