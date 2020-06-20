using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAttribute : MonoBehaviour
{

    public int hp;
    public GameObject uiCanvas;


    // Start is called before the first frame update
    void Start()
    {
        hp = 10;
        uiCanvas.GetComponent<UIControl>().HPShow(hp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Injured(int damage) {
        hp -= damage;
        uiCanvas.GetComponent<UIControl>().HPShow(hp);
    }


    public void Cure(int cure)
    {
        hp += cure;
        if (hp > 10)
            hp = 10;
        uiCanvas.GetComponent<UIControl>().HPShow(hp);
    }

    internal void ReSetHp()
    {
        hp = 10;
        uiCanvas.GetComponent<UIControl>().HPShow(hp);
    }
}
