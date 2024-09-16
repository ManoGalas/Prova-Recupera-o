using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class SpawnManager : MonoBehaviour
{
    public GameObject[] applePrefabs;
    private float timer;
    private const float cooldown = 1f;

    void Start()
    {
        timer = cooldown;
    }

    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            float appleIndex = Random.Range(0f, 1f);
            GameObject appleSelected;

            if (appleIndex <= 0.5f)
            {
                appleSelected = applePrefabs[0];
            }
            else if (appleIndex <= 0.8f)
            {
                appleSelected = applePrefabs[1];
            }
            else
            {
                appleSelected = applePrefabs[2];
            }

        }
    }
}
