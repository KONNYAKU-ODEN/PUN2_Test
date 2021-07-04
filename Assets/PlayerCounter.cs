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

    //���[���ɓ��������ɌĂ΂��
    public override void OnJoinedRoom()
    {
        playerCount.text = PhotonNetwork.PlayerList.Length.ToString();
    }

    // ���v���C���[�����[���֎Q���������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName}���Q�����܂���");
        playerCount.text = PhotonNetwork.PlayerList.Length.ToString();
    }

    // ���v���C���[�����[������ޏo�������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName}���ޏo���܂���");
        playerCount.text = PhotonNetwork.PlayerList.Length.ToString();
    }
}