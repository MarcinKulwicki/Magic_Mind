using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using TMPro;

public class MPManager : Photon.MonoBehaviour
{
    public string GameVersion;
    public TextMeshProUGUI connectState;
    public GameObject[] DisableOnConnection;
    public GameObject[] EnableOnConnection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void FixedUpdate() {
        
        connectState.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    public void ConnectToMaster(){
        PhotonNetwork.ConnectUsingSettings(GameVersion);
    }

    public virtual void OnConnectedToMaster(){
        
        foreach(GameObject disable in DisableOnConnection){
            disable.SetActive(false);
        }
        foreach(GameObject enable in EnableOnConnection){
            enable.SetActive(true);
        }
    }

    public void CreateOrJoin(){
        PhotonNetwork.JoinRandomRoom();
    }

    public virtual void OnPhotonRandomJoinFailed(){
        RoomOptions rm = new RoomOptions{
            MaxPlayers = 4,
            isVisible = true
        };
        int rndID = Random.Range(0,3000);
        PhotonNetwork.CreateRoom("Default: "+rndID, rm, TypedLobby.Default);
    }
}
