using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class exp : MonoBehaviour
{
    PlayerController player;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = player.playerkillCount.ToString();
    }
}
