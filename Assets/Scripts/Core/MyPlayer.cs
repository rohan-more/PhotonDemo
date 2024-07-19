using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Photon.Realtime;
public class MyPlayer : Player
{

    private string _nickName;
    private int _actorNumber;
    private bool _isLocal;
    private PlayerType _playerType;

    protected internal MyPlayer(string nickName, int actorNumber, bool isLocal) : base(nickName, actorNumber, isLocal)
    {
        
    }

    public PlayerType Type
    {
        get => _playerType;
        set => _playerType = value;
    }
}
