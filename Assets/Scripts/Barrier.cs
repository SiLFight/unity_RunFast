using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "runner") 
        {
            RunnerInjured();
            ObjectPool.getInstance().RecycleObj(this.gameObject);
        }
    }
    private void SlowDown(float speed)
    {
        GameObject.Find("Runner").GetComponent<RunnerControl>().SlowDown(speed);
    }

    private void RunnerInjured()
    {
        GameObject.Find("Runner").GetComponent<RunnerAttribute>().Injured(1);
    }

    private void OnBecameInvisible()
    {
        if (this.gameObject.active == true) {
            ObjectPool.getInstance().RecycleObj(this.gameObject);
        }
    }
}
