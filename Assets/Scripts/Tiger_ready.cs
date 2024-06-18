using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger_ready : StateMachineBehaviour
{
    Rigidbody2D rigid;
    Transform transform;
    TigerBoss Tiger;
    public float followmin;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Tiger = animator.GetComponent<TigerBoss>();
        transform = animator.GetComponent<Transform>();
        rigid = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Tiger.atkDelay <= 0 && Tiger.pattern == 1)
        {
            animator.SetTrigger("bite");
            Tiger.atkDelay = Tiger.atkCool;
        }
        
        else if (Tiger.atkDelay <= 0 && Tiger.pattern == 2)
        {
            animator.SetTrigger("punch");
            Tiger.atkDelay = Tiger.atkCool;
        }

        else if(Vector2.Distance(Tiger.player.position, transform.position) > followmin)
        {
            animator.SetBool("iswalk", true);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
