using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class BAndMControl : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        /*
        //计时模式下，配置表刷怪
        if (!Setting.getInstance().IsEndLessMode) {
            //只需要生成一次
            if(this.gameObject.name == "BAndM1")
                makeBAndMByTable(1);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Setting.getInstance().IsEndLessMode) 
        {
            print(this.gameObject.name);
            if (other.gameObject.name == "Runner")
            {
                if (this.gameObject.name == "BAndM1")
                {
                    //一开始会触发一次
                    makeBAndMByRandom(GameObject.Find("BAndM2").transform);
                }
                else if (this.gameObject.name == "BAndM2")
                {
                    makeBAndMByRandom(GameObject.Find("BAndM3").transform);
                }
                else if (this.gameObject.name == "BAndM3")
                {
                    makeBAndMByRandom(GameObject.Find("BAndM1").transform);
                }
            }
        }
    }

    /// <summary>
    /// 生成一个道具
    /// </summary>
    /// <param name="parent"></param>
    public void makeGameItem() {
        GameObject gameItem = null;
        int r = Random.Range(1, 4);
        switch (r)
        {
            case 1:
                gameItem = ObjectPool.getInstance().GetObj("Star");
                break;
            case 2:
                gameItem = ObjectPool.getInstance().GetObj("Diamondo");
                break;
            case 3:
                gameItem = ObjectPool.getInstance().GetObj("Heart");
                break;
            case 4:
                gameItem = ObjectPool.getInstance().GetObj("Diamondo");
                break;
        }
        gameItem.transform.parent = this.transform;

        float x_Random = Random.Range(-1.5f, 1.5f);
        float z_Random = Random.Range(-24, 24f);

        gameItem.transform.localPosition = new Vector3(x_Random, 2.5f, z_Random);
    }

    private void makeBAndMByRandom(Transform parent)
    {
        GameObject bOrM = null;
        for (int i = 0; i < 10; i++) {
            int r = Random.Range(1, 6);
            switch (r) {
                case 1:
                    bOrM = ObjectPool.getInstance().GetObj("Barrier1");
                    break;
                case 2:
                    bOrM = ObjectPool.getInstance().GetObj("Barrier2");
                    break;
                case 3:
                    bOrM = ObjectPool.getInstance().GetObj("Barrier3");
                    break;
                case 4:
                    bOrM = ObjectPool.getInstance().GetObj("Monster1");
                    break;
                case 5:
                    bOrM = ObjectPool.getInstance().GetObj("Monster2");
                    break;
                case 6:
                    bOrM = ObjectPool.getInstance().GetObj("Monster1");
                    break;
            }
            bOrM.transform.parent = parent;

            float x_Random = Random.Range(-1.5f,1.5f);

            bOrM.transform.localPosition = new Vector3(x_Random, 2, -25+ i*5f);
        }
    }

    public void makeBAndMByTable(int numlevel)
    {
        List<BAndM> bAndMs = new List<BAndM>();
        JsonData jsonData = JsonMapper.ToObject(File.ReadAllText(Application.persistentDataPath + "/bandm.txt"));
        JsonData level = jsonData["level"];
        if (level.Count > 0 && numlevel-1 < level.Count)
        {
            JsonData levelbAndMs = level[numlevel - 1]["bAndMs"];
            for (int i = 0; i < levelbAndMs.Count; i++)
            {
                
                bAndMs.Add(new BAndM(levelbAndMs[i]["name"].ToString(), float.Parse(levelbAndMs[i]["x"].ToString()), float.Parse(levelbAndMs[i]["z"].ToString())));
            }
        }

        foreach (BAndM bAndM in bAndMs) {
            GameObject bOrM = null;
            if (bAndM.name == "Barrier1") 
            {
                bOrM = ObjectPool.getInstance().GetObj("Barrier1");
            } 
            else if (bAndM.name == "Barrier2") 
            {
                bOrM = ObjectPool.getInstance().GetObj("Barrier2");
            }
            else if (bAndM.name == "Barrier3")
            {
                bOrM = ObjectPool.getInstance().GetObj("Barrier3");
            }
            else if (bAndM.name == "Monster1")
            {
                bOrM = ObjectPool.getInstance().GetObj("Monster1");
            }
            else if (bAndM.name == "Monster2")
            {
                bOrM = ObjectPool.getInstance().GetObj("Monster2");
            }
            bOrM.transform.parent = GameObject.Find("BAndM1").transform;
            bOrM.transform.localPosition = bAndM.pos;
        }
    }
}
