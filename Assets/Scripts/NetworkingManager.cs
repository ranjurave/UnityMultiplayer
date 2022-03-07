using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkingManager : MonoBehaviourPunCallbacks
{
    public GameObject connecting;
    public GameObject multiPlayer;
    void Start()
    {
        Debug.Log("Connecting to server");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Joining Lobby");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        Debug.Log("Ready for Multiplayer");
        connecting.SetActive(false);
        multiPlayer.SetActive(true);
    }
}
