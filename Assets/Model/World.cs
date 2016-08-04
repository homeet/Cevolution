using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour
{


    Tile[,] tiles;
    InstalledObject[,] InstObj;
    int width, height;
    int StoneChance = 5;

    public int Width
    {
        get
        {
            return width;
        }
    }
    public int Height
    {
        get
        {
            return height;
        }
    }

    public World(int width = 100, int height = 100)
    {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }
        }
        Debug.Log("World created with " + tiles.Length + " Tiles");
    }
    //GENWORLD
    public void GenerateWorldTiles()
    {
        //Base
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y].Type = Tile.TileType.MeadowGrass;


            }
        }
        //Everything Else
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                ResourceGen(x, y);









            }
        }

    }


    public Tile GetTileAt(int x, int y)
    {
        if (x > width - 1 || x < 0 || y > height - 1 || y < 0)
        {
            Debug.LogError("Tile is out of range! (" + x + "," + y + ")");
            return null;

        }
        else {
            return tiles[x, y];
        }
    }

    public InstalledObject GetInstObjectAt(int x, int y)
    {
        if (x > width - 1 || x < 0 || y > height - 1 || y < 0)
        {
            Debug.LogError("InstObject is out of range! (" + x + "," + y + ")");
            return null;

        }
        else {
            return InstObj[x,y];
        }
    }




    //GENERATION

    //STONES




    public void ResourceGen(int x, int y)
    {
        LimeStoneGen(x, y);
    }



public void LimeStoneGen(int x,int y)
{
        
        if (Random.Range(0, 20) == 0)
    {
            
    }
}


    


















}


