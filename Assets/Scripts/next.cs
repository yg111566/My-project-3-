using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class next : MonoBehaviour
{
    public GameObject nextfile;
    public GameObject nowfile;

    public void getnext(){
        nextfile.SetActive(true);
        nowfile.SetActive(false);
    }
}
