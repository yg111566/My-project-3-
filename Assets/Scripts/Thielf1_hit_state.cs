using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thielf1_hit_state : StateMachineBehaviour
{
    public float strength;
    public Rigidbody2D rigid;
    Thielf1 thielf;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigid = animator.GetComponent<Rigidbody2D>();
        thielf = animator.GetComponent<Thielf1>();
        rigid.velocity = Vector2.zero;
        rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        Vector2 dirvec = rigid.transform.position - thielf.player.position;
        rigid.AddForce(dirvec.normalized * strength, ForceMode2D.Impulse);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
