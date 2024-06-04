using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroyeffect", 0.3f);
    }

    // Update is called once per frame
    void Destroyeffect()
    {
        Destroy(gameObject);
    }
}
