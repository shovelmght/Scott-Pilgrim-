using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnnemyAnim : MonoBehaviour
{

    [SerializeField]
    private float delayAttack = 0.8f;     // Time delay for Jump
    private float currentDelayAttack = 0;  // delay for Jump
    private bool doOnceAttack;             // make set trigger Jump do once

    [SerializeField]
    private float delayHit = 0.8f;     // Time delay for Jump
    private float currentDelayHit = 0;  // delay for Jump
    private bool doOnceHit;             // make set trigger Jump do once

    private Animator animator;
    private EnnemyController player;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<EnnemyController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
         if (player.GetDie())
        {
            animator.SetBool("Hit", true);
            player.SetDie(false);
         }

        if (player.GetAttack())
        {
            if (!doOnceAttack)
            {
                animator.SetTrigger("Attack");
                doOnceAttack = true;
            }
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
