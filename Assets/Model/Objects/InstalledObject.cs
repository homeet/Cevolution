using UnityEngine;
using System.Collections;

public class InstalledObject{

    public enum InstalledObjects
    {
        StoneWall
    }

    int x, y,type;
    public int X
    {
        get
        {
            return x;
        }
    }
    public int Y
    {
        get
        {
            return y;
        }
    }
    public int Type
    {
        get
        {
            return type;
        }
    }

    public InstalledObject(int x,int y,int type)
    {

    }
    public InstalledObjects Otype
    {
        get
        {
            return Otype;
        }
        set
        {
            Otype = value;
        }
    }


}
