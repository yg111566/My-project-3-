using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class pig : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Animator anim;
    public Transform player;
    public Transform pos;
    public Vector2 respawnpos;
    public Rigidbody2D rigid;
    public GameObject effect;
    public float defaultHp = 10;
    public float Hp = 10;
    public float speed;
    public Vector2 home;
    public Vector2 size;
    private float value;

    public GameObject Atkrange;

    public float atkCool = 1;
    public float atkDelay;
    public float exp;
    private bool count = true;
    PlayerController playerkill;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerkill = FindObjectOfType<PlayerController>();
        respawnpos = GameObject.FindGameObjectWithTag("DieDummy").transform.position;
        home = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Hp <= 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            anim.SetTrigger("Die");
            if(count)
            {
                playerkill.killcount(exp);
                count = false;
            }
            Invoke("Revive",10);
        }
        if(Hp <= 0 && spriteRenderer.color.a <= 0.4)
            transform.position = respawnpos;
        if (atkDelay>= 0)
            atkDelay -= Time.deltaTime;
        if(spriteRenderer.color.a < 1)
        {
            value = spriteRenderer.color.a;
            value += Time.deltaTime;
            value = Mathf.Clamp01(value);
            ChangeTransparency(value);
        }
    }

    public void DirectionPig(float Target,float baseobj)
    {
        if (Target < baseobj)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            anim.SetFloat("Direction", 1);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            anim.SetFloat("Direction", -1);
        }
    }
    public void TakeDamage(float dmg)
    {
        if(Hp > 0)
        {
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            ChangeTransparency(0.3f);
            anim.SetTrigger("Hit");
            Invoke("freehit", 0.5f);
            Instantiate(effect, transform.position, transform.rotation);
            Hp = Hp - dmg;
        }
    }

    void ChangeTransparency(float alpha)
    {
        Color currentColor = spriteRenderer.color;
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        spriteRenderer.color = newColor;
    }
    public void Revive()
    {
        anim.SetTrigger("revive");
        transform.position = home;
        Hp = defaultHp;
        count = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, size);
    }
    private void freehit()
    {
        anim.SetTrigger("Afterhit");
    }
}
