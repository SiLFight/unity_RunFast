using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{

    public List<Image> heartList;
    public Text pointText;
    public Text pointTextValue;
    public Text countDown;
    public GameObject gameOverText;
    public GameObject reStart;
    public GameObject nextLevel;
    public GameObject backMain;
    public GameObject gameControl;
    public GameObject jiesuan;
    public GameObject jiesuanIcon;
    GameObject runner;
    public int getPoint;

    float raceTime;

    bool isLive;
    

    // Start is called before the first frame update
    void Start()
    {
        getPoint = 0;
        isLive = false;
        countDown.enabled = false;
        runner = GameObject.Find("Runner");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GetDiamondo() {
        getPoint += 5000;
    }

    IEnumerator AddPoint() {
        yield return new WaitForSeconds(3);
        while (isLive) {
            float speed = runner.GetComponent<RunnerControl>().speed_Run;
            if (speed < 0) {
                speed = 0;
            }
            getPoint += (int)(0.2* speed);
            pointText.text = "当前分数：";
            pointTextValue.text = getPoint.ToString();
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void HPShow(int hp) {
        for (int i = 0; i < heartList.Count; i++)
        {
            heartList[i].enabled = false;
        }
        for (int i = 0; i < hp; i++) {
            heartList[i].enabled = true;
        }
        if (hp <= 0) {
            GameOver();
        }
    }

    private void GameOver()
    {
        isLive = false;

        UpDataPointList();

        Time.timeScale = 0;
        gameOverText.SetActive(true);
        gameOverText.GetComponent<Text>().text = "Game Over";
        reStart.SetActive(true);
        backMain.SetActive(true);
        jiesuan.SetActive(true);
        jiesuanIcon.SetActive(false);
    }

    public void GameClear() {
        Time.timeScale = 0;
        gameOverText.SetActive(true);
        gameOverText.GetComponent<Text>().text = "恭喜过关";
        backMain.SetActive(true);
        nextLevel.SetActive(true);
        jiesuan.SetActive(true);
        jiesuanIcon.SetActive(true);
    }

    private void UpDataPointList()
    {


        List<Point> list = new List<Point>();
        //拿出数据
        JsonData jsonData = JsonMapper.ToObject(File.ReadAllText(Application.persistentDataPath + "/pointlist.txt"));
        if (((IDictionary)jsonData).Contains("pointList"))
        {
            JsonData pointList = jsonData["pointList"];
            for (int i = 0; i < pointList.Count; i++)
            {
                list.Add(new Point((int)pointList[i]["point"]));
            }
        }
        
        //插入新数据并排序
        list.Add(new Point(getPoint));
        list.Sort(new Point());

        string json = "{\"pointList\":" + JsonMapper.ToJson(list) + "}";

        print("json=" + json);

        File.WriteAllText(Application.persistentDataPath + "/pointlist.txt", json);
    }

    public void ReStatr() {
        gameOverText.SetActive(false);
        reStart.SetActive(false);
        backMain.SetActive(false);
        gameControl.GetComponent<GameControl>().ReStartGame();
    }

    public void StartGame() {
        StopCoroutine("CountRaceTime");
        StopCoroutine("AddPoint");
        StartCoroutine("CountDown");
        if (Setting.getInstance().IsEndLessMode)
        {
            pointText.text = "当前分数：";
            pointTextValue.text = "0";
            ReSetPonit();
        }
        else
        {
            isLive = true;
            raceTime = 20;
            pointText.text = "剩余时间：";
            pointTextValue.text = ((int)raceTime).ToString();
            StartCoroutine("CountRaceTime");
        }
    }

    public void NextLevel() {
        gameOverText.SetActive(false);
        nextLevel.SetActive(false);
        backMain.SetActive(false);
        gameControl.GetComponent<GameControl>().NextLevel();
    }

    IEnumerator CountRaceTime()
    {
        yield return new WaitForSeconds(3);
        while (isLive)
        {
            raceTime -= Time.deltaTime;
            if (raceTime < 0)
                raceTime = 0;
            pointText.text = "剩余时间：";
            pointTextValue.text = ((int)raceTime).ToString();
            if (raceTime == 0) 
            {
                GameOver();
                break;
            }
                
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator CountDown() {
        countDown.enabled = true;
        countDown.text = "3";
        yield return new WaitForSeconds(1);
        countDown.text = "2";
        yield return new WaitForSeconds(1);
        countDown.text = "1";
        yield return new WaitForSeconds(1);
        countDown.enabled = false;
    }

    public void ReSetPonit() {
        getPoint = 0;
        isLive = true;
        StartCoroutine("AddPoint");
    }

    public void BackToMain() {
        ObjectPool.getInstance().ReStatr();
        SceneManager.LoadScene("MainMenu");
    }
}
