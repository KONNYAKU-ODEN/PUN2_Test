using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviourPunCallbacks
{
    public GameObject door;
    public int enemyHP = 20;

    //PhotonView photonView;

    private void Start()
    {
        //photonView = GetComponent<PhotonView>();
    }

    //public void DecreaseEnemyHP(int Damage)
    //{
    //    photonView.RPC(nameof(SendEnemyHP), RpcTarget.AllViaServer, Damage);
    //}
    public void DecreaseEnemyHP(int Damage)
    {
        enemyHP -= Damage;
        Debug.Log("EnemyHP:" + enemyHP);

        if(enemyHP <= 0)
        {
            door.GetComponent<EliminateToOpenDoor>().DecreaseEnemy();
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }            
        }
    }
}
