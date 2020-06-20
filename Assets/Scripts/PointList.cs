using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointList : MonoBehaviour
{

    public GameObject[] items;

    List<Point> list = new List<Point>();

    // Start is called before the first frame update
    void Start()
    {
        ReadList();
    }

    private void ReadList()
    {



        JsonData jsonData = JsonMapper.ToObject(File.ReadAllText(Application.persistentDataPath + "/pointlist.txt"));
        if (((IDictionary)jsonData).Contains("pointList")) {
            JsonData pointList = jsonData["pointList"];
            for (int i = 0; i < pointList.Count; i++)
            {
                list.Add(new Point((int)pointList[i]["point"]));
            }

            list.Sort(new Point());

            int max = list.Count;
            if (max > 5)
            {
                max = 5;
            }

            for (int i = 0; i < max; i++)
            {
                Text[] child = items[i].GetComponentsInChildren<Text>();
                for (int j = 0; j < child.Length; j++)
                {
                    if (child[j].name == "Ranking")
                    {
                        child[j].text = (i + 1).ToString();
                    }
                    if (child[j].name == "Point")
                    {
                        child[j].text = list[i].point.ToString();
                    }

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
