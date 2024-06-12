using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using Photon.Realtime;
using Random = UnityEngine.Random;

namespace Core
{
    
    public enum PlayerType {HIDER, SEEKER}
    
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        public static RoomManager Instance;
        public PhotonView _photonView;
        public Dictionary<Player, PlayerType> playerList;

        void Awake()
        {
            if(Instance)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            Instance = this;

            playerList = new Dictionary<Player, PlayerType>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if(scene.buildIndex == 1) // We're in the game scene
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
}
