using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class SpawnManager : MonoBehaviour
{
    string[] prefabsPaths = {"Prefabs/ Maça Dourada" , "Prefabs/ Maça Verde", "Prefabs/ Maça Vermelha"};
    private float timer;
    private const float cooldown = 1f;

    void Start()
    {
        timer = cooldown;
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            float appleIndex = Random.Range(0f, 1f);
            string appleSelected;

            switch (appleIndex)
            {
                case < 0.5f:
                    appleSelected = prefabsPaths[0];
                    break;
                case < 0.8f:
                    appleSelected = prefabsPaths[1];
                    break;
                default:
                    appleSelected = prefabsPaths[2];
                    break;
            }

            NetworkManager.instance.Instantiate(appleSelected, new Vector2(Random.Range(-GameManager.instance.ScreenBounds.x, GameManager.instance.ScreenBounds.x), GameManager.instance.ScreenBounds.y), Quaternion.identity);
            timer = cooldown;
        }
    }
}
