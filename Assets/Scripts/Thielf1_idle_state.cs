using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thielf1_idle_state : StateMachineBehaviour
{
    Transform Transform;
    Rigidbody2D rigid;
    Thielf1 thielf;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thielf = animator.GetComponent<Thielf1>();
        Transform = animator.GetComponent<Transform>();
        rigid = animator.GetComponent<Rigidbody2D>();
        rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(Transform.position, thielf.player.position) <= 4)
        {
            animator.SetBool("iswalk", true);
            return;
        }
        Vector2 homvec = rigid.transform.position;
        if (Vector2.Distance(thielf.home, homvec) > 1f)
            animator.SetBool("iswalk", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}