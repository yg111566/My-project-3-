using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSentence : MonoBehaviour
{
    public string[] talksentences;
    public string[] hitsentences;
    public string[] hitsentences1;
    public string[] hitsentences2;
    
    public Transform chatTr;
    public GameObject chatBoxPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void TalkNpc()
    {
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(talksentences,chatTr);
    }
    public void HitNpc()
    {
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(hitsentences,chatTr);
    }
    public void HitNpc1()
    {
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(hitsentences1,chatTr);
    }
    public void HitNpc2()
    {
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(hitsentences2,chatTr);
    }
}
