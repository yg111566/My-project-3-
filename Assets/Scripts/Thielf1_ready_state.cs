using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thielf1_ready_state : StateMachineBehaviour
{
    Rigidbody2D rigid;
    Transform transform;
    Thielf1 thielf;
    public float followmin;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thielf = animator.GetComponent<Thielf1>();
        transform = animator.GetComponent<Transform>();
        rigid = animator.GetComponent<Rigidbody2D>();
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (thielf.atkDelay <= 0)
        {
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            animator.SetTrigger("atk");
            thielf.atkDelay = thielf.atkCool;
        }

        if (Vector2.Distance(thielf.player.position, transform.position) > followmin&& animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            animator.SetBool("iswalk", true);
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
    }
}
