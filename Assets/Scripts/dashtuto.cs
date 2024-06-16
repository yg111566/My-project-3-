using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class dashtuto : MonoBehaviour
{
    public float homex;
    
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
       homex = obj.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void movefirst()
    {
        obj.transform.position = new UnityEngine.Vector3(homex+1.5f,obj.transform.position.y,0);
    }
        public void movelast()
    {
        obj.transform.position = new UnityEngine.Vector3(homex,obj.transform.position.y,0);
    }
}
