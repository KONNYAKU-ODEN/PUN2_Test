using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
public class CNameInputField : MonoBehaviourPunCallbacks
{
	#region Private�ϐ���`
	static string playerNamePrefKey = "PlayerName";
	InputField _inputField;
	#endregion
	#region MonoBehaviour�R�[���o�b�N
	void Start()
	{
		string defaultName = "";
		_inputField = this.GetComponent<InputField>();
		//�O��v���C�J�n���ɓ��͂������O�����[�h���ĕ\��
		if (_inputField != null)
		{
			if (PlayerPrefs.HasKey(playerNamePrefKey))
			{
				defaultName = PlayerPrefs.GetString(playerNamePrefKey);
				_inputField.text = defaultName;
			}
		}
	}
	#endregion
	#region Public Method
	public void SetPlayerName()
	{
		PhotonNetwork.NickName = _inputField.text + " ";     //����Q�[���ŗ��p����v���C���[�̖��O��ݒ�
		PlayerPrefs.SetString(playerNamePrefKey, _inputField.text);    //����̖��O���Z�[�u
		Debug.Log(PhotonNetwork.NickName);   //player�̖��O�̊m�F�B�i���삪�m�F�ł���΂��̍s�͏����Ă������j
	}
	#endregion
}