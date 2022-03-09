using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public static RoomManager Instance;
    private void Awake() {
        if (Instance) {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Vector3 spawnPosition = new Vector3(Random.Range(-3,3),0, Random.Range(-3, 3));
        if (PhotonNetwork.InRoom) {
            //Photon instantiate can look only in resources folder
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity); 
            Debug.Log("existing scene load");
        }
        else {
            //if not online
            Debug.Log("new scene load");
            Instantiate(Resources.Load("ShooterPlayer"), spawnPosition, Quaternion.identity);
        }
    }
}
