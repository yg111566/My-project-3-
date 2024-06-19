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
    public GameObject Biterange;
    public GameObject Punchrange;
    public float atkCool = 3;
    public float atkDelay;
    public int pattern;
    public float exp = 20;
    // Start is called before the first frame update
    void Start()
    {
        pattern = 1;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerkill = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Hp <= 0)
        {
            if(count)
            {
                playerkill.killcount(exp);
                count = false;
            }
            playerkill.killtiger = true;
            Destroy(gameObject);
        }
        if(atkDelay > 0)
            pattern = Random.Range(1,3);
        if (atkDelay >= 0)
            atkDelay -= Time.deltaTime;
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
            Instantiate(effect, transform.position, transform.rotation);
            Hp = Hp - dmg;
        }
    }

    public void punch(){
        Punchrange.SetActive(true);
    }
    public void removepunch(){
        Punchrange.SetActive(false);
    }
    
    public void bite(){
        Biterange.SetActive(true);
    }
    public void removebite(){
        Biterange.SetActive(false);
    }
    
}
