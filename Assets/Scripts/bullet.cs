using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float dummy;
    public float speed;
    public float distance;
    public float Damage;

    public float damage;
    public LayerMask isLayer;
    PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        Invoke("DestroyBullet", 2);
    }

    // Update is called once per frame
    void Update()
    {
        damage = Damage + player.playerkillCount;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if(ray.collider !=  null)
        {
            if (ray.collider.tag == "Enemy")
            {
                ray.collider.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
            DestroyBullet();
        }
        if(transform.rotation.y == 0)
            transform.Translate(transform.right * speed * Time.deltaTime * -1);
        else
            transform.Translate(transform.right * speed * Time.deltaTime);
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
