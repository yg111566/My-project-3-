using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thielf1_walk_state : StateMachineBehaviour
{
 Rigidbody2D rigid;
    Transform Transform;
    Thielf1 thielf;
    public float followmax;
    public float followmin;
    public float leftmax = 4f;
    public float lefttime = 0f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigid = animator.GetComponent<Rigidbody2D>();
        thielf = animator.GetComponent<Thielf1>();
        Transform = animator.GetComponent<Transform>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(thielf.player.position, Transform.position) > followmax)
        {
            if(leftmax < lefttime)
            {
                animator.SetBool("iswalk", false);
                animator.SetBool("isback", true);
            }
            lefttime += Time.deltaTime;
            rigid.velocity = Vector2.zero;

        }
        else if (Vector2.Distance(thielf.player.position,Transform.position)>followmin && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            lefttime = 0;
            rigid.velocity = new Vector2(animator.GetFloat("Direction") * thielf.speed * -1, rigid.velocity.y);
        }
        else
        {
            animator.SetBool("iswalk", false);
            animator.SetBool("isback", false);
            
            rigid.velocity = Vector2.zero;
        }
        thielf.Direction(thielf.player.position.x, Transform.position.x);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
