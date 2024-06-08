using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thielf1_back_state : StateMachineBehaviour
{
    Rigidbody2D rigid;
    Transform transform;
    Thielf1 thielf1;
    //onstateenter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        rigid = animator.GetComponent<Rigidbody2D>();
        thielf1 = animator.GetComponent<Thielf1>();
        transform = animator.GetComponent<Transform>();
    }

    //onstateupdate is called on each update frame between onstateenter and onstateexit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        if(Vector2.Distance(thielf1.home,transform.position) <0.1f || Vector2.Distance(transform.position,thielf1.player.position)<4f)
        {
            animator.SetBool("isback", false);
        }
        else
        {
            thielf1.Direction(thielf1.home.x, transform.position.x);
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                rigid.velocity = new Vector2(animator.GetFloat("Direction") * thielf1.speed * -1, rigid.velocity.y);
        }
        thielf1.Hp = thielf1.defaultHp;
    }

    //onstateexit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {

    }


}
