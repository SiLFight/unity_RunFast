using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CheckFile();
    }

    private void CheckFile()
    {
        if (!File.Exists(Application.persistentDataPath + "/bandm.txt"))
            File.Copy(Application.streamingAssetsPath + "/bandm.txt", Application.persistentDataPath + "/bandm.txt");
        if (!File.Exists(Application.persistentDataPath + "/pointlist.txt"))
            File.Create(Application.persistentDataPath + "/pointlist.txt");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEndlessGame() {
        SceneManager.LoadScene("LoadingScene");
        Setting.getInstance().GameMode = "endless";
        Setting.getInstance().IsEndLessMode = true;

        ObjectPool.getInstance().ReStatr();
    }

    public void StartCheckPointGame()
    {
        SceneManager.LoadScene("LoadingScene");
        Setting.getInstance().GameMode = "checkpoint";
        Setting.getInstance().IsEndLessMode = false;

        ObjectPool.getInstance().ReStatr();
    }

    public void OpenPointList() {
        SceneManager.LoadScene("PointList");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
