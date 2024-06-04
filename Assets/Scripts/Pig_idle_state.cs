using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Pig_idle_state : StateMachineBehaviour
{
    Transform pigTransform;
    pig pig;
    Rigidbody2D pigrigid;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pig = animator.GetComponent<pig>();
        pigTransform = animator.GetComponent<Transform>();
        pigrigid = animator.GetComponent <Rigidbody2D>();
        pigrigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(pigTransform.position, pig.player.position) <= 4)
        {
            animator.SetBool("iswalk", true);
            return;
        }
        Vector2 homvec = pigrigid.transform.position;
        if (Vector2.Distance(pig.home,homvec) > 1f)
            animator.SetBool("iswalk",true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
