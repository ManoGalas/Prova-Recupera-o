using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    const int speed = 10;
    float direction;
    Rigidbody2D rb2d;
    private bool playerLocal;
    GameManager gameManager;
    Apple apple;

    // Start is called before the first frame update
    void Start()
    {
        playerLocal = photonView.IsMine;

        rb2d = GetComponent<Rigidbody2D>();

        if (!playerLocal)
        {
            Color color = Color.white;
            color.a = 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerLocal)
        {
            direction = Input.GetAxis("Horizontal");
        }
        Move();
    }

    void Move()
    {
        
        rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.x);
        
        float clampedX = Mathf.Clamp(transform.position.x, GameManager.instance.ScreenBounds.x, GameManager.instance.ScreenBounds.x);
        transform.position = new Vector2(clampedX, transform.position.x);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Apple") && playerLocal)
        {
            gameManager.photonView.RPC("AddScore", RpcTarget.All);

            apple.photonView.RPC("DestroyApple", RpcTarget.All);
        }
    }
    

}
