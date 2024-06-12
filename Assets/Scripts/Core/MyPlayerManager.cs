using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Core;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

public class MyPlayerManager : MonoBehaviour
{
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        List<Player> players = PhotonNetwork.PlayerList.ToList();
        Dictionary<Player, PlayerType>.KeyCollection playerKeys = RoomManager.Instance.playerList.Keys;
        foreach (var item in playerKeys)
        {
            if (players.Any(t => item.NickName == t.NickName))
            {
                RoomManager.Instance.playerList.TryGetValue(item, out PlayerType type);

                if (type == PlayerType.SEEKER)
                {
                    CreateSeekers();
                }
                else
                {
                    CreateHider();
                }
            }

            break;
        }

        
        /*if (PhotonNetwork.IsMasterClient)
        {
            CreateHider();
        }
        else
        { 
            CreateSeekers();
        }*/
    }

    void CreateHider()
    {
        float random_x = Random.Range(0, 10);
        Vector3 spawnpoint = new Vector3(random_x, 3.0f, 0.0f);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "TP_Player"), spawnpoint, Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "TP_Camera"), spawnpoint, Quaternion.identity);
    }

    void CreateSeekers()
    {
        float random_x = Random.Range(0, 10);
        Vector3 spawnpoint = new Vector3(random_x, 3.0f, 10.0f);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FP_Player"), spawnpoint, Quaternion.identity);
    }
}