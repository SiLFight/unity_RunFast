using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : IComparer<Point>
{ 
    public int point;

    public Point(int point) {
        this.point = point;
    }

    public Point()
    {
    }

    public int Compare(Point x, Point y)
    {
        if (x.point > y.point)
        {
            return -1;
        }
        else if (x.point < y.point)
        {
            return 1;
        }
        else 
        {
            return 0;
        }
    }
}
