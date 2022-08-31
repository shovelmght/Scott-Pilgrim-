using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    [SerializeField]
    private LayerMask platformLayerMask;
    [SerializeField]
    private GameObject fireBall;
    [SerializeField]
    private float delayEnd = 2f;
    [SerializeField]
    private float delayEndAtt = 2f;
    [SerializeField]
    private GameObject bunchParticlule;
    [SerializeField]
    private bool moveRight = true;
    private Rigidbody2D rb;
    public float jumpForce = 10;
    public float speed = 2;
    private bool attack = false;
    private bool attackDelay = false;
    public bool die = false;
    private bool diePart = false;
    private bool isAttackPlayer = false;
    private bool doOnce = true;
    private float currentDelay;
    private float currentDelayAtt;
    public PlayerController player;
    public FriendController friend;
    private int nbrLight = 0;
    public int maxLight = 10;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();   // get Reference to player
        friend = GameObject.FindObjectOfType<FriendController>();   // get Reference to player
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }


    private void FixedUpdate()
    {
        if (moveRight)
        {
            rb.AddForce(Vector3.left * speed);
            sprite.flipX = true;
        }
        else
        {
            rb.AddForce(Vector3.right * speed);
            sprite.flipX = false;
        }
    }
    void Update()
    {
        if (currentDelay >= delayEnd)
        {
            currentDelay = 0f;   // reset current time
            moveRight = !moveRight;
        }

        currentDelay += Time.deltaTime;    // add time to timer

        if(attackDelay)
        {
            if (currentDelayAtt >= delayEndAtt)
            {
                attack = false;

                if(currentDelayAtt >= delayEndAtt * 3)
                {
                    currentDelayAtt = 0f;   // reset current time
                    attackDelay = false;
                    if (isAttackPlayer)
                    {
                        player.RespawnPlayer();
                    }
                    else
                    {
                        friend.RespawnPlayer();
                    }
                }
            }

            currentDelayAtt += Time.deltaTime;    // add time to timer
        }
        if(diePart)
        {
            if (nbrLight < maxLight)
            {
                GameObject BLight = Instantiate(bunchParticlule, transform.position, Quaternion.identity);
                BLight.GetComponent<LightParticle>().SetIsEnnemy(true);
                nbrLight++;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(speed > 0)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                player.StopPlayer();
                isAttackPlayer = true;
                attack = true;
                attackDelay = true;

            }
            else if (col.gameObject.CompareTag("Friend"))
            {
                friend.StopPlayer();
                isAttackPlayer = false;
                attack = true;
                attackDelay = true;
            }
        }
    }

    public bool GetAttack()
    {
        return attack;
    }

    public bool GetDie()
    {
        return die;
    }

    public void SetDie(bool onOff)
    {
        if(doOnce || onOff == false)
        {
            doOnce = false;
            die = onOff;
            diePart = true;
            rb.gravityScale = 0;
            Destroy(coll);
        }
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
