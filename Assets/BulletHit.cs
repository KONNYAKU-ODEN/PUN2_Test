using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Characters.FirstPerson;

public class BulletHit : MonoBehaviour
{
    PhotonView myPV;

    private void Start()
    {
        myPV = GetComponent<PhotonView>();
        Destroy(gameObject, 10f);
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet Hit");
        if(/*myPV.IsMine && */other.gameObject.CompareTag("Player"))
        {
            //Destroy(other);
            other.gameObject.SetActive(false);
        }
        Destroy(gameObject);
    }
}
