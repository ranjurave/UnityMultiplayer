using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkingManager : MonoBehaviourPunCallbacks
{
    public GameObject connecting;
    public GameObject multiPlayer;
    void Start()
    {
        Debug.Log("Connecting to server");
        PhotonNetwork.ConnectUsingSettings();
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

    public void FindMatch() {
        Debug.Log("Finding the room");
        PhotonNetwork.JoinRandomRoom(); 
    }

    // if cannot find room
    public override void OnJoinRandomFailed(short returnCode, string message) {
        MakeRoom();
    }

    private void MakeRoom() {
        int randomRoomName = UnityEngine.Random.Range(0, 5000);
        RoomOptions roomOptions = new RoomOptions() {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 6,
            PublishUserId = true
        };
        PhotonNetwork.CreateRoom("RoomName_"+randomRoomName, roomOptions);
        Debug.Log("Room Created "+ randomRoomName);
    }

    public override void OnJoinedRoom() {
        Debug.Log("Loading 2");
        //SceneManager.LoadScene(1); 
        PhotonNetwork.LoadLevel(2);
    }
}
