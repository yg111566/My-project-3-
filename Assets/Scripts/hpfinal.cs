using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpfinal : MonoBehaviour
{
    [SerializeField]
    private Slider hp;
    
    Dark dark;

    // Start is called before the first frame update
    void Start()
    {
        dark = FindObjectOfType<Dark>();
        hp.value = (float)dark.Hp / (float)dark.defaultHp;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHp();
    }

    private void HandleHp()
    {
        hp.value = Mathf.Lerp(hp.value,(float)dark.Hp / (float)dark.defaultHp,Time.deltaTime * 10);
    }
}
