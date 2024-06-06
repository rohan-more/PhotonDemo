using System;
using System.Collections;
using System.Collections.Generic;
using Core.UI;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _joinButton;
    private string roomName;
    private RoomInfo _roomInfo;
    
    private void OnEnable()
    {
        _joinButton.onClick.AddListener((() =>
        {
            Events.OnJoinRoom(_roomInfo);
            Events.OnShowTab(TabName.ROOM);
        }));
    }

    public void Initialize(RoomInfo _info)
    {
        _roomInfo = _info;
        _text.text = _info.Name + " " + _info.PlayerCount + "/" + _info.MaxPlayers;
    }

}
