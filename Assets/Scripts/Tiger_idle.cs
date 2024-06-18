using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger_idle : StateMachineBehaviour
{
    Transform Transform;
    Rigidbody2D rigid;
    TigerBoss Tiger;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Tiger = animator.GetComponent<TigerBoss>();
        Transform = animator.GetComponent<Transform>();
        rigid = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(Transform.position, Tiger.player.position) <= 30)
        {
            animator.SetBool("iswalk", true);
            return;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}