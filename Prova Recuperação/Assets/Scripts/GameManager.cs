using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    Vector2 screenBounds;
    float score;
    int playersInGame;
    Text scoreText;


    #region Singleton

    public static GameManager instance;
    private void Awake()
    {
        // Verifica se a instância é nula
        if (instance == null)
        {
            instance = this; // Define a instância para este objeto           
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroi o objeto se já houver uma instância existente
        }
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        screenBounds += new Vector2(1, -1);

        photonView.RPC("AddPlayer", RpcTarget.AllBuffered);

    }

    #endregion

    public Vector2 ScreenBounds { get => screenBounds; }

    
    
    // Start is called before the first frame update


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void AddScoreRPC()
    {
        photonView.RPC("AddScore", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void AddScore()
    {
        int value = 10;
        score = value;
        score++;
        scoreText.text = "Score: " + value.ToString();
    }
    private void AddPlayerRPC()
    {
       
    }

    [PunRPC]
    private void AddPlayer()
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        NetworkManager.instance.Instantiate("Prefabs/Player", new Vector2(0, -4), Quaternion.identity);
    }


}
