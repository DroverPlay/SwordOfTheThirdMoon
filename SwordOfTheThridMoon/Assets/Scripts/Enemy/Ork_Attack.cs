using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ork_Attack : StateMachineBehaviour
{
    Transform player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player);
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance > 3)
        {
            animator.SetBool("isAttacking", false);
        }

        if (distance > 15)
        {
            animator.SetBool("isChasing", false);
        }
    }
}
