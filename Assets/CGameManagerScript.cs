using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class CGameManagerScript : MonoBehaviourPunCallbacks
{
	//�N�������O�C������x�ɐ�������v���C���[Prefab
	public GameObject playerPrefab;
	void Start()
	{
		if (!PhotonNetwork.IsConnected)   //Phootn�ɐڑ�����Ă��Ȃ����
		{
			SceneManager.LoadScene("Login"); //���O�C����ʂɖ߂�
			return;
		}
		//Photon�ɐڑ����Ă���Ύ��v���C���[�𐶐�
		PhotonNetwork.IsMessageQueueRunning = true;
		GameObject Player = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
	}
	void OnGUI()
	{
		//���O�C���̏�Ԃ���ʏ�ɏo��
		GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
	}
}