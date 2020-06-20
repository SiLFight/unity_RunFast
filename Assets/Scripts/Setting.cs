using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting
{

    private static Setting instance;

    private Setting() {
      
    }

    public static Setting getInstance() {
        if (instance == null)
            instance = new Setting();
        return instance;
    }

    //endless为无尽，checkpoint为关卡
    public string GameMode { get; set; }

    public bool IsEndLessMode { get; set; }
}
