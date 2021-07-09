using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugKey : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
        // ログアウト時に実行される
            if (PhotonNetwork.IsConnected)
            {   
                PhotonNetwork.Disconnect();   //Photonから切断する
                Debug.Log("Photonから切断しました。");
            }
            
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log(cause);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Login"); //ログイン画面に戻る
    }
}
