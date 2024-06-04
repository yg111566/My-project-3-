using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig_ready_state : StateMachineBehaviour
{
    Rigidbody2D pigrigid;
    Transform pigtransform;
    pig pig;
    public float followmin;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pig = animator.GetComponent<pig>();
        pigtransform = animator.GetComponent<Transform>();
        pigrigid = animator.GetComponent<Rigidbody2D>();
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            pigrigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (pig.atkDelay <= 0)
        {
            pigrigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            animator.SetTrigger("Attack");
            pig.atkDelay = pig.atkCool;
        }

        if (Vector2.Distance(pig.player.position, pigtransform.position) > followmin&& animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            animator.SetBool("iswalk", true);
            pigrigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pigrigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
    }


}
