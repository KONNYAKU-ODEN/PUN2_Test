using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    PhotonView photonView;
    Vector3 initialPos;
    bool open = false;
    bool touch = false;
    bool pick = false;

    [SerializeField] Vector3 targetPos;
    [SerializeField] float speed = 1.0f;

    public GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(pick)
        {
            if (touch && Input.GetKeyDown(KeyCode.E))
            {
                photonView.RPC(nameof(OpenDoor), RpcTarget.AllBuffered);
                Debug.Log("open" + open);
            }

            if (open && transform.position != targetPos)
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            else if (!open && transform.position != initialPos)
                transform.position = Vector3.MoveTowards(transform.position, initialPos, speed * Time.deltaTime);
        }
    }

    public void PickUpKey()
    {
        photonView.RPC(nameof(SwitchPick), RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void SwitchPick()
    {
        pick = true;
        Debug.Log("Pick:" + pick);
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(key);
        }
    }

    [PunRPC]
    void OpenDoor()
    {
        open = !open;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touch = true;
            Debug.Log("touch:" + touch);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touch = false;
            Debug.Log("touch:" + touch);
        }
    }

    //// dst‚ÉˆÚ“®‚·‚é
    //[PunRPC]
    //void MoveDoor(Vector3 dst)
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, dst, speed * Time.deltaTime);
    //}
}
