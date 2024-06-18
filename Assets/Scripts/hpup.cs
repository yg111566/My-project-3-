using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpup : MonoBehaviour
{
    [SerializeField]
    private Slider hp;
    
    TigerBoss tiger;

    // Start is called before the first frame update
    void Start()
    {
        tiger = FindObjectOfType<TigerBoss>();
        hp.value = (float)tiger.Hp / (float)tiger.defaultHp;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHp();
    }

    private void HandleHp()
    {
        hp.value = Mathf.Lerp(hp.value,(float)tiger.Hp / (float)tiger.defaultHp,Time.deltaTime * 10);
    }
}
