using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public Collider2D[] collid;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroyeffect",1.05f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Destroyeffect()
    {
        Destroy(gameObject);
    }
    public void makescollid(){
        foreach(Collider2D collided in collid)
        {
            collided.enabled = true;
        }
    }
}

