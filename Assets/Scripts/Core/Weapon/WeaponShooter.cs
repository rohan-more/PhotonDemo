using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    [SerializeField] private float rayMaxDistance;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private PhotonView _photonView;
    private void ShootRay()
    {

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, rayMaxDistance))
        {
            if (hit.transform.CompareTag("TP_Player"))
            {
                ScoreManager.Instance.AddScore(1, true, 5);
            }
            
            if (hit.transform.CompareTag("Prop_Clone"))
            {
                int viewID = hit.transform.GetComponent<PhotonView>().ViewID;
                Debug.Log("Hit " + hit.transform.GetComponent<PhotonView>().ViewID);
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Explosion"), hit.transform.position, Quaternion.identity);
                _photonView.RPC("RPC_DestroyProp", RpcTarget.OthersBuffered, viewID);
            }
            
        }
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootRay();
        }
    }
}
