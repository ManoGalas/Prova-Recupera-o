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
    private Rigidbody2D rigidbody2D;

    public Rigidbody2D Rb { get => rigidbody2D;}

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidbody2D.velocity = Vector2.down * speed;

        if (transform.position.y < GameManager.instance.ScreenBounds.y)
        {
            if(photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    public void DestroyAppleRPC()
    {
        photonView.RPC("DestroyApple", RpcTarget.AllBuffered);
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
