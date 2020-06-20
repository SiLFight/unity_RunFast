using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRunnerAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Animator>().Play("RunForward1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
