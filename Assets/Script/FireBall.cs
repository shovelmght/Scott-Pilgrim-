using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;    // speed of object
    [SerializeField]
    private float timeDestroy = 5;    // speed of object
    [SerializeField]
    private float scaleGrow = 0.5f;    // speed of object
    [SerializeField]
    private float delayEnd = 0.5f;  
    private float currentDelay;
    private PlayerController player;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeDestroy);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<PlayerController>();   // get Reference to player
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;

        if (transform.localScale.y < 2)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
     
        if (currentDelay >= delayEnd)
        {
            speed = 20;
        }

        currentDelay += Time.deltaTime;    // add time to timer
    
        if(transform.localScale.y < 5)
        {
            transform.localScale += new Vector3(scaleGrow, scaleGrow, scaleGrow) * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Ennemy"))
        {
            col.GetComponent<EnnemyController>().SetDie(true);
            col.GetComponent<EnnemyController>().SetSpeed(0);
            Destroy(col.gameObject, 2.5f) ;
        }
    }
}
