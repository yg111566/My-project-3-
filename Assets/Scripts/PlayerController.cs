using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float playerkillCount = 0;
    public float transparency = 0.5f;
    public float JumpPower = 20;
    public float defaultmoveSpeed;
    public float moveSpeed = 7;

    public float invincibletime = 1f;
    public float maxHp = 100;
    public float minHp = 100;
    
    public float HP = 100;
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


    public Vector2 boxsize;

    public GameObject StartSet;
    public GameObject Prologue;
    public GameObject menuSet;
    public GameObject Dead;
    public GameObject effect;

    public bool killtiger;

    tiger tiger;

    public bool cansave = false;
    

    Color half = new Color(1,1,1,0.5f);
    Color full = new Color(1,1,1,1);
    
    
    bool isHurt;
    int Jumpcnt;
    bool IsDash = false;
    bool IsGround;
    private void Start()
    {
        Time.timeScale = 0;
        killtiger = false;
        defaultmoveSpeed = moveSpeed;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(regeneration());
        tiger = FindObjectOfType<tiger>();
    }

    private void Update()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position, boxsize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.CompareTag("Enemy") && Input.GetKeyDown(KeyCode.DownArrow))
                {
                    collider.SendMessage("TalkNpc",SendMessageOptions.DontRequireReceiver);
                }
            }
        maxHp = minHp + playerkillCount;
        if(HP>maxHp)
        {
            HP = maxHp;
        }
        if(HP<=0)
        {
            Dead.SetActive(true);
            Invoke("gameexit",3);    
        }
        if(cansave && Input.GetKeyDown(KeyCode.DownArrow))
        {
            Instantiate(effect, transform.position, transform.rotation);
            GameSave();
        }
        
        if(Input.GetButtonDown("Cancel"))
        {
            if(menuSet.activeSelf)
            {
                menuSet.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                menuSet.SetActive(true);
                Time.timeScale = 0;
            }
        }
        //대쉬
        if (Input.GetKeyDown(KeyCode.C) && dashtime <= 0)
        {
            IsDash = true;
        }
        if (dashtime <= defaultdashtime - 0.1)
        {
            moveSpeed = defaultmoveSpeed;
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            if (IsDash)
                dashtime = defaultdashtime;
        }
        else
        {
            moveSpeed = dashspeed;
            rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
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
        //위치 이동
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("atk") && transform.eulerAngles.y == 180)
            rigid.velocity = new Vector2(AtkDashing(), rigid.velocity.y); 
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("atk") && transform.eulerAngles.y == 0)
            rigid.velocity = new Vector2(-1 * AtkDashing(), rigid.velocity.y);
        if (hor != 0)
            rigid.velocity = new Vector2(hor * moveSpeed + (hor * AtkDashing()), rigid.velocity.y);
        
        //회전
        if (hor > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (hor < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
        
        //애니메이션 처리
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

    public void killcount(float value)
    {
        playerkillCount += value;
    }
    public void Hurt(int Damage, Vector2 pos)
    {
        if(!isHurt)
        {
            isHurt = true;
            HP = HP - Damage;
            if(HP <= 0)
            {
                StartCoroutine(HurtRoutine());
            }
            else
            {
                anim.SetTrigger("hurt");
                float x = transform.position.x - pos.x;
                if(x<0)
                    x=1;
                else
                    x=-1;
                
                StartCoroutine(KnockBack(x));
                StartCoroutine(HurtRoutine());
                StartCoroutine(alphablink());
            }
        }
    }

    IEnumerator KnockBack(float dir)
    {
        float ctime = 0;
        while(ctime<0.2f)
        {
            if(transform.rotation.y == 0)
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime*dir);
            else
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime*dir*-1f);

            ctime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator alphablink()
    {
        while(isHurt)
        {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = half;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = full;
        }
    }

    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(invincibletime);
        isHurt = false;
    }

    private void  OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemyAtk"))
        {
            Hurt(other.GetComponentInParent<enemydmgbase>().Damage,other.transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("EnemyAtk"))
        {
            Hurt(other.GetComponentInParent<enemydmgbase>().Damage,other.transform.position);
        }
        if(other.CompareTag("save"))
        {
            cansave = true;
            HP = maxHp;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("save"))
        {
            cansave = false;
        }
    }

    private float AtkDashing()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("atk"))
            return atkdash;
        return 1;
    }

    IEnumerator regeneration()
    {
        while(true){
            yield return new WaitForSeconds(5.0f);
            regener();
        }
    }
    private void regener()
    {
        if(HP < maxHp)
            HP+= (maxHp/10);
    }

    public void GameSave()
    {
        if(cansave)
        {
            SaveData save = new SaveData();
            save.HP = HP;
            save.x = transform.position.x;
            save.y = transform.position.y;
            save.exp = playerkillCount;
            save.tiger = killtiger;
            SaveManager.Save(save);
        }
    }

    public void GameLoad()
    {
        Time.timeScale = 1;
        SaveData save = SaveManager.Load();
        HP = save.HP;
        playerkillCount = save.exp;
        killtiger = save.tiger;
        transform.position = new Vector3(save.x,save.y,0);
        StartSet.SetActive(false);
    }

    public void resume(){
        Time.timeScale = 1;
    }
    public void gameexit()
    {
        Application.Quit();
    }
    public void prologue()
    {
        Prologue.SetActive(true);
    }
}
