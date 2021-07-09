using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    Vector3 initialPos;
    bool open = false;
    bool pick = false;

    [SerializeField] Vector3 targetPos;
    [SerializeField] float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pick && Input.GetKeyDown(KeyCode.E))
        {
            open = !open;
            Debug.Log("open" + open);
        }

        if (open && transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else if (!open && transform.position != initialPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, speed * Time.deltaTime);
        }

    }

    public void PickUpKey()
    {
        pick = true;
    }
}
