using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFriendFollow : MonoBehaviour
{
    public FriendController friend;
    public GameObject ennemy;
    private bool doonce = true;
    private SpriteRenderer prison;

    private void Start()
    {
        prison = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Projectil"))
        {

          friend.SetFollow(true);

            if(doonce)
            {
                Destroy(prison);
                Instantiate(ennemy, new Vector3(transform.position.x - 50, transform.position.y + 30, transform.position.z), Quaternion.identity);
                Instantiate(ennemy, new Vector3(transform.position.x - 70, transform.position.y + 30, transform.position.z), Quaternion.identity);
                Instantiate(ennemy, new Vector3(transform.position.x - 60, transform.position.y + 30, transform.position.z), Quaternion.identity);
            }

        }
    }
}
