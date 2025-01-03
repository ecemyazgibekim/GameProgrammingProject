using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour
{
    public float deactivateTime = 5f;

    void Update()
    {
        Invoke("DeactivateGameObject", deactivateTime);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}

