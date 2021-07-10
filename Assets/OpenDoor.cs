using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    PhotonView photonView;

    Vector3 initialPos;
    bool open = false;
    bool touch = false;

    [SerializeField] Vector3 targetPos;
    [SerializeField] float speed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        initialPos = transform.position;
        //Debug.Log("initialPos" + initialPos);
    }

    // Update is called once per frame
    void Update()
    {
        if(touch && Input.GetKeyDown(KeyCode.E))
        {
            photonView.RPC(nameof(Open), RpcTarget.AllBuffered);
            Debug.Log("open" + open);
        }

        if (open && transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            //photonView.RPC(nameof(MoveDoor), RpcTarget.AllBuffered, targetPos);
        }
        else if (!open && transform.position != initialPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, speed * Time.deltaTime);
            //photonView.RPC(nameof(MoveDoor), RpcTarget.AllBuffered, initialPos);
        }
        
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            open = !open;
    //            Debug.Log("open:" + open);
    //        }
                
    //    }
    //}

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

    [PunRPC]
    private void Open()
    {
        open = !open;
    }

    //// dst‚ÉˆÚ“®‚·‚é
    //[PunRPC]
    //void MoveDoor(Vector3 dst)
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, dst, speed * Time.deltaTime);
    //}
}
