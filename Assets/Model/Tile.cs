using UnityEngine;
using System.Collections;
using System;

public class Tile { 

	public enum TileType
    {
        MeadowGrass,stone,Water
    };
    public enum InstalledObjectType
    {
        StoneWall
    }

    public int AmountofTile = Enum.GetNames(typeof(TileType)).Length;

    public GameObject actualObj;
    TileType type = TileType.MeadowGrass;

    Action<Tile> cbTileTypeChanged;
    Action<InstalledObject> cbInstChanged;

    public TileType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
            if(cbTileTypeChanged != null)
            {
                cbTileTypeChanged(this);
            }
        }
    }
    public InstalledObjectType iotype;
    LooseObject looseObject;
    InstalledObject installedObject;

    World world;
    int x, y;
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



    public Tile(World world, int x,int y)
    {
        this.world = world;
        this.x = x;
        this.y = y;
    }

    public void RegisterTileTypeChangedcb(Action<Tile> cb)
    {
        cbTileTypeChanged += cb;
    }
    public void UnregisterTileTypeChangedcb(Action<Tile> cb)
    {
        cbTileTypeChanged -= cb;
    }

    public void RegisterInstChangedcb(Action<InstalledObject> cb)
    {
        cbInstChanged += cb;
    }
    public void UnregisterInstChangedcb(Action<InstalledObject> cb)
    {
        cbInstChanged -= cb;
    }
}
