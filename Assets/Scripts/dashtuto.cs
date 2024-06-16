using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashtuto : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void movefirst()
    {
        obj.transform.Translate(Vector2.left*1.5f);
    }
        public void movelast()
    {
        obj.transform.Translate(Vector2.left*-1.5f);
    }
}
