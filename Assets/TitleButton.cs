using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class TitleButton : MonoBehaviourPunCallbacks
{
    #region Public�ϐ���`

    //Public�ϐ��̒�`�̓R�R��
    public Canvas[] canvas;
    /*
     * cnavas�ԍ����X�g
     * 0.Canvas_ConnectToMaster
     * 1.Canvas_JoinOrCreateRoom
     * 2.Canvas_CreateRoom
     * 
     */

    public InputField createRoomName;
    public InputField joinRoomName;
    #endregion

    #region Private�ϐ�
    //Private�ϐ��̒�`�̓R�R��
    int currentCanvasNum = 0;
    #endregion

    #region Start�֐�
    private void Start()
    {
        Debug.Log("canvas.Length:" + canvas.Length);
        canvas[0].enabled = true;
        for(int i = 1; i < canvas.Length;++i)
        {
            canvas[i].enabled = false;
        }
    }
    #endregion

    #region Public Methods
    // �Q�[���I���{�^��
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // uint�ɂ����UI�{�^���Ɏg���Ȃ��݂���������d���Ȃ�������int�ɂ��Ă���
    // Canvas�̐؂�ւ�
    // next:���̃L�����p�X�ԍ�
    public void SwitchCanvas(int NextCanvasNum)
    {
        canvas[currentCanvasNum].enabled = false;
        canvas[NextCanvasNum].enabled = true;
        currentCanvasNum = NextCanvasNum; // ���݂̃L�����o�X�ԍ��̕ύX
    }

    //���O�C���{�^�����������Ƃ��Ɏ��s�����
    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {           //Photon�ɐڑ��ł��Ă��Ȃ����
            PhotonNetwork.ConnectUsingSettings();   //Photon�ɐڑ�����
            Debug.Log("Photon�ɐڑ����܂����B");
        }
    }
    // ���O�A�E�g���Ɏ��s�����
    public void Disconnect()
    {
        if (PhotonNetwork.IsConnected)
        {           //Photon�ɐڑ��ł��Ă��Ȃ����
            PhotonNetwork.Disconnect();   //Photon����ؒf����
            Debug.Log("Photon����ؒf���܂����B");
            SwitchCanvas(0);
        }
    }
    public void JoinRoom()
    {
        Debug.Log("�Q�����[����:" + joinRoomName.text);
        PhotonNetwork.JoinRoom(joinRoomName.text);
    }

    public void JoinRandom()
    {
        // ���ɑ��݂��郉���_���ȃ��[���ɎQ������
        PhotonNetwork.JoinRandomRoom();
    }

    public void CreateRoom()
    {
        Debug.Log("�쐬���[����:" + createRoomName.text);

        // �쐬���郋�[���̃��[���ݒ���s��
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.CustomRoomProperties = null;
        roomOptions.CustomRoomPropertiesForLobby = new string[0];
        PhotonNetwork.CreateRoom(createRoomName.text, roomOptions, TypedLobby.Default);

        // �p�X���[�h�Ɠ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���Ă���Q������j
        //PhotonNetwork.JoinOrCreateRoom(createRoomName.text, roomOptions, TypedLobby.Default);
    }

    public void QuitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("Battle");
    }

    #endregion

    #region private Methods
    #endregion

    #region Photon�R�[���o�b�N
    //�}�X�^�[�T�[�o�[�ڑ����ɌĂяo�����
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster���Ă΂�܂���");
        // "room"�Ƃ������O�̃��[���ɎQ������i���[����������΍쐬���Ă���Q������j
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
        SwitchCanvas(1);
    }

    // ���[���̍쐬�������������ɌĂ΂��R�[���o�b�N
    public override void OnCreatedRoom()
    {
        Debug.Log("���[���̍쐬�ɐ������܂���");
        //GUILayout.Label("���[���̍쐬�ɐ������܂���");
        SwitchCanvas(4);
    }

    // ���[���̍쐬�����s�������ɌĂ΂��R�[���o�b�N
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"���[���̍쐬�Ɏ��s���܂���: {message}");
        //GUILayout.Label("���[���̍쐬�Ɏ��s���܂���");
    }

    //���[���ɓ��������ɌĂ΂��
    public override void OnJoinedRoom()
    {
        Debug.Log("���[���ɓ���܂����B");
        //battle�V�[�������[�h
        //PhotonNetwork.LoadLevel("Battle");
        SwitchCanvas(4);
    }

    // ����̕����ւ̓����Ɏ��s������
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
    }
    #endregion

    void OnGUI()
    {
        //���O�C���̏�Ԃ���ʏ�ɏo��
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }
}