using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InputController : MonoBehaviour
{
    /// <summary>
    /// DEBUG
    /// </summary>
    /// 

    //TEXT
    Text text;


    
    

    List<GameObject> Cursor_gos = new List<GameObject>();
    public int DistanceX = 75;
    public int DistanceY = 75;

    public Sprite XCursor, TileCursor;

    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    // Use this for initialization
    void Start()
    {
        World world = (World)FindObjectOfType(typeof(World));
        text = (Text)FindObjectOfType(typeof(Text));
    }


    // Update is called once per frame
    void Update()
    {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;
        Vector3 MousePos = Input.mousePosition;
        int x = (int)MousePos[0];
        int y = (int)MousePos[1];
        CameraControl(x, y);
        InterfaceUI();
        WorldInteraction((int)Mathf.Round(currFramePosition[0]), (int)Mathf.Round(currFramePosition[1]));
        
    }
    float Flip(float min, float max, float num)
    {
        return (max + min) - num;
    }
    void CameraControl(int x, int y)
    {



        if (Input.GetAxis("Mouse ScrollWheel") < 0) //back
        {


            Camera.main.transform.Translate(0, 0, -1);
        }
    
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.transform.position.z >= 2)
            {
                Camera.main.transform.Translate(0, 0, 1);
            }

        }
        ScreenScroll(x, y);
        KeyboardScroll(x, y);
    }

    

  

    public void ScreenScroll(int x, int y)
    {
        float XDiff = Screen.width - DistanceX;
        float YDiff = Screen.height - DistanceY;
        if (x <= DistanceX)
        {
            float Speed = -(Flip(0, DistanceX, x));
            Camera.main.transform.Translate(Speed / 200, 0, 0);
        }
        else if (x >= XDiff)
        {
            float Speed = (Flip(0, DistanceX, XDiff - x)) - DistanceX;
            Camera.main.transform.Translate(Speed / 200, 0, 0);
        }

        if (y <= DistanceY)
        {
            float Speed = -(Flip(0, DistanceY, y));
            Camera.main.transform.Translate(0, Speed / 200, 0);
        }
        else if (y >= YDiff)
        {
            float Speed = (Flip(0, DistanceY, YDiff - y)) - DistanceY;
            Debug.Log(Speed);
            Camera.main.transform.Translate(0, Speed / 200, 0);
        }
    }

    public void KeyboardScroll(int x,int y)
    {
        if (Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.Translate(0, 0.5f, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.Translate(0, -0.5f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.Translate(-0.5f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.Translate(0.5f, 0, 0);
        }
        if (Input.GetKey(KeyCode.PageDown))
        {
            Camera.main.transform.Translate(0, 0, -0.5f);
        }
        if (Input.GetKey(KeyCode.PageUp))
        {
            Camera.main.transform.Translate(0, 0, 0.5f);
        }
    }

    public void WorldInteraction(int x,int y)
    {
        while(Input.GetMouseButtonDown(0))
            {
                
            GameObject Cursor_go = new GameObject("Cursor " + x + "_" + y);
            Cursor Cursor_data = new Cursor(x, y);
            Cursor_data.SetSprite(x, y,Cursor_go);
            Tile t = WorldController.Instance.world.GetTileAt(x, y);
            Cursor_go.AddComponent<SpriteRenderer>();
            Cursor_gos.Add(Cursor_go);
                Cursor_go.GetComponent<SpriteRenderer>().sprite = TileCursor;
                //Cursor_go.GetComponent<SpriteRenderer>().sprite = XCursor;
            Cursor_go.transform.position = new Vector3(x, y, -1);
            text.text = x + "_" + y;
            text.enabled = true;
        }

        
      
    }





    public void InterfaceUI()
    {



        
    }





}
































