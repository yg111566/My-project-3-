using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig_follow_state : StateMachineBehaviour
{
    Rigidbody2D pigrigid;
    Transform pigTransform;
    pig pig;
    public float followmax;
    public float followmin;
    public float leftmax = 4f;
    public float lefttime = 0f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pigrigid = animator.GetComponent<Rigidbody2D>();
        pig = animator.GetComponent<pig>();
        pigTransform = animator.GetComponent<Transform>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(pig.player.position, pigTransform.position) > followmax)
        {
            if(leftmax < lefttime)
            {
                animator.SetBool("iswalk", false);
                animator.SetBool("isback", true);
            }
            lefttime += Time.deltaTime;
            pigrigid.velocity = Vector2.zero;

        }
        else if (Vector2.Distance(pig.player.position,pigTransform.position)>followmin && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            lefttime = 0;
            pigrigid.velocity = new Vector2(animator.GetFloat("Direction") * pig.speed * -1, pigrigid.velocity.y);
        }
        else
        {
            animator.SetBool("iswalk", false);
            animator.SetBool("isback", false);
            pigrigid.velocity = Vector2.zero;
        }
        pig.DirectionPig(pig.player.position.x, pigTransform.position.x);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pig.atkDelay = pig.atkCool;
    }
}

    
