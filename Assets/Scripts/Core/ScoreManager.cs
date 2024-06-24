using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public static ScoreManager Instance { get; private set; }

    private Dictionary<int, int> huntersScores = new Dictionary<int, int>();
    private Dictionary<int, int> propsScores = new Dictionary<int, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int playerId, bool isHunter, int points)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_AddScore", RpcTarget.All, playerId, isHunter, points);
        }
    }

    [PunRPC]
    void RPC_AddScore(int playerId, bool isHunter, int points)
    {
        if (isHunter)
        {
            if (!huntersScores.ContainsKey(playerId))
            {
                huntersScores[playerId] = 0;
            }
            huntersScores[playerId] += points;
        }
        else
        {
            if (!propsScores.ContainsKey(playerId))
            {
                propsScores[playerId] = 0;
            }
            propsScores[playerId] += points;
        }
    }

    public int GetScore(int playerId, bool isHunter)
    {
        if (isHunter)
        {
            return huntersScores.ContainsKey(playerId) ? huntersScores[playerId] : 0;
        }
        else
        {
            return propsScores.ContainsKey(playerId) ? propsScores[playerId] : 0;
        }
    }

    public void ResetScores()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ResetScores", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPC_ResetScores()
    {
        huntersScores.Clear();
        propsScores.Clear();
    }

    public Dictionary<int, int> GetHuntersScores()
    {
        return new Dictionary<int, int>(huntersScores);
    }

    public Dictionary<int, int> GetPropsScores()
    {
        return new Dictionary<int, int>(propsScores);
    }
}
