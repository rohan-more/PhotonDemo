using System.Collections.Generic;
using Core.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class PhotonManager : MonoBehaviourPunCallbacks
{

    private string _errorMessage;

    public string ErrorMessage
    {
        get => _errorMessage;
        set => _errorMessage = value;
    }

    private string _roomNameText;

    void Start()
    {
        Debug.Log("Connecting....");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Events.OnShowTab(TabName.LOBBY);
        Debug.Log("Connected to a lobby!");
        int number = Random.Range(0, 100);
        PhotonNetwork.NickName = "Player " + number;
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
        Debug.Log("Connected to a room: " + roomName);
        Events.OnShowTab(TabName.LOADING);
    }
    
    public override void OnJoinedRoom()
    {
        Events.OnRoomName(PhotonNetwork.CurrentRoom.Name);
        Events.OnShowTab(TabName.ROOM);
        Events.OnUpdatePlayerList();
        Events.OnMasterLeftRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ErrorMessage = message;
        Events.OnCreateRoomFailure(PhotonNetwork.CurrentRoom.Name);
    }
    
    public override void OnLeftRoom()
    {
        Events.OnShowTab(TabName.LOBBY);
    }
    
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("Host " + newMasterClient.NickName + " has left room. ");
        Events.OnMasterLeftRoom();
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Events.OnUpdateRoomList(roomList);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " has entered room " + PhotonNetwork.CurrentRoom + " Player ID: " + newPlayer.UserId);
        Events.OnPlayerEnteredRoom(newPlayer);
    }
    
}
