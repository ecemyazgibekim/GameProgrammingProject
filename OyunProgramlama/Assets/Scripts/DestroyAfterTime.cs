using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyTime = 5f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
