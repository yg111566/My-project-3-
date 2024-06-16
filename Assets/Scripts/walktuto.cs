using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walktuto : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool direct;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        direct = spriteRenderer.flipX;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void walk()
    {
        direct = spriteRenderer.flipX;
        direct = !direct;
        spriteRenderer.flipX = direct;
    }
}
