using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Villager : MonoBehaviour {



    public int x, y;

    List<GameObject> villager_gos = new List<GameObject>();
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public Villager(int x,int y)
    {
        this.x = x;
        this.y = y;
        spawnVillager(x, y);

    }

    public void spawnVillager(int x,int y)
    {
        GameObject villager_go = new GameObject();
        villager_gos.Add(villager_go);
        
    }




}
