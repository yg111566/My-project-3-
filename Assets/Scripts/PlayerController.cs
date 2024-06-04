using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float transparency = 0.5f;
    public float JumpPower = 20;
    public float defaultmoveSpeed;
    public float moveSpeed = 7;
    public int additionalJumpCount = 3;
    public Transform pos;
    public float checkRadius;
    public float dashspeed = 10;
    public float atkdash = 3;
    public float dashtime = 10;
    public float defaultdashtime = 3;
    private Rigidbody2D rigid;
    public LayerMask islayer;
    private SpriteRenderer spriteRenderer;
    private Animator anim;


    int Jumpcnt;
    bool IsDash = false;
    bool IsGround;
    private void Start()
    {
        defaultmoveSpeed = moveSpeed;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //대쉬
        if (Input.GetKeyDown(KeyCode.C) && dashtime <= 0)
        {
            IsDash = true;
        }
        if (dashtime <= defaultdashtime - 0.1)
        {
            ChangeTransparency(1);
            moveSpeed = defaultmoveSpeed;
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            if (IsDash)
                dashtime = defaultdashtime;
        }
        else
        {
            moveSpeed = dashspeed;
            rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            ChangeTransparency(transparency);
        }
        dashtime -= Time.deltaTime;
        IsDash = false;
        IsGround = Physics2D.OverlapCircle(pos.position, checkRadius, islayer);
        if (IsGround == true && Input.GetKeyDown(KeyCode.Space) && Jumpcnt > 0)
        {
            rigid.velocity = Vector2.up * JumpPower;
            anim.SetBool("IsJump", true);
            anim.SetBool("IsFall", false);
        }
        if (IsGround == false && Input.GetKeyDown(KeyCode.Space) && Jumpcnt > 0)
        {
            rigid.velocity = Vector2.up * JumpPower;
            Jumpcnt--;
            anim.SetBool("IsJump", true);
            anim.SetBool("IsFall", false);
        }
        
        if(IsGround)
        {
            Jumpcnt = additionalJumpCount;
        }
    }

    private void FixedUpdate()
    {
        //키 입력
        float hor = Input.GetAxis("Horizontal");
        //해당 위치로 이동
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("atk") && transform.eulerAngles.y == 180)
            rigid.velocity = new Vector2(AtkDashing(), rigid.velocity.y); 
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("atk") && transform.eulerAngles.y == 0)
            rigid.velocity = new Vector2(-1 * AtkDashing(), rigid.velocity.y);
        if (hor != 0)
            rigid.velocity = new Vector2(hor * moveSpeed + (hor * AtkDashing()), rigid.velocity.y);
        
        //캐릭터 보는 방향 조정
        if (hor > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (hor < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
        
        //애니메이션
        if (hor != 0)
        {
            anim.SetBool("IsWalk", true);

        }
        if (hor == 0)
        {
            anim.SetBool("IsWalk", false);
        }
        if (!IsGround && rigid.velocity.y < 0)
        {
            anim.SetBool("IsFall", true);
            anim.SetBool("IsJump", false);

        }
        if (IsGround && rigid.velocity.y < 0)
        {
            anim.SetBool("IsFall", false);
            anim.SetBool("IsJump", false);
        }
        if (IsGround)
        {
            anim.SetBool("IsFall", false);
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
    }
    void ChangeTransparency(float alpha)
    {
        Color currentColor = spriteRenderer.color;
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        spriteRenderer.color = newColor;
    }
    private float AtkDashing()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("atk"))
            return atkdash;
        return 1;
    }
}
