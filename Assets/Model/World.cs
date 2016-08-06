using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour
{
    public int chunkLength = 16;
    public GameObject[,] chunks;
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

    public World(int width = 128, int height = 128)
    {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];
        chunks = new GameObject[width / chunkLength, height / chunkLength];

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



    public void fillMapWithInstObjects()
    {
        for (int x = 0; x > WorldController.Instance.world.width; x++)
        {
            for (int y = 0; y > WorldController.Instance.world.height; y++)
            {
                InstObj[x, y] = new InstalledObject(x,y,0);
                GameObject obj_go = new GameObject();
                obj_go.name = "Object" + x + "_" + y;
                InstalledObject obj_data = WorldController.Instance.world.GetInstObjectAt(x, y);
                obj_data.actualObj = obj_go;
                obj_go.AddComponent<SpriteRenderer>();
                obj_go.SetActive(false);
            }
        }
    }


    




    //GENERATION

    //STONES


}

















