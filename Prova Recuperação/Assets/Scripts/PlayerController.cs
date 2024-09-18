using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        rb2d = GetComponent<Rigidbody2D>();
        playerLocal = photonView.IsMine;

        if (!playerLocal)
        {
            Color color = Color.white;
            color.a = 0.1f;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {    
        if (playerLocal)
        {
            direction = Input.GetAxis("Horizontal");
            Move();
        }  
    }

    void Move()
    {  
        rb2d.velocity = new Vector2(direction * speed,0);
        Vector2 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, - GameManager.instance.ScreenBounds.x, GameManager.instance.ScreenBounds.x);
        transform.position = currentPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Apple")&& playerLocal)
        {
            int value = collision.GetComponent<Apple>().Score;
            GameManager.instance.photonView.RPC("AddScore", RpcTarget.All);
            collision.GetComponent<Apple>().photonView.RPC("DestroyApple", RpcTarget.All);
        }
    } 
}
