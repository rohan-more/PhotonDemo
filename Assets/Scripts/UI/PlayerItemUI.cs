using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Core.UI
{
    public class PlayerItemUI : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _nameText;
        Player _player;

        public void Initialize(Player playerData)
        {
            _player = playerData;
            _nameText.text = playerData.NickName;
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (Equals(_player, otherPlayer))
            {
                Destroy(gameObject);
            }
        }

        public override void OnLeftRoom()
        {
            Destroy(gameObject);
        }
    }
}
