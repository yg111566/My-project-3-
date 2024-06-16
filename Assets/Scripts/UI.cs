
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;

    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        hpbar.value = (float)player.HP / (float)player.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHp();
    }

    private void HandleHp()
    {
        hpbar.value = Mathf.Lerp(hpbar.value,(float)player.HP / (float)player.maxHp,Time.deltaTime * 10);
    }
}
