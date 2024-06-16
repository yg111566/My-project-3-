using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig_attack_state : StateMachineBehaviour
{
    pig pig;
    Rigidbody2D pigrigid;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pig = animator.GetComponent<pig>();
        pigrigid = animator.GetComponent <Rigidbody2D>();
        pigrigid.velocity = new Vector2(animator.GetFloat("Direction") * pig.speed * -10, pigrigid.velocity.y);
        pig.Atkrange.SetActive(true);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pig.atkDelay = pig.atkCool;
        pigrigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        pig.Atkrange.SetActive(false);
    }
}
