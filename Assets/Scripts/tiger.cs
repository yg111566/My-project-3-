using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Animator anim;
    public Transform player;
    public Transform pos;    public Rigidbody2D rigid;
    public GameObject effect;
    public float defaultHp = 10;
    public float Hp = 10;
    public float speed;
    public Vector2 size;
    private float value;
    // Start is called before the first frame update
    void Start()
    {
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
        if (Hp > 0)
        {
            Instantiate(effect, transform.position, transform.rotation);
            Hp = Hp - 1;
        }
    }

    void ChangeTransparency(float alpha)
    {
        Color currentColor = spriteRenderer.color;
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        spriteRenderer.color = newColor;
    }
}
