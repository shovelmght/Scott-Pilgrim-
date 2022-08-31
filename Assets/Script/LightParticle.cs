using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightParticle : MonoBehaviour
{
    private PlayerController player;
    private FriendController friend;
    private bool canMove = false;
    private float currentDelayCanMove = 0;
    public float delayEndCanMove = 2;
    private Rigidbody2D rb;
    public float speed = 10;
    public float startspeed = 150;
    private bool moveRight = false;
    private bool moveLeft = false;
    private bool moveUp = false;
    private bool moveDown = false;
    private bool canDestroy = false;
    private bool isEnnemy = false;
    private bool isPlayer = false;
    public float maxDistPlayer = 2;
    public float angDrag = 5;
    private float randX;
    private float randY;
    private float delayEnd = 2f;
    private float currentDelay;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();   // get Reference to friend
        friend = GameObject.FindObjectOfType<FriendController>();   // get Reference to player
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(40.0f, 75.0f);
        randX = Random.Range(-1.0f, 1.0f);
        randY = Random.Range(0.0f, 1.0f);
        rb.AddForce(new Vector3(randX, randY, 0) * startspeed);
        Destroy(this.gameObject, 7);
    }

    private void FixedUpdate()
    {
        if (moveRight)
        {
            rb.AddForce(Vector3.left * speed);

        }
        else if (moveLeft)
        {
            rb.AddForce(Vector3.right * speed);

        }

        if (moveUp)
        {
            rb.AddForce(Vector3.down * speed);

        }
        else if (moveDown)
        {
            rb.AddForce(Vector3.up * speed);

        }
    }

    // Update is called once per frame
    void Update()
    {

        currentDelayCanMove += Time.deltaTime;    // add time to timer
        if (!canMove)
        {
            if (currentDelayCanMove >= delayEndCanMove)
            {
                canMove = true;
                rb.gravityScale = 0;
                rb.angularDrag = angDrag;
                canDestroy = true;
            }

            currentDelayCanMove += Time.deltaTime;    // add time to timer
        }

        if (canMove)
        {
            if (isEnnemy)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    moveRight = true;
                    moveLeft = false;
                }
                else if (player.transform.position.x > transform.position.x)
                {
                    moveRight = false;
                    moveLeft = true;
                }
                else
                {
                    moveRight = false;
                    moveLeft = false;
                }

                if (player.transform.position.y < transform.position.y)
                {
                    moveUp = true;
                    moveDown = false;
                }
                else if (player.transform.position.y > transform.position.y)
                {
                    moveUp = false;
                    moveDown = true;
                }
                else
                {
                    moveUp = false;
                    moveDown = false;
                }
            }

            else if (isPlayer)
            {

                if (currentDelay >= delayEnd)
                {

                    isEnnemy = true;
                    isPlayer = false;

                }

                currentDelay += Time.deltaTime;    // add time to timer

                if (player.GetStardPosition().x + 3 < transform.position.x)
                {
                    moveRight = true;
                    moveLeft = false;
                }
                else if (player.GetStardPosition().x - 3 > transform.position.x)
                {
                    moveRight = false;
                    moveLeft = true;
                }
                else
                {
                    moveRight = false;
                    moveLeft = false;
                    isEnnemy = true;
                    isPlayer = false;
                }

                if (player.GetStardPosition().y < transform.position.y)
                {
                    moveUp = true;
                    moveDown = false;
                }
                else if (player.GetStardPosition().y > transform.position.y)
                {
                    moveUp = false;
                    moveDown = true;
                }
                else
                {
                    moveUp = false;
                    moveDown = false;
                }
            }
            else 
            {
                if (friend.GetStartPos().x  < transform.position.x)
                {
                    moveRight = true;
                    moveLeft = false;
                }
                else if (friend.GetStartPos().x > transform.position.x)
                {
                    moveRight = false;
                    moveLeft = true;
                }
                else
                {
                    moveRight = false;
                    moveLeft = false;
                    isEnnemy = true;
                    isPlayer = false;
                }

                if (friend.GetStartPos().y < transform.position.y)
                {
                    moveUp = true;
                    moveDown = false;
                }
                else if (friend.GetStartPos().y > transform.position.y)
                {
                    moveUp = false;
                    moveDown = true;
                }
                else
                {
                    moveUp = false;
                    moveDown = false;
                }
            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && canDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetIsEnnemy(bool onOff)
    {
        isEnnemy = onOff;
    }

    public void SetIsPlayer(bool onOff)
    {
        isPlayer = onOff;
    }
}
