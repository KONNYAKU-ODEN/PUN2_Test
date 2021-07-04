using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class TitleButton : MonoBehaviourPunCallbacks
{
    #region Public変数定義

    //Public変数の定義はココで
    public Canvas[] canvas;
    /*
     * cnavas番号リスト
     * 0.Canvas_ConnectToMaster
     * 1.Canvas_JoinOrCreateRoom
     * 2.Canvas_CreateRoom
     * 
     */

    public InputField createRoomName;
    public InputField joinRoomName;
    #endregion

    #region Private変数
    //Private変数の定義はココで
    int currentCanvasNum = 0;
    #endregion

    #region Start関数
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
    // ゲーム終了ボタン
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // uintにするとUIボタンに使えないみたいだから仕方なく引数をintにしている
    // Canvasの切り替え
    // next:次のキャンパス番号
    public void SwitchCanvas(int NextCanvasNum)
    {
        canvas[currentCanvasNum].enabled = false;
        canvas[NextCanvasNum].enabled = true;
        currentCanvasNum = NextCanvasNum; // 現在のキャンバス番号の変更
    }

    //ログインボタンを押したときに実行される
    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {           //Photonに接続できていなければ
            PhotonNetwork.ConnectUsingSettings();   //Photonに接続する
            Debug.Log("Photonに接続しました。");
        }
    }
    // ログアウト時に実行される
    public void Disconnect()
    {
        if (PhotonNetwork.IsConnected)
        {           //Photonに接続できていなければ
            PhotonNetwork.Disconnect();   //Photonから切断する
            Debug.Log("Photonから切断しました。");
            SwitchCanvas(0);
        }
    }
    public void JoinRoom()
    {
        Debug.Log("参加ルーム名:" + joinRoomName.text);
        PhotonNetwork.JoinRoom(joinRoomName.text);
    }

    public void JoinRandom()
    {
        // 既に存在するランダムなルームに参加する
        PhotonNetwork.JoinRandomRoom();
    }

    public void CreateRoom()
    {
        Debug.Log("作成ルーム名:" + createRoomName.text);

        // 作成するルームのルーム設定を行う
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.CustomRoomProperties = null;
        roomOptions.CustomRoomPropertiesForLobby = new string[0];
        PhotonNetwork.CreateRoom(createRoomName.text, roomOptions, TypedLobby.Default);

        // パスワードと同じ名前のルームに参加する（ルームが存在しなければ作成してから参加する）
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

    #region Photonコールバック
    //マスターサーバー接続時に呼び出される
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMasterが呼ばれました");
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
        SwitchCanvas(1);
    }

    // ルームの作成が成功した時に呼ばれるコールバック
    public override void OnCreatedRoom()
    {
        Debug.Log("ルームの作成に成功しました");
        //GUILayout.Label("ルームの作成に成功しました");
        SwitchCanvas(4);
    }

    // ルームの作成が失敗した時に呼ばれるコールバック
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"ルームの作成に失敗しました: {message}");
        //GUILayout.Label("ルームの作成に失敗しました");
    }

    //ルームに入った時に呼ばれる
    public override void OnJoinedRoom()
    {
        Debug.Log("ルームに入りました。");
        //battleシーンをロード
        //PhotonNetwork.LoadLevel("Battle");
        SwitchCanvas(4);
    }

    // 特定の部屋への入室に失敗した時
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
        //ログインの状態を画面上に出力
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }
}