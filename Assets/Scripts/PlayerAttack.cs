using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;
    public Transform atkpos;
    private Animator anim;
    private Rigidbody2D rigid;
    public float Damage = 3;
    public float damage = 3;
    

    private float meleeatktime;
    public float meleeCool = 0.5f;
    public Vector2 boxsize;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //���Ÿ�����
        damage = Damage + player.playerkillCount;
        if (Input.GetKeyDown(KeyCode.A) && anim.GetBool("IsFire") == false && !anim.GetCurrentAnimatorStateInfo(0).IsName("atk"))
        {
            anim.SetBool("IsFire", true);
            Invoke("FireArrow", 0.4f);
        }
        
        if (Input.GetKeyDown(KeyCode.Z) && anim.GetBool("IsFire") == false && !anim.GetCurrentAnimatorStateInfo(0).IsName("atk"))
        {
            if(meleeatktime <= 0)
            { 
                anim.SetTrigger("atk");
                meleeatktime = meleeCool;
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(atkpos.position, boxsize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        collider.SendMessage("TakeDamage",damage,SendMessageOptions.DontRequireReceiver);
                    }
                }

            } 
        }
        else
        {
            meleeatktime -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(atkpos.position, boxsize);
    }
    private void FireArrow()
    {
        Instantiate(bullet, pos.position, transform.rotation);
        anim.SetBool("IsFire", false);
    }
}
