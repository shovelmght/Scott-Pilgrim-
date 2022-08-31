using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour
{
    [SerializeField]
    private LayerMask platformLayerMask;
    [SerializeField]
    private GameObject fireBall;
    private Rigidbody2D rb;
    public float jumpForce = 10;
    public float speed = 150;
    public float maxDistPlayer = 15;
    private float speedRef;
    [SerializeField]
    private bool moveRight = false;
    [SerializeField]
    private bool moveLeft = false;
    private bool attack = false;
    private bool die = false;
    private bool doOnce = true;
    [SerializeField]
    private bool follow = false;
    private bool jump = false;
    private Vector3 startPosition;
    public PlayerController player;
    private int nbrLight = 10;
    public int maxLight = 10;

    [SerializeField]
    private GameObject bunchParticlule;

    private BoxCollider2D coll;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
        speedRef = speed;
    }


    private void FixedUpdate()
    {
        if(follow)
        {
            if (moveRight)
            {
                rb.AddForce(Vector3.left * speed);
                sprite.flipX = true;
            }
            else if (moveLeft)
            {
                rb.AddForce(Vector3.right * speed);
                sprite.flipX = false;
            }
        }


        if (jump)
        {
            jump = false;
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
    void Update()
    {
        if (follow)
        {
            if (!die)
            {
                if (player.transform.position.x + maxDistPlayer < transform.position.x)
                {
                    moveRight = true;
                    moveLeft = false;
                }
                else if (player.transform.position.x - maxDistPlayer > transform.position.x)
                {
                    moveRight = false;
                    moveLeft = true;
                }
                else
                {
                    moveRight = false;
                    moveLeft = false;
                }
            }
            else if (die)
            {
                moveRight = false;
                moveLeft = false;
            }
        }
        else
        {
            moveRight = false;
            moveLeft = false;
        }

        if (nbrLight < maxLight)
        {
            GameObject BLight = Instantiate(bunchParticlule, transform.position, Quaternion.identity);
            nbrLight++;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            follow = true;
        }
    }

    public void RespawnPlayer()
    {
        transform.position = startPosition;
        die = false;
        speed = speedRef;
    }

    public bool GetAttack()
    {
        return attack;
    }

    public bool GetDie()
    {
        return die;
    }

    public bool GetMoveRight()
    {
        return moveRight;
    }

    public bool GetMoveLeft()
    {
        return moveLeft;
    }

    public bool GetJump()
    {
        return jump;
    }

    public void SetDie(bool onOff)
    {
        if(doOnce || onOff == false)
        {
            doOnce = false;
            die = onOff;
        }
    }

    public void StopPlayer()
    {
        nbrLight = 0;
        speed = 0;
        die = true;
        follow = false;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetFollow(bool onOff)
    {
        follow = onOff;
    }
    public void SetJump(bool onOff)
    {
        jump = onOff;
    }

    public Vector3 GetStartPos()
    {
        return startPosition;
    }
}
