using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MyPlayerManager : MonoBehaviour
{
    PhotonView PV;

    GameObject controller;

    int kills;
    int deaths;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        float random_x = Random.Range(0, 4);
        Vector3 spawnpoint = new Vector3(random_x, 3.0f, 0.0f);
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), spawnpoint, Quaternion.identity);
    }
}
