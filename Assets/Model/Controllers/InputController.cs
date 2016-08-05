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

    public GameObject TileCursorPF;

    Vector3 dragStartPosition;
    List<GameObject> dragPreviewGameObjects;




    List<GameObject> Cursor_gos = new List<GameObject>();
    public int DistanceX = 10;
    public int DistanceY = 10;

    public Sprite XCursor, TileCursor;

    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    // Use this for initialization
    void Start()
    {
        World world = (World)FindObjectOfType(typeof(World));
        text = (Text)FindObjectOfType(typeof(Text));
        dragPreviewGameObjects = new List<GameObject>();
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

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;

    }
    void OnMouseMove()
    {
        if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
        {

        }
    }

    float Flip(float min, float max, float num)
    {
        return (max + min) - num;
    }
    void CameraControl(int x, int y)
    {

        int zoomMult = 5;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {


            Camera.main.orthographicSize -= (Input.GetAxis("Mouse ScrollWheel") * zoomMult);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize >= 1)
        {
            Camera.main.orthographicSize -= (Input.GetAxis("Mouse ScrollWheel") * zoomMult);

        }
        ScreenScroll(x, y);
        KeyboardScroll(x, y);
    }

    void UpdateDragging()
    {
        // Start Drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currFramePosition;
        }

        int start_x = (int)Mathf.Round(dragStartPosition.x);
        int end_x = (int)Mathf.Round(currFramePosition.x);
        int start_y = (int)Mathf.Round(dragStartPosition.y);
        int end_y = (int)Mathf.Round(currFramePosition.y);

        // Biggo Flippo
        if (end_x < start_x)
        {
            int tmp = end_x;
            end_x = start_x;
            start_x = tmp;
        }
        if (end_y < start_y)
        {
            int tmp = end_y;
            end_y = start_y;
            start_y = tmp;
        }

        // Clean up old drag previews
        while (dragPreviewGameObjects.Count > 0)
        {
            GameObject go = dragPreviewGameObjects[0];
            dragPreviewGameObjects.RemoveAt(0);
            SimplePool.Despawn(go);
        }
        
        if (Input.GetMouseButton(0))
        {
            // Display a preview of the drag area
            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldController.Instance.world.GetTileAt(x, y);
                    if (t != null)
                    {
                        // Display the building hint on top of this tile position
                        GameObject go = SimplePool.Spawn(TileCursorPF, new Vector3(x, y, 0), Quaternion.identity);
                        go.transform.SetParent(this.transform, true);
                        dragPreviewGameObjects.Add(go);
                        
                        
                    }
                }
            }
            
            
                    

        }
        if (Input.GetMouseButtonUp(0))
        {

            // Loop through all the tiles
            for (int x2 = start_x; x2 <= end_x; x2++)
            {
                for (int y2 = start_y; y2 <= end_y; y2++)
                {
                    Tile t = WorldController.Instance.world.GetTileAt(x2, y2);
                    if (t != null)
                    {
                        t.Type = Tile.TileType.Water;
                    }
                }
            }
        }
    }

    public Vector3 createVector3(int x,int y, int z)
    {
        Vector3 vector;
        vector.x = x;
        vector.y = y;
        vector.z = z;
        return vector;
    }

    public int getMid(int a,int b)
    {
        int mid = a + b / 2;
        return mid;
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

    public void KeyboardScroll(int x, int y)
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

    public void WorldInteraction(int x, int y)
    {
        UpdateDragging();




    }

    public void OnMouseDrag()
    {

    }




    public void InterfaceUI()
    {




    }





}
































