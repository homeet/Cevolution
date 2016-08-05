using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour
{


    Tile[,] tiles;
    Tile tile;
    InstalledObject[,] InstObj;
    int width, height;

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

    public World(int width = 256, int height = 256)
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
            return InstObj[x, y];
        }
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



        float scale = 30f;
        int randomShiftx = Random.Range(0, 200000);
        int randomShifty = Random.Range(0, 200000);
        float waterChance = 0.2f;
        float stoneChance = 0.6f;
        for (int x = 0; x < width; x++)
        {

            for (int y = 0; y < height; y++)
            {
                float Perlin = Mathf.PerlinNoise((x/scale+randomShiftx) , (y/scale + randomShifty));
                if (Perlin < waterChance)
                {
                    
                    tiles[x, y].Type = Tile.TileType.Water;
                }
                else if (Perlin > waterChance && Perlin < stoneChance)
                {
                    tiles[x, y].Type = Tile.TileType.MeadowGrass;
                    
                }
                else
                {
                    tiles[x, y].Type = Tile.TileType.stone;
                }


            }
        }

    }


    




    //GENERATION

    //STONES


}

















