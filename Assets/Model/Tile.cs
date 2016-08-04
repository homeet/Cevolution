using UnityEngine;
using System.Collections;
using System;

public class Tile { 

	public enum TileType
    {
        MeadowGrass,Limestone,Water
    };
    public int AmountofTile = Enum.GetNames(typeof(TileType)).Length;

    TileType type = TileType.MeadowGrass;

    Action<Tile> cbTileTypeChanged;
    
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
}
