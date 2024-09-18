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
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
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
    void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
    

    [PunRPC]
    void AddPlayer()
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            CreatePlayer();
        }
    }

    void CreatePlayer()
    {
        NetworkManager.instance.Instantiate("Prefabs/Player", new Vector2(0, -4), Quaternion.identity);
    }

    

}
