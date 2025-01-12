using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    //public TMP_Text timerText; 
    public GameObject failurePanel; 
    public GameObject successPanel; 
    //private float timer = 300f; 
    private bool gameRunning = true; 
    public SpawnManager spawnManager;
    private bool isSpawning = false;

    [Header("Double Points Skill")]
    public Button doublePointsButton;
    public TMP_Text doublePointsTimerText;
    public GameObject doublePointsShowText;
    private bool isDoublePointsActive = false;
    private float doublePointsDuration = 5f;

    [Header("Slow Motion Skill")]
    public Button slowMotionButton;
    public TMP_Text slowMotionTimerText;
    private bool isSlowMotionActive = false;
    private float slowMotionDuration = 5f;
    private float normalTimeScale = 1f;
    public GameObject slowMotionShowText;

    [Header("Lift Objects Skill")]
    public Button liftObjectsButton;
    public TMP_Text liftObjectsTimerText;
    private bool isLiftObjectsActive = false;
    private float liftObjectsDuration = 5f;

    void Start()
    {
        //StartCoroutine(TimerCountdown());
        spawnPointOccupied = new bool[spawnPoints.Length];
        audioSource = gameObject.AddComponent<AudioSource>();
        UpdateScoreText();
        failurePanel.SetActive(false); 
        successPanel.SetActive(false); 
        SpawnObjectsIfNeeded(); 
        doublePointsShowText.SetActive(false);
        slowMotionShowText.SetActive(false);

        if (doublePointsButton != null)
        {
            doublePointsButton.onClick.AddListener(ActivateDoublePoints);
        }

        if (slowMotionButton != null)
        {
            slowMotionButton.onClick.AddListener(ActivateSlowMotion);
        }

        if (liftObjectsButton != null)
        {
            liftObjectsButton.onClick.AddListener(ActivateLiftObjects);
        }
    }

    void Update()
    {
        if (gameRunning) 
        {
            //UpdateTimerText(); 

            if (score >= 1150 || AreAllPrefabsCleared()) 
            {
                //GameSuccess(); 
                SpawnObjectsIfNeeded();
            }
        }
    }

    public bool AreAllPrefabsCleared()
    {
        return spawnManager.AllObjectsCleared();
    }

    private void SpawnObjectsIfNeeded() 
    {
        if (!isSpawning && AreAllPrefabsCleared())
        {
            isSpawning = true;
            spawnManager.SpawnObjects(); 
            isSpawning = false;
        }
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
                    GameObject obj1 = objectsOnCylinder.Find(obj => obj.tag == objectTypeOnCylinder);
                    GameObject obj2 = objectsOnCylinder.FindLast(obj => obj.tag == objectTypeOnCylinder);
                    StartCoroutine(AnimateMatchingObjects(obj1, obj2));
                    IncreaseScore(50);
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

    private void ActivateDoublePoints()
    {
        if (!isDoublePointsActive)
        {
            StartCoroutine(DoublePointsRoutine());
        }
    }

    private IEnumerator DoublePointsRoutine()
    {
        isDoublePointsActive = true;
        doublePointsButton.interactable = false;
        float countdown = doublePointsDuration;

        while (countdown > 0)
        {
            doublePointsTimerText.text = countdown.ToString("F0");
            yield return new WaitForSeconds(1f);
            countdown--;
            doublePointsShowText.SetActive(true);
        }

        doublePointsTimerText.text = "";
        isDoublePointsActive = false;
        doublePointsButton.interactable = true;
        doublePointsShowText.SetActive(false);
    }

    private void ActivateSlowMotion()
    {
        if (!isSlowMotionActive)
        {
            StartCoroutine(SlowMotionRoutine());
        }
    }

    private IEnumerator SlowMotionRoutine()
    {
        isSlowMotionActive = true;
        slowMotionButton.interactable = false;
        Time.timeScale = 0.3f; 
        float countdown = slowMotionDuration;

        while (countdown > 0)
        {
            slowMotionTimerText.text = countdown.ToString("0");
            yield return new WaitForSecondsRealtime(1f);
            countdown--;
            slowMotionShowText.SetActive(true);
        }

        Time.timeScale = normalTimeScale; 
        slowMotionTimerText.text = "";
        isSlowMotionActive = false;
        slowMotionButton.interactable = true;
        slowMotionShowText.SetActive(false);
    }

    private IEnumerator LiftObjectsRoutine()
    {
        isLiftObjectsActive = true;
        liftObjectsButton.interactable = false;
        float countdown = liftObjectsDuration;

        foreach (GameObject obj in spawnManager.spawnedObjects)
        {
            if (obj != null && obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddForce(Vector3.up * 500f); // Nesnelere yukarıya doğru kuvvet uygula
            }
        }

        while (countdown > 0)
        {
            liftObjectsTimerText.text = countdown.ToString("0");
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        liftObjectsTimerText.text = "";
        isLiftObjectsActive = false;
        liftObjectsButton.interactable = true;
    }

    private void ActivateLiftObjects()
    {
        if (!isLiftObjectsActive)
        {
            StartCoroutine(LiftObjectsRoutine());
        }
    }

    private void IncreaseScore(int amount)
    {
        if (isDoublePointsActive)
        {
            amount *= 2; 
        }
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

    //private IEnumerator TimerCountdown() 
    //{
    //    while (timer > 0)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        timer -= 1f; 
    //        UpdateTimerText(); 
    //    }
    //
    //    GameOver(); 
    //}

    //private void UpdateTimerText() 
    //{
    //    if (timerText != null)
    //    {
    //        int minutes = Mathf.FloorToInt(timer / 60f);
    //        int seconds = Mathf.FloorToInt(timer % 60f);
    //        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
    //    }
    //}

    private IEnumerator AnimateMatchingObjects(GameObject obj1, GameObject obj2)
    {
        Vector3 midpoint = (obj1.transform.position + obj2.transform.position) / 2;
        float duration = 0.5f;
        float elapsedTime = 0f;

        Vector3 startPos1 = obj1.transform.position;
        Vector3 startPos2 = obj2.transform.position;

        if (duplicateTagSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(duplicateTagSFX);
        }

         if (duplicateTagVFX != null)
        {
            Instantiate(duplicateTagVFX, obj1.transform.position, Quaternion.identity);
            Instantiate(duplicateTagVFX, obj2.transform.position, Quaternion.identity);
        }

        while (elapsedTime < duration)
        {
            obj1.transform.position = Vector3.Lerp(startPos1, midpoint, elapsedTime / duration);
            obj2.transform.position = Vector3.Lerp(startPos2, midpoint, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(ClearSpawnPointsAfterDelay(0.2f));
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
