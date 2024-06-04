using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig_back_state : StateMachineBehaviour
{
    Rigidbody2D pigrigid;
    Transform pigtransform;
    pig pig;
    //onstateenter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        pigrigid = animator.GetComponent<Rigidbody2D>();
        pig = animator.GetComponent<pig>();
        pigtransform = animator.GetComponent<Transform>();
    }

    //onstateupdate is called on each update frame between onstateenter and onstateexit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        if(Vector2.Distance(pig.home,pigtransform.position) <0.1f || Vector2.Distance(pigtransform.position,pig.player.position)<4f)
        {
            animator.SetBool("isback", false);
        }
        else
        {
            pig.DirectionPig(pig.home.x, pigtransform.position.x);
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                pigrigid.velocity = new Vector2(animator.GetFloat("Direction") * pig.speed * -1, pigrigid.velocity.y);
        }
        pig.Hp = pig.defaultHp;
    }

    //onstateexit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {

    }


    
}
