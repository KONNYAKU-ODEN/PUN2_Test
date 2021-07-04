using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class CLauncherScript : MonoBehaviourPunCallbacks
{
    #region Public�ϐ���`

    //Public�ϐ��̒�`�̓R�R��

    #endregion

    #region Private�ϐ�
    //Private�ϐ��̒�`�̓R�R��
    #endregion

    #region Public Methods
    //���O�C���{�^�����������Ƃ��Ɏ��s�����
    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {           //Photon�ɐڑ��ł��Ă��Ȃ����
            PhotonNetwork.ConnectUsingSettings();   //Photon�ɐڑ�����
            Debug.Log("Photon�ɐڑ����܂����B");
        }
    }
    #endregion

    #region Photon�R�[���o�b�N
    //���[���ɓ����O�ɌĂяo�����
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster���Ă΂�܂���");
        // "room"�Ƃ������O�̃��[���ɎQ������i���[����������΍쐬���Ă���Q������j
        PhotonNetwork.JoinOrCreateRoom("test", new RoomOptions(), TypedLobby.Default);
    }

    //���[���ɓ��������ɌĂ΂��
    public override void OnJoinedRoom()
    {
        Debug.Log("���[���ɓ���܂����B");
        //battle�V�[�������[�h
        PhotonNetwork.LoadLevel("Battle");
    }

    #endregion
}