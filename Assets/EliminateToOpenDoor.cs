using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminateToOpenDoor : MonoBehaviour
{
    Vector3 initialPos;
    bool eliminated = false;
    //bool touch = false;

    [SerializeField] Vector3 targetPos;
    [SerializeField] float speed = 1.0f;
    [SerializeField] GameObject []enemy;

    byte enemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;

        if(enemy.Length <= 0)
        {
            eliminated = true;
        }
        else
        {
            enemyCount = (byte)enemy.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (touch && Input.GetKeyDown(KeyCode.E))
        //{
        //    eliminated = !eliminated;
        //    Debug.Log("eliminated:" + eliminated);
        //}

        //if (eliminated && transform.position != targetPos)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        //}
        //else if (!eliminated && transform.position != initialPos)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, initialPos, speed * Time.deltaTime);
        //}

        if (eliminated)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

        if (transform.position == targetPos)
        {
            Destroy(gameObject);
        }
    }

    public void DecreaseEnemy()
    {
        --enemyCount;

        if (enemyCount <= 0)
        {
            eliminated = true;
            Debug.Log("eliminated:" + eliminated);
        }
            
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        touch = true;
    //        Debug.Log("touch:" + touch);
    //    }

    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        touch = false;
    //        Debug.Log("touch:" + touch);
    //    }
    //}
}
