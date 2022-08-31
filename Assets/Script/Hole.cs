using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.GetComponent<PlayerController>().SetDie(true);
            col.GetComponent<PlayerController>().RespawnPlayer();
        }
        else if (col.gameObject.CompareTag("Friend"))
        {
            col.GetComponent<FriendController>().RespawnPlayer();
        }
        else if(!col.gameObject.CompareTag("light"))
        {
            Destroy(col.gameObject);
        }
    }
}
