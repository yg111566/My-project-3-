using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject nextfile;
    public GameObject Startfile;

    public void getnext(){
        Time.timeScale = 1;
        nextfile.SetActive(false);
        Startfile.SetActive(false);
    }
}
