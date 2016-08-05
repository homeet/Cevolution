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
        villager_go.AddComponent<SpriteRenderer>().sprite = WorldController.Instance.Villager;
        villager_go.transform.position = WorldController.Instance.createVector3(x, y, 1);
        villager_go.transform.SetParent(this.transform, true);
    }




}
