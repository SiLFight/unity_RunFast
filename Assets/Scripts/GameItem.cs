using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
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
        if (other.gameObject.name == "Runner")
        {
            ObjectPool.getInstance().RecycleObj(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (this.gameObject.active == true)
        {
            ObjectPool.getInstance().RecycleObj(this.gameObject);
        }
    }
}
