using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger_walk : StateMachineBehaviour
{
    Rigidbody2D rigid;
    Transform Transform;
    TigerBoss Tiger;
    public float followmin;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigid = animator.GetComponent<Rigidbody2D>();
        Tiger = animator.GetComponent<TigerBoss>();
        Transform = animator.GetComponent<Transform>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(Tiger.player.position,Transform.position)>followmin)
        {
            rigid.velocity = new Vector2(animator.GetFloat("Direction") * Tiger.speed * -1, rigid.velocity.y);
        }
        else
        {
            animator.SetBool("iswalk", false);
            animator.SetTrigger("ready");
            
            rigid.velocity = Vector2.zero;
        }
        Tiger.Direction(Tiger.player.position.x, Transform.position.x);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
