using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using Vuforia;
using Assets;


public class GameManager : MonoBehaviourPunCallbacks

{
    [Header("Status")]
    public bool gameEnded = false;
    [Header("Players")]
    public string playerPrefabLocation;
    public Transform[] spawnPoints;
    public PlayerController[] players;
    private int playersInGame;
    private List<int> pickedSpawnIndex;
    public Button button;
    [Header("Reference")]
    public GameObject imageTarget;
    public string selectedModelName;
    private List<Model> models;

    //instance
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        pickedSpawnIndex = new List<int>();
        players = new PlayerController[PhotonNetwork.PlayerList.Length];
        photonView.RPC("ImInGame", RpcTarget.AllBuffered);
        DefaultTrackableEventHandler.isTracking = false;
        //foreach (GameObject gameObj in GameObject.FindObjectsOfType(typeof(GameObject)))
        //{
        //models.Add(new Model(gameObj.name,gameObj));
        //}
    }
    private void Update()
    {
        Debug.Log("is tracking " + DefaultTrackableEventHandler.isTracking);
        foreach (GameObject gameObj in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (gameObj.name == "Skeleton(Clone)") {

                gameObj.transform.SetParent(imageTarget.transform);
            }
        }

        for (int i = 1; i < imageTarget.transform.childCount; i++)
        {
            imageTarget.transform.GetChild(i).gameObject.SetActive(DefaultTrackableEventHandler.isTracking);
        }


    }
    [PunRPC]
    void ImInGame()
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayer();
        }
    }

    

    void SpawnPlayer()
    {
        //int rand = Random.Range(0, spawnPoints.Length);
        //while (pickedSpawnIndex.Contains(rand))
        //{
            //rand = Random.Range(0, spawnPoints.Length);
        //}
        pickedSpawnIndex.Add(0);
        //GameObject playerObject = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[rand].position, Quaternion.identity);
        //intialize the player
        if (PhotonNetwork.IsMasterClient == true)
        {
            GameObject playerObject = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[0].position, Quaternion.Euler(-90, 90, 0));
            PlayerController playerScript = playerObject.GetComponent<PlayerController>();
            playerScript.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);

        }

    }
   
    
    public PlayerController GetPlayer(int playerID)
    {
        return players.First(x => x.id == playerID);
    }
    public PlayerController GetPlayer(GameObject playerObj)
    {
        return players.First(x => x.gameObject == playerObj);
    }
}