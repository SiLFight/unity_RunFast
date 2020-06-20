using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject endPoint;

    float itemCountDown;
    int whichMakeGameItem;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;

        if (Setting.getInstance().IsEndLessMode)
        {
            itemCountDown = 0;
            whichMakeGameItem = 0;
            endPoint.SetActive(false);
            EndLessGame();
        }
        else 
        {
            endPoint.SetActive(true);
            NextLevel();
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        //无尽模式每隔20秒生成一个道具
        if (Setting.getInstance().IsEndLessMode) 
        {
            itemCountDown += Time.deltaTime;
            if (itemCountDown > 20)
            {
                itemCountDown = 0;
                whichMakeGameItem++;

                switch (whichMakeGameItem) {
                    case 1:
                        GameObject.Find("BAndM1").GetComponent<BAndMControl>().makeGameItem();
                        break;
                    case 2:
                        GameObject.Find("BAndM2").GetComponent<BAndMControl>().makeGameItem();
                        break;
                    case 3:
                        GameObject.Find("BAndM3").GetComponent<BAndMControl>().makeGameItem();
                        whichMakeGameItem = 0;
                        break;
                }
            }
        }
        
    }

    public void GetDiamondo()
    {
        GameObject.Find("UICanvas").GetComponent<UIControl>().GetDiamondo();
    }

    public void ReStartGame() {

        if (Setting.getInstance().IsEndLessMode)
        {
            itemCountDown = 0;
            whichMakeGameItem = 0;
            endPoint.SetActive(false);
            EndLessGame();
        }
        else
        {
            endPoint.SetActive(true);
            NextLevel();
        }
    }

    private void EndLessGame() {
        GameObject.Find("Gound1").transform.localPosition = new Vector3(0, 0, 0);
        GameObject.Find("Gound2").transform.localPosition = new Vector3(0, 0, 50);
        GameObject.Find("Gound3").transform.localPosition = new Vector3(0, 0, 100);
        
        GameObject[] BAndMs = GameObject.FindGameObjectsWithTag("BAndM");
        for (int i = 0; i < BAndMs.Length; i++)
        {
            ObjectPool.getInstance().RecycleObj(BAndMs[i]);
        }

        GameObject.Find("UICanvas").GetComponent<UIControl>().StartGame();

        GameObject runner = GameObject.Find("Runner");
        runner.GetComponent<RunnerAttribute>().ReSetHp();
        runner.GetComponent<RunnerControl>().ReStrat();
        Time.timeScale = 1;
    }

    internal void NextLevel()
    {
        GameObject[] BAndMs = GameObject.FindGameObjectsWithTag("BAndM");
        for (int i = 0; i < BAndMs.Length; i++)
        {
            if (BAndMs[i].active)
                ObjectPool.getInstance().RecycleObj(BAndMs[i]);
        }

        GameObject.Find("UICanvas").GetComponent<UIControl>().StartGame();
        
        

        GameObject runner = GameObject.Find("Runner");
        runner.GetComponent<RunnerAttribute>().ReSetHp();
        runner.GetComponent<RunnerControl>().ReStrat();

        GameObject.Find("BAndM1").GetComponent<BAndMControl>().makeBAndMByTable(1);//传入下一关的关数 暂时定死为0
        Time.timeScale = 1;
    }
}
