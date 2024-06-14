using System;
using System.Collections.Generic;
using Core;
using Core.UI;
using Photon.Realtime;
using UnityEngine;

public static class Events
{
    public static event Action<TabName> ShowTab;
    
    public static event Action<string> RoomName;
    
    public static event Action<string> CreateRoomFailure;

    public static event Action<RoomInfo> JoinRoom;
    
    public static event Action UpdatePlayerList;
    
    public static event Action MasterLeftRoom;
    
    public static event Action<List<RoomInfo>> UpdateRoomList;
    
    public static event Action<Player> PlayerEnteredRoom;
    
    public static event Action<int, string> SelectedObjectType;
    
    public static void OnShowTab(TabName obj)
    {
        ShowTab?.Invoke(obj);
    }

    public static void OnRoomName(string obj)
    {
        RoomName?.Invoke(obj);
    }

    public static void OnCreateRoomFailure(string obj)
    {
        CreateRoomFailure?.Invoke(obj);
    }

    public static void OnUpdatePlayerList()
    {
        UpdatePlayerList?.Invoke();
    }

    public static void OnUpdateRoomList(List<RoomInfo> obj)
    {
        UpdateRoomList?.Invoke(obj);
    }

    public static void OnPlayerEnteredRoom(Player obj)
    {
        PlayerEnteredRoom?.Invoke(obj);
    }

    public static void OnJoinRoom(RoomInfo obj)
    {
        JoinRoom?.Invoke(obj);
    }

    public static void OnMasterLeftRoom()
    {
        MasterLeftRoom?.Invoke();
    }

    public static void OnSelectedObjectType(int id, string obj)
    {
        SelectedObjectType?.Invoke(id, obj);
    }
}
