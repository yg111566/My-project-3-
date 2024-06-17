using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerBoss : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Animator anim;
    public Transform player;
    public Transform pos;
    public Rigidbody2D rigid;
    public GameObject effect;
    public float defaultHp = 1000;
    public float Hp = 1000;
    public float speed;
    public Vector2 size;
    private float value;
    private bool count = true;
    PlayerController playerkill;
    public GameObject Atkrange;
    public float atkCool = 3;
    public float atkDelay;

    public float exp = 20;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerkill = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Hp <= 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            anim.SetTrigger("Die");
            if(count)
            {
                playerkill.killcount(exp);
                count = false;
            }
            Invoke("Revive", 10);
        }
        if (Hp <= 0 && spriteRenderer.color.a <= 0.4)
        if (atkDelay >= 0)
            atkDelay -= Time.deltaTime;
        if (spriteRenderer.color.a < 1)
        {
            value = spriteRenderer.color.a;
            value += Time.deltaTime;
            value = Mathf.Clamp01(value);
            ChangeTransparency(value);
        }
    }

    public void Direction(float Target, float baseobj)
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
        if (Hp > 0)
        {
            rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            ChangeTransparency(0.3f);
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
}
