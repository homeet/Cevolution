using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class mob : MonoBehaviour
{

    bool isBaby;
    string Name;
    public Sprite sheepSprite;

    public int Mobx { get; set; }
    public int Moby { get; set; }

    public int HerdID { get; set; }

    int MobID = 0;
    List<int> takenHerdID = new List<int>();
    public List<mob> mobs = new List<mob>();

    AiController aicontroller;
    GameObject worldController = GameObject.Find("WorldController");

    public int AnimalAmount = 3;




    public enum mobTypes
    {
        Sheep
    }
    public mobTypes mobType { get; set; }





    // Use this for initialization
    void Start()
    {
        for (int i = 0; i <= (3 + UnityEngine.Random.Range(-1, 1)); i++)
        {
            int x = UnityEngine.Random.Range(0, 100);
            int y = UnityEngine.Random.Range(0, 100);
            SpawnNewMob(false, mobTypes.Sheep, 1, x, y);
        }
        Update();
    }

    void Update()
    {
        
        float time = 0.0f;
        time += Time.deltaTime;

        if (time <= 60)
        {
            time = 0;
            Debug.Log("Meem");
        }

    }




    public mob(int x, int y, mobTypes species, bool isBaby, int HerdID)
    {

        this.Mobx = x;
        this.Moby = y;
        this.isBaby = isBaby;
        this.HerdID = HerdID;

        

}
    //if HerdID = 0 then create new herd
    public void SpawnNewMob(bool isBaby, mobTypes species, int HerdID, int x, int y)
    {

        GameObject Mob_GO = new GameObject();
        MobID++;
        Mob_GO.name = species + "N." + MobID;
        if (HerdID == 0)
        {
            takenHerdID[takenHerdID.Count + 1] = takenHerdID.Count + 1;
            HerdID = takenHerdID[takenHerdID.Count + 1];
        }

        mobs.Add(new mob(x, y, species, isBaby, HerdID));
        mob Mob_data = getMobFromID(MobID - 1);
        Mob_GO.AddComponent<SpriteRenderer>();
        if (species == mobTypes.Sheep && isBaby == false)
        {
            Mob_GO.GetComponent<SpriteRenderer>().sprite = sheepSprite;
        }
        Mob_GO.transform.position = new Vector3(Mob_data.Mobx, Mob_data.Moby, 1);
        Mob_GO.transform.SetParent(this.transform, true);
    }

    public mob getMobFromID(int id)
    {
        mob mob_data = mobs[id];

        return mob_data;
    }

    public int[] getDistanceBetweenCoords(int x1, int y1, int x2, int y2)
    {
        
        int x = x1 - x2;
        int y = y1 - y2;
        int[] coord = new int[1];
        coord[0] = x;
        coord[1] = y;


        return coord;
    }




    public int[] getMobCoord(int mobID)
    {
        mob thismob = getMobFromID(mobID);
        int x = thismob.Mobx;
        int y = thismob.Moby;

        int[] coord = new int[1];
        if (x > 100 - 1 || x < 0 || y > 100 - 1 || y < 0)
        {
            Debug.Log(y);
            coord[0] = x;
            coord[1] = y;
        }
        return coord;
    }

    public int[] getDistanceBetweenMobs(int mobID1, int mobID2)
    {
        int[] Mob1Loc = getMobCoord(mobID1);
        int[] Mob2Loc = getMobCoord(mobID2);

        int[] coord = getDistanceBetweenCoords(Mob1Loc[0], Mob1Loc[1], Mob2Loc[0], Mob2Loc[1]);


        return coord;
    }

    public void moveMobToCoord(int mobID1, int x, int y)
    {
        mob currMob = getMobFromID(mobID1);




        currMob.transform.Translate(x, y, 1);
    }
}
