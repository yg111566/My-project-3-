using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig_Die_state : StateMachineBehaviour
{
    private float value = 1;
    private SpriteRenderer spriteRenderer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ChangeTransparency(value);
        value -= Time.deltaTime * 3;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
    void ChangeTransparency(float alpha)
    {
        Color currentColor = spriteRenderer.color;
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        spriteRenderer.color = newColor;
    }
}
