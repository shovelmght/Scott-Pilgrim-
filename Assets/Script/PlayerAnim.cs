using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField]
    private float delay_Jump = 0.7f;     // Time delay for Jump
    private float currentDelayJump = 0;  // delay for Jump
    private bool doOnceJump;             // make set trigger Jump do once

    [SerializeField]
    private float delayAttack = 0.8f;     // Time delay for Jump
    private float currentDelayAttack = 0;  // delay for Jump
    private bool doOnceAttack;             // make set trigger Jump do once

    [SerializeField]
    private float delayHit = 0.8f;     // Time delay for Jump
    private float currentDelayHit = 0;  // delay for Jump
    private bool doOnceHit;             // make set trigger Jump do once

    private Animator animator;
    private PlayerController player;
    private InputManager input = new InputManager();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();   // get Reference to player
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        //Anim Jump
        if (input.Jump())
        {

            if (!doOnceJump)    // make set trigger jump do once
            {
                animator.SetTrigger("Jump");
                doOnceJump = true;   // reset doOnce with delay

            }
        }
        //Anim die
        else if (player.GetDie())
        {
            animator.SetBool("Hit", true);
            player.SetDie(false);
        }
        //Anim Hit
        else if (player.GetDie())
        {
            if (!doOnceHit)            // make set trigger die do once
            {
                doOnceHit = true;   // reset doOnce with delay
                animator.SetBool("Hit", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Run", false);

            }
        }
        //Anim Attack
        else if (input.ShootProjectil())
        {
            if (!doOnceAttack)
            {
                animator.SetTrigger("Attack");
                doOnceAttack = true;
            }

        }
        else if (input.WantsMoveRight() || input.WantsMoveLeft())
        {
            animator.SetBool("Shifting", true);
            animator.SetBool("Idle", false);
        }

        //Anim Idle
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Shifting", false);
        }


        //Delay for Jump
        if (doOnceJump)
        {
            if (currentDelayJump >= delay_Jump)
            {
                doOnceJump = false;          // reset doOnce with delay
                currentDelayJump = 0f;    // Rester Time

            }

            currentDelayJump += Time.deltaTime;  // Update time
        }

        //Delay for attack
        if (doOnceAttack)
        {
            if (currentDelayAttack >= delayAttack)
            {
                doOnceAttack = false;
                currentDelayAttack = 0f;    // Rester Time

            }

            currentDelayAttack += Time.deltaTime;  // Update time

        }

        //Delay for hit
        if (doOnceHit)
        {
            if (currentDelayHit >= delayHit)
            {
                doOnceHit = false;
                currentDelayHit = 0f;    // Rester Time

            }

            currentDelayHit += Time.deltaTime;  // Update time

        }
    }
}
