using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoundMove : MonoBehaviour
{
    RunnerControl rc;
    int count = 0;

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
        if (Setting.getInstance().IsEndLessMode) 
        {
            if (other.gameObject.name == "Runner")
            {
                if (count == 1 && this.gameObject.name == "Gound1")
                {
                    CallReset();
                    count = 0;
                }
                else
                {
                    if (this.gameObject.name == "Gound1")
                    {
                        count++;
                        GameObject.Find("Gound3").transform.localPosition = new Vector3(0, 0, this.transform.localPosition.z + 100);
                    }
                    else if (this.gameObject.name == "Gound2")
                    {
                        GameObject.Find("Gound1").transform.localPosition = new Vector3(0, 0, this.transform.localPosition.z + 100);
                    }
                    else
                    {
                        GameObject.Find("Gound2").transform.localPosition = new Vector3(0, 0, this.transform.localPosition.z + 100);
                    }
                }

            }
        }
    }

    private void CallReset()
    {
        GameObject runner = GameObject.Find("Runner");
        rc = runner.GetComponent<RunnerControl>();
        rc.ResetAllEvent += Reset;
        rc.Reset();
    }

    private void Reset()
    {
        GameObject.Find("Gound1").transform.localPosition = new Vector3(0, 0, 0);
        GameObject.Find("Gound2").transform.localPosition = new Vector3(0, 0, 50);
        GameObject.Find("Gound3").transform.localPosition = new Vector3(0, 0, 100);
    }
}
