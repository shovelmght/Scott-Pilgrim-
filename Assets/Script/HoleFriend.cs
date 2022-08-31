using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleFriend : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Friend"))
        {
            col.GetComponent<FriendController>().SetJump(true);
        }
    }
}
