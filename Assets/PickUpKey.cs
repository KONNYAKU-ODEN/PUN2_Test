using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    public UnlockDoor unlockDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            // Create Ray
            Ray ray =
              Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            // If Ray hit something
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit);
            }
        }
    }
}
