using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Create Ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            // If Ray hit something
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.CompareTag("Key"))
                {
                    GameObject Key = GameObject.Find("Door (1)");
                    PhotonView photonView = hit.collider.GetComponent<PhotonView>();
                    Key.GetComponent<UnlockDoor>().PickUpKey();

                    //photonView.RequestOwnership();

                    //if (!photonView.IsMine)
                    //{
                    //    Debug.Log("photonView.ViewID:" + PhotonNetwork.LocalPlayer);
                    //    photonView.RequestOwnership();
                    //}
                    //PhotonNetwork.Destroy(hit.collider.gameObject);
                }
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 5, false);
                Debug.Log(hit);
            }
        }
    }
}
