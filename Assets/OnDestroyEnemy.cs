using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyEnemy : MonoBehaviour
{
    public GameObject door;

    private void OnDestroy()
    {
        door.GetComponent<EliminateToOpenDoor>().DecreaseEnemy();
    }
}
