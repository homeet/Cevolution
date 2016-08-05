using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class WorldController : MonoBehaviour {



    public static WorldController Instance { get; protected set; }

    public World world { get; protected set; }

    //tiles
    public Sprite stoneSprite;
    public Sprite MeadowGrassSprite;
    public Sprite WaterSprite;

    //installedObjects
    public Sprite StoneWall;

    public List<Villager> villagers= new List<Villager>();


    public int getMid(int a, int b)
    {
        int mid = a + b / 2;
        return mid;
    }
    public int getMid(int a)
    {
        int mid = a / 2;
        return mid;
    }

    // Use this for initialization
    void Start () {
        //empty world
        world = new World();
        Instance = this;
        
        //game object for every tile
        for (int x = 0; x < world.Width; x++){
            for (int y = 0; y < world.Height; y++){
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                Tile tile_data = world.GetTileAt(x, y);
                tile_data.actualObj = tile_go;
                tile_go.AddComponent<SpriteRenderer>();
                tile_data.RegisterTileTypeChangedcb((tile) => { OnTileTypeChanged(tile, tile_go); });
            }
        }
        //not so empty
        world.GenerateWorldTiles();

        //chunk gen
        for (int cX = 0; cX < world.Width / world.chunkLength; cX++) {
            for (int cY = 0; cY < world.Height / world.chunkLength; cY++) {
                Vector2 worldPos = new Vector2(cX * world.chunkLength, cY * world.chunkLength);
                GameObject chunkObj = new GameObject(cX + "/" + cY);
                chunkObj.transform.parent = transform;
                chunkObj.transform.position = worldPos;
                world.chunks[cX, cY] = chunkObj;
                for (int x = 0; x < world.chunkLength; x++) {
                    for (int y = 0; y < world.chunkLength; y++) {
                        Vector2 tilePos = new Vector2(worldPos.x + x, worldPos.y + y);
                        Tile t = world.GetTileAt((int)tilePos.x, (int)tilePos.y);
                        t.actualObj.transform.parent = chunkObj.transform;
                    }
                }
            }
        }
        for (int i = 0; i < 3; i++)
        {
            Villager villager = new Villager(getMid(world.Width), getMid(world.Height));
            villagers.Add(villager);
        }
        
    }


    float time = 2f;
    // Update is called once per frame
    void Update () {
        foreach (GameObject o in world.chunks) {
            if (Vector2.Distance(o.transform.position, Camera.main.transform.position) > 60) {
                o.SetActive(false);
            } else o.SetActive(true);
        }
	}

    void OnTileTypeChanged(Tile tile_data , GameObject tile_go)
    {

        if (tile_data.Type == Tile.TileType.stone)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = stoneSprite;
        }
        else if (tile_data.Type == Tile.TileType.MeadowGrass)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = MeadowGrassSprite;
        }
        else if (tile_data.Type == Tile.TileType.Water)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = WaterSprite;
        }
        else {
            Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
        }
        tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
        tile_go.transform.SetParent(this.transform, true);
    }
    public Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return world.GetTileAt(x, y);
    }



    public InstalledObject GetInstObjectAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return world.GetInstObjectAt(x, y);
    }

   
}
