using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCounter : MonoBehaviourPunCallbacks
{
    Text playerCount;

    private void Start()
    {
        playerCount = GetComponent<Text>();
    }

    //ルームに入った時に呼ばれる
    public override void OnJoinedRoom()
    {
        playerCount.text = PhotonNetwork.PlayerList.Length.ToString();
    }

    // 他プレイヤーがルームへ参加した時に呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName}が参加しました");
        playerCount.text = PhotonNetwork.PlayerList.Length.ToString();
    }

    // 他プレイヤーがルームから退出した時に呼ばれるコールバック
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName}が退出しました");
        playerCount.text = PhotonNetwork.PlayerList.Length.ToString();
    }
}