using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thielf1_atk_state : StateMachineBehaviour
{
    Thielf1 thielf;
    Thielf1_walk_state Thielf1_walk_state;
    Rigidbody2D rigid;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thielf = animator.GetComponent<Thielf1>();
        rigid = animator.GetComponent <Rigidbody2D>();
        if(Vector2.Distance(thielf.player.position,animator.transform.position) <= 0.5f)
            animator.SetTrigger("scready");
        else
            animator.SetTrigger("else");
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thielf.atkDelay = thielf.atkCool;
        rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
    }
}

