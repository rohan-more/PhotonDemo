using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Core.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private PhotonManager _photonManager;

        [SerializeField] private Button _showCreateLobbybtn;
        [SerializeField] private Button _showFindRoomsbtn;
        [SerializeField] private Button _createLobbyBtn;
        [SerializeField] private Button _leaveRoomBtn;
        [SerializeField] private Button _startGameBtn;
        [SerializeField] private TMP_InputField _roomNameField;
        [SerializeField] private TMP_Text _errorText;
        [SerializeField] private TMP_Text _roomNameText;
        
        [SerializeField] private PlayerItemUI _playerItemUIPrefab;
        [SerializeField] private Transform _playerListParent;
        
        [SerializeField] private Transform _roomListParent;
        [SerializeField] private RoomItem _roomListItemPrefab;
        
        private static readonly char[] chars = 
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        private void Start()
        {
            _createLobbyBtn.onClick.AddListener(() =>
            {
                if (_roomNameField != null)
                {
                    _photonManager.CreateRoom(GenerateRandomString(6));
                }
            });
            
            _showCreateLobbybtn.onClick.AddListener(() =>
            {
                _photonManager.CreateRoom(GenerateRandomString(6));
                //Events.OnShowTab(TabName.CREATE);
            });
            
            _showFindRoomsbtn.onClick.AddListener(() => { Events.OnShowTab(TabName.FIND_ROOM); });
            
            _leaveRoomBtn.onClick.AddListener(LeaveRoom);
            
            _startGameBtn.onClick.AddListener(() =>
            {
                PhotonNetwork.LoadLevel(2);
            });
            
        }

        public void OnEnable()
        {
            Events.RoomName += UpdateRoomName;
            Events.CreateRoomFailure += UpdateRoomName;
            Events.UpdatePlayerList += UpdatePlayerList;
            Events.UpdateRoomList += OnRoomListUpdate;
            Events.PlayerEnteredRoom += OnPlayerEnteredRoom;
            Events.JoinRoom += OnJoinRoom;
            Events.MasterLeftRoom += OnMasterLeftRoom;
        }

        public void OnDisable()
        {
            Events.RoomName -= UpdateRoomName;
            Events.CreateRoomFailure -= UpdateRoomName;
            Events.UpdatePlayerList -= UpdatePlayerList;
            Events.UpdateRoomList -= OnRoomListUpdate;
            Events.PlayerEnteredRoom -= OnPlayerEnteredRoom;
            Events.JoinRoom -= OnJoinRoom;
            Events.MasterLeftRoom -= OnMasterLeftRoom;
        }
        
        public static string GenerateRandomString(int length)
        {
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[UnityEngine.Random.Range(0, chars.Length)]);
            }

            return result.ToString();
        }
        
        private void OnMasterLeftRoom()
        {
            _startGameBtn.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        }

        private void UpdateRoomName(string roomName)
        {
            _roomNameText.text = roomName;
        }
        
        private void ShowRoomCreationFailure(string errorMsg)
        {
            _errorText.text = errorMsg;
        }

        private void UpdatePlayerList()
        {
            Player[] players = PhotonNetwork.PlayerList;
        
            foreach(Transform child in _playerListParent)
            {
                Destroy(child.gameObject);
            }

            for(int i = 0; i < players.Count(); i++)
            {
                GameObject item = Instantiate(_playerItemUIPrefab.gameObject, _playerListParent);
                item.GetComponent<PlayerItemUI>().Initialize(players[i]);
                Player newPlayer = players[i];
                Debug.Log(newPlayer.NickName + " joined " + PhotonNetwork.CurrentRoom.Name + " Player ID: " + newPlayer.UserId);

                RoomManager.Instance.playerList.Add(newPlayer, newPlayer.IsMasterClient ? PlayerType.SEEKER : PlayerType.HIDER);
            }
        }
        
        private void LeaveRoom()
        {
            Debug.Log(PhotonNetwork.NickName + " has left room " + PhotonNetwork.CurrentRoom);
            PhotonNetwork.LeaveRoom();
            Events.OnShowTab(TabName.LOADING);
        }
        
        private void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach(Transform trans in _roomListParent)
            {
                Destroy(trans.gameObject);
            }

            if (roomList.Count > 0)
            {
                _showFindRoomsbtn.gameObject.SetActive(true);
            }

            foreach (var t in roomList)
            {
                if(t.RemovedFromList)
                {
                    continue;
                }
                GameObject item = Instantiate(_roomListItemPrefab.gameObject, _roomListParent);
                item.GetComponent<RoomItem>().Initialize(t);
            }
        }

        private void OnPlayerEnteredRoom(Player newPlayer)
        {
            GameObject item = Instantiate(_playerItemUIPrefab.gameObject, _playerListParent);
            item.GetComponent<PlayerItemUI>().Initialize(newPlayer);
            Debug.Log(newPlayer.NickName + " has entered room " + PhotonNetwork.CurrentRoom + " Player ID: " + newPlayer.UserId);

            RoomManager.Instance.playerList.Add(newPlayer, newPlayer.IsMasterClient ? PlayerType.SEEKER : PlayerType.HIDER);
        }
        
        private void OnJoinRoom(RoomInfo info)
        {
            PhotonNetwork.JoinRoom(info.Name);
            Events.OnShowTab(TabName.ROOM);
        }
        
        
    }
}