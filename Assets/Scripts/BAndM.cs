using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAndM
{
    public string name;
    public Vector3 pos;

    public BAndM(string n, float x, float z) {
        this.name = n;
        this.pos = new Vector3(x, 2, z);
    }
}
