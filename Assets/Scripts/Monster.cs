using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Animator anim;
    float speed_XAaix;
    int baseNumber;
    public GameObject father;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator XAsixMove() {
        while (true) {
            father.transform.localPosition = new Vector3(father.transform.localPosition.x + speed_XAaix * baseNumber,
                father.transform.localPosition.y,
                father.transform.localPosition.z);
            if (father.transform.localPosition.x > 1.45f)
            {
                baseNumber = -1;
                father.transform.localRotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
            else if (father.transform.localPosition.x < -1.45f) 
            {
                baseNumber = 1;
                father.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnEnable()
    {
        anim = father.GetComponent<Animator>();
        if (this.gameObject.name == "Slime")
        {
            anim.Play("WalkFWD1");
            speed_XAaix = 0.015f;
        }
        else
        {
            anim.Play("RunFWD1");
            speed_XAaix = 0.045f;
        }

        if (father.transform.localPosition.x > 0)
        {
            baseNumber = -1;
            father.transform.localRotation = Quaternion.Euler(new Vector3(0, 270, 0));
        }
        else
        {
            baseNumber = 1;
            father.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }

        StartCoroutine("XAsixMove");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "runner")
        {
            RunnerInjured();
            ObjectPool.getInstance().RecycleObj(this.transform.parent.gameObject);
        }
    }

    private void SlowDown(float speed)
    {
        GameObject.Find("Runner").GetComponent<RunnerControl>().SlowDown(speed);
    }

    private void RunnerInjured()
    {
        if (this.gameObject.name == "Slime")
        {
            //红色怪物扣2血
            GameObject.Find("Runner").GetComponent<RunnerAttribute>().Injured(2);
        }
        else 
        {
            //蓝色怪物扣3血
            GameObject.Find("Runner").GetComponent<RunnerAttribute>().Injured(3);
        }
        
    }

    private void OnBecameInvisible()
    {
        if (this.transform.parent.gameObject.active == true) {
            ObjectPool.getInstance().RecycleObj(this.transform.parent.gameObject);
        }
        
    }
}
