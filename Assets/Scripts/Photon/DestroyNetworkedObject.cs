using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DestroyNetworkedObject : MonoBehaviour
{
    
    [PunRPC]
    void RPC_DestroyProp(int targetPropID)
    {
        PhotonView photonView = PhotonView.Find(targetPropID);
        if (photonView != null)
        {
            Debug.Log("Destroying " + gameObject.name + " ID: " + photonView.ViewID);
            PhotonNetwork.Destroy(photonView.gameObject);
        }
        
    }

}
