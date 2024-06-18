using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Animator anim;
    public Transform player;
    public Transform pos;    
    public Rigidbody2D rigid;
    public GameObject effect;
    public GameObject TigerBoss;
    public float defaultHp = 1000;
    public float Hp = 10;
    public float speed;
    public Vector2 size;
    private float value;


    NpcSentence chat;
    // Start is called before the first frame update
    void Start()
    {
        chat = GetComponent<NpcSentence>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }
    public void TakeDamage(float dmg)
    {
        if (Hp > 3)
        {
            Instantiate(effect, transform.position, transform.rotation);
            chat.HitNpc();
            Hp = Hp - 1;
        }
        else if (Hp <= 3 && Hp>1)
        {
            Instantiate(effect, transform.position, transform.rotation);
            chat.HitNpc1();
            Hp = Hp - 1;
        }
        else if (Hp == 1)
        {
            Instantiate(effect, transform.position, transform.rotation);
            chat.HitNpc2();
            Hp = Hp - 1;
        }
        
        else if(Hp <= 0)
        {
            Instantiate(TigerBoss, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void ChangeTransparency(float alpha)
    {
        Color currentColor = spriteRenderer.color;
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        spriteRenderer.color = newColor;
    }
}
