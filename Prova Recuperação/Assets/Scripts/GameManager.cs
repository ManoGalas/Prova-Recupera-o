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
    Text text;


    #region Singleton

    // Declara uma instância estática da classe NetworkManager
    public static GameManager instance;

    // Método chamado quando o script é inicializado
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
    }

    #endregion

    public Vector2 ScreenBounds { get => screenBounds; }

    const string playerPrefabPath = "Prefabs/Player";

    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    [PunRPC]
    void AddScore()
    {
        int value = 10;
        score = value;
        score++;
    }
    private void AddPlayerRPC()
    {
        photonView.RPC("AddPlayer", RpcTarget.AllBuffered);
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
        PlayerController player = NetworkManager.instance.Instantiate(playerPrefabPath, new Vector2(0, -4), Quaternion.identity).GetComponent<PlayerController>();
        player.photonView.RPC("Initialize", RpcTarget.All);


    }


}
