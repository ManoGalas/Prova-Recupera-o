using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEditor;
using UnityEngine.SocialPlatforms.Impl;
using Photon.Pun.Demo.PunBasics;
using Unity.VisualScripting;

public class Apple : MonoBehaviourPun
{
    const int speed = 5;
    [SerializeField] int score;
    private Rigidbody rigidbody2D;

    public Rigidbody Rb { get => rigidbody2D; set => rigidbody2D = value;}

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigidbody2D.velocity = Vector2.down * speed;

        if (transform.position.y < Camera.main.transform.position.y)
        {
            if(photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    [PunRPC]
    public void DestroyApple()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    } 
}
