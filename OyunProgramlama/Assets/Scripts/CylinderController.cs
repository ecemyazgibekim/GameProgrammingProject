using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CylinderController : MonoBehaviour
{
    public Transform[] spawnPoints;
    private List<GameObject> objectsOnCylinder = new List<GameObject>();
    private bool[] spawnPointOccupied;
    private string objectTypeOnCylinder = null;
    public int maxObjects = 300;
    public float rejectionForce = 900f; 
    public AudioClip rejectionSFX; 
    private AudioSource audioSource; 
    public AudioClip positiveSFX; 
    public AudioClip duplicateTagSFX; 
    public GameObject duplicateTagVFX; 
    public TMP_Text scoreText;
    private int score = 0; 
    public TMP_Text timerText; 
    public GameObject failurePanel; 
    public GameObject successPanel; 
    private float timer = 300f; 
    private bool gameRunning = true; 
    public SpawnManager spawnManager;

    void Start()
    {
        StartCoroutine(TimerCountdown());
        spawnPointOccupied = new bool[spawnPoints.Length];
        audioSource = gameObject.AddComponent<AudioSource>();
        UpdateScoreText();
        failurePanel.SetActive(false); 
        successPanel.SetActive(false); 
    }

    void Update()
    {
        if (gameRunning) 
        {
            UpdateTimerText(); 

            if (score >= 1150 || AreAllPrefabsCleared()) 
            {
                GameSuccess();
            }
        }
    }

    private bool AreAllPrefabsCleared()
    {
        foreach (GameObject obj in spawnManager.spawnedObjects)
        {
            if (obj != null) 
            {
                return false;
            }
        }
        return true; 
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
                    IncreaseScore(50);
                    StartCoroutine(ClearSpawnPointsAfterDelay(0.5f));
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
        }
    }

    private void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private IEnumerator ClearSpawnPointsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (Transform spawnPoint in spawnPoints)
        {
            foreach (Transform child in spawnPoint)
            {
                Destroy(child.gameObject); 
            }
        }

        foreach (GameObject obj in objectsOnCylinder)
        {
            Destroy(obj); 
        }

        objectsOnCylinder.Clear(); 
        for (int i = 0; i < spawnPointOccupied.Length; i++)
        {
            spawnPointOccupied[i] = false; 
        }

        Debug.Log("Spawn noktaları ve sahnedeki nesneler temizlendi.");
    }

    private IEnumerator TimerCountdown() 
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer -= 1f; 
            UpdateTimerText(); 
        }

        GameOver(); 
    }

    private void UpdateTimerText() 
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
        }
    }

    private void GameOver() 
    {
        gameRunning = false;
        Time.timeScale = 0f; 
        failurePanel.SetActive(true); 
    }

    private void GameSuccess() 
    {
        gameRunning = false;
        Time.timeScale = 0f; 
        successPanel.SetActive(true); 
    }

}
