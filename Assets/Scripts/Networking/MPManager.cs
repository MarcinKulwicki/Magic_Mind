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
    public GameObject[] DisableOnJoinRoom;
    public GameObject[] EnableOnConnection;
    public GameObject[] EnableOnJoinRoom;
    public GameObject[] DisableOnRoomFull;
    public string username;
    public int currentPlayer = 0;
    public bool gameStart = false;
    public TextMeshProUGUI timerText;
    private float _timer = 3;
    private List<GameObject> spawnPoints = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            if(child.tag == "SpawnPoint")
                spawnPoints.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current Player: "+currentPlayer);
        // currentPlayer = PhotonNetwork.playerList.Length;
        if(!gameStart){

            if(currentPlayer == 2){
                timerText.gameObject.SetActive(true);
                foreach (GameObject disable in DisableOnRoomFull){
                    disable.SetActive(false);
                }
                _timer -= Time.deltaTime;
                timerText.text = "Starts in: "+ Mathf.Round(_timer);
                if(_timer < 0){
                    gameStart = true;
                    timerText.gameObject.SetActive(false);
                }
            }
        }
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
            MaxPlayers = 2,
            isVisible = true
        };
        int rndID = Random.Range(0,3000);
        PhotonNetwork.CreateRoom("Default: "+rndID, rm, TypedLobby.Default);
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

        if(stream.isWriting){
            
            stream.SendNext(currentPlayer);
        }else if(stream.isReading){

           currentPlayer = (int) stream.ReceiveNext();
        }
    }

    public virtual void OnJoinedRoom(){
        photonView.RPC("AddPlayerCount", PhotonTargets.All);
        foreach(GameObject disable in DisableOnJoinRoom){
            disable.SetActive(false);
        }
        foreach(GameObject enable in EnableOnJoinRoom){
            enable.SetActive(true);
        }
        Vector3 pos = spawnPoints[Random.Range(0,spawnPoints.Count)].transform.position;
        GameObject player = PhotonNetwork.Instantiate("Player", pos, Quaternion.identity, 0);
        player.GetComponent<Player>().username = username;
        player.GetComponent<Player>().mp = this;
    }

    [PunRPC]
    void AddPlayerCount(){
        currentPlayer++;
    }
}
