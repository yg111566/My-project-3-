using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class END : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable() 
    {
        Invoke("Endgame",3);
    }

    public void Endgame()
    {
        Application.Quit();
    }
}
