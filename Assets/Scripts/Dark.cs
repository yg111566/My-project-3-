using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dark : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Transform player;
    public Animator anim;
    public GameObject effect;
    public GameObject eatk;
    public GameObject eatk1;
    public Rigidbody2D rigid;
    public float atkCool  = 3;
    public float atkDelay;
    public float defaultHp = 1500;
    public float Hp = 1500;
    public int pattern;
    public int up = 2;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = player.position.y + up;
        transform.position = newPosition;
        if(atkDelay > 0)
            pattern = Random.Range(1,3);
        if (atkDelay >= 0)
            atkDelay -= Time.deltaTime;
        if (atkDelay <= 0)
        {
            atkDelay = atkCool;
            if(pattern == 1){
                anim.SetTrigger("atk");
                atks();
            }
            if(pattern == 2){
                anim.SetTrigger("atk1");
                atks1();
            }
        }
    }
    public void TakeDamage(float dmg)
    {
        if (Hp > 0)
        {
            Instantiate(effect, transform.position, transform.rotation);
            Hp = Hp - dmg;
        }
        if(Hp<= 0)
        {
            SceneManager.LoadScene("Ending");
        }
    }

    public void atks()
    {
        Instantiate(eatk, player.position, player.rotation);
    }
    public void atks1()
    {
        Instantiate(eatk1, player.position, player.rotation);
    }
    private void OnEnable() {
        Hp = defaultHp;
    }
}
