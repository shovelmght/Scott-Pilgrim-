using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private LayerMask platformLayerMask;
    [SerializeField]
    private GameObject fireBall;
    [SerializeField]
    private GameObject MainCam;
    [SerializeField]
    private GameObject CamFriend;
    [SerializeField]
    private GameObject BeginCam;
    [SerializeField]
    private GameObject bunchParticlule;
    [SerializeField]
    private float jumpForce = 10;
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private float speedJump = 50;
    [SerializeField]
    private float delayEnd = 0.5f;
    [SerializeField]
    private float delayEndCanMove = 5.5f;
    [SerializeField]
    private float delayEndFireBall = 1f;
    [SerializeField]
    private float dScale = 3f;

    private Rigidbody2D rb;
    private int nbrJump = 0;
    private bool jump = false;
    private bool jumpEvent = false;
    private bool flipFlap = true;
    private bool right = true;
    private bool die = false;
    private bool canMove = false;
    private bool downScale = false;
    private float currentDelayCanMove = 0;
    private float currentDelay;
    private float speedRef;
    private int nbrLight = 10;
    private int maxLight = 10;
    private Vector3 startPosition;
    private Vector3 startScale;

    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private InputManager input = new InputManager();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
        startScale = transform.localScale;
        speedRef = speed;
    }

    private void FixedUpdate()
    {
        if ( canMove)
        {
            if (input.WantsMoveLeft())
            {
                rb.AddForce(Vector3.left * speed);
                sprite.flipX = true;
                right = false;
            }
            else if (input.WantsMoveRight())
            {
                rb.AddForce(Vector3.right * speed);
                sprite.flipX = false;
                right = true;
            }
        }

        if (jump)
        {
            jump = false;
            rb.AddForce(Vector3.up * jumpForce);
            nbrJump++;
        }
    }
    void Update()
    {
        if (nbrLight < maxLight)
        {
            GameObject BLight = Instantiate(bunchParticlule, transform.position, Quaternion.identity);
            BLight.GetComponent<LightParticle>().SetIsPlayer(true);
            nbrLight++;
        }
        IsGrounded();

        if (input.ShootProjectil() && canMove)
        {
            if(!downScale)
            {
                if (right)
                {
                    Instantiate(fireBall, transform.position, Quaternion.Euler(0, 0, 0));
                }
                else
                {
                    Instantiate(fireBall, transform.position, Quaternion.Euler(0, 180, 0));
                }
                canMove = false;
            }
        }

        if (jumpEvent)
        {
            speed = speedJump;
            if (currentDelay >= delayEnd)
            {
                jumpEvent = false;
                speed = speedRef;
                currentDelay = 0;
            }

            currentDelay += Time.deltaTime;    // add time to timer
        }

        if (input.Jump())
        {
            if (nbrJump == 1)
            {
                rb.AddForce(Vector3.up * jumpForce);
                nbrJump = 0;
            }
            else
            {
                if (IsGrounded())
                {
                    jump = true;
                    jumpEvent = true;
                }
                else
                {
                    nbrJump = 0;
                }
            }
        }
        if(!canMove)
        {
            if (currentDelayCanMove >= delayEndCanMove)
            {
                canMove = true;
                CamFriend.SetActive(false);
                BeginCam.SetActive(false);
                currentDelayCanMove = 0;
                delayEndCanMove = delayEndFireBall;
            }

            currentDelayCanMove += Time.deltaTime;    // add time to timer
        }

        if(input.ShowObjectif())
        {
            if(flipFlap)
            {
                CamFriend.SetActive(true);
                MainCam.SetActive(false);
                flipFlap = false;
            }
            else
            {
                MainCam.SetActive(true);
                CamFriend.SetActive(false);
                flipFlap = true;
            }
        }

        if(downScale)
        {
            if(transform.localScale.x > 0.1)
            {
                transform.localScale = new Vector3(transform.localScale.x - dScale * Time.deltaTime, transform.localScale.y - dScale * Time.deltaTime, transform.localScale.z);
            }
            else
            {
                downScale = false;
            }
        }
    }

    public void RespawnPlayer()
    {
        transform.position = startPosition;
        speed = speedRef;
        die = false;
        MainCam.SetActive(true);
        BeginCam.SetActive(false);
        downScale = false;
        transform.localScale = startScale;
        sprite.color = Color.white;
    }
    public void StopPlayer()
    {
        if(!die)
        {
            nbrLight = 0;
            speed = 0;
            die = true;
            BeginCam.SetActive(true);
            MainCam.SetActive(false);
            downScale = true;
            sprite.color = Color.red;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, -Vector2.up, coll.bounds.extents.y + 0.1f, platformLayerMask);

        Color rayColor;

        if (hit.collider != null)
        {
            rayColor = Color.green;
            Debug.DrawRay(coll.bounds.center, -Vector2.up * (coll.bounds.extents.y + 0.1f), rayColor);
            return true;

        }
        else
        {
            rayColor = Color.red;
            Debug.DrawRay(coll.bounds.center, -Vector2.up * (coll.bounds.extents.y + 0.1f), rayColor);
            return false;
        }
    }

    public Vector3 GetStardPosition()
    {
        return startPosition;
    }

    public bool GetDie()
    {
        return die;
    }

    public void SetDie(bool onOff)
    {
        die = onOff;
    }
}
