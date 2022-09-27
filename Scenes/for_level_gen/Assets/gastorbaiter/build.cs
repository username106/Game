using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class build : MonoBehaviour
{
    //public NavMeshSurface surfase;
    public NavMeshSurface[] surfaces;
    //------45 deg raycast chek
    private RaycastHit forward45;
    private bool forward45_check;
    private RaycastHit back45;
    private bool back45_check;
    private RaycastHit right45;
    private bool right45_check;
    private RaycastHit left45;
    private bool left45_check;
    //---negative 45 deg raycast chek
    private RaycastHit forward45negativ;
    private bool forward45negativ_check;
    private RaycastHit back45negativ;
    private bool back45negativ_check;
    private RaycastHit right45negativ;
    private bool right45negativ_check;
    private RaycastHit left45negativ;
    private bool left45negativ_check;
    //--------------raycast chek
    private RaycastHit right;
    private bool right_check;
    private RaycastHit left;
    private bool left_check;
    private RaycastHit up;
    private bool up_check;
    private RaycastHit down;
    private bool down_check;
    private RaycastHit forward;
    private bool forward_check;
    private RaycastHit back;
    private bool back_check;
    GameObject objToSpawn;
    //--------------prefab fields
    private bool Roof_was_build;
    private bool isDone;
    //STAIRS
    [SerializeField]
    GameObject StairsPrefab;
    //ROOF FOR CORR
    [SerializeField]
    GameObject RoofPrefab;
    //FlOOR FOR CORR
    [SerializeField]
    GameObject FloorPrefab;
    //WALL FOR CORR
    [SerializeField]
    GameObject Side_Wll_Cordr_Prefab;
    //PLACEHOLDER FOR ROOMS
    [SerializeField]
    GameObject TempRoomPrefab;
    //ROOF FOR ROOM
    [SerializeField]
    GameObject Roof_RoomPrefab;
    //FLOOR FOR ROOM
    [SerializeField]
    GameObject Floor_RoomPrefab;
    //WALL FOR ROOM
    [SerializeField]
    GameObject Wall_RoomPrefab;
    //PLAYER
    [SerializeField]
    GameObject Player;
    //ENAMI
    [SerializeField]
    GameObject Enamy;
    //---
    
     int enamy_count;
     bool easyl;
     bool mediuml;
     bool hardl;
    //---------------------------------
    [SerializeField]
    public GameObject Empty;
    private GameObject floor;
    private GameObject camera_pose;
    private GameObject actually_camera_pose;
    //removable
    bool spawned;

    void Start()
    {
        //Empty = new GameObject();
        //surfase.BuildNavMesh();
        RoomOrbiter();

        LefttWallBuilder();
        RightWallBuilder();
        BackWallBuilder();
        ForwardWallBuilder();
        RoofBuilder();
        FloorBuilder();

        StairsOrbitor();
        StairsBuilder();

        RoomWallBilder();

        StairsDestroier();

        RoomPlusCorridorDestroyer();
    }
    
    void Update()
    {
        if (!isDone)
        {
            ScaleIt();
            navIt();
            SpawnPoint();
            agent_apear();
            isDone = true;
        }
        agentsi();

        //RaycastChecking();
    }
    void RaycastChecking()
    {
        GameObject[] goS = GameObject.FindGameObjectsWithTag("stairs");
        GameObject[] goS1 = GameObject.FindGameObjectsWithTag("stairs1");
        GameObject[] goS2 = GameObject.FindGameObjectsWithTag("stairs2");
        GameObject[] goS3 = GameObject.FindGameObjectsWithTag("stairs3");
        for (int i = 0; i < goS.Length; i++)
        {
            var Vforward45 = (transform.forward + transform.up).normalized;
            var Vback45 = (-transform.forward + transform.up).normalized;
            var Vright45 = (transform.right + transform.up).normalized;
            var Vleft45 = (-transform.right + transform.up).normalized;
            transform.position = goS[i].transform.position + new Vector3(0.5f, 0.5f, 0.5f);
            Debug.DrawRay(transform.position, transform.forward * 0.51f, Color.blue);
            Debug.DrawRay(transform.position, -transform.forward * 0.51f, Color.blue);
            Debug.DrawRay(transform.position, transform.right * 0.51f, Color.red);
            Debug.DrawRay(transform.position, -transform.right * 0.51f, Color.red);
            Debug.DrawRay(transform.position, transform.up * 0.51f, Color.white);
            Debug.DrawRay(transform.position, -transform.up * 0.51f, Color.white);
            Debug.DrawRay(transform.position, Vforward45 * 0.81f, Color.red);
            Debug.DrawRay(transform.position, Vback45 * 0.81f, Color.red);
            Debug.DrawRay(transform.position, Vright45 * 0.81f, Color.red);
            Debug.DrawRay(transform.position, Vleft45 * 0.81f, Color.red);
        }
        for (int i = 0; i < goS1.Length; i++)
        {
            var Vforward45 = (transform.forward + transform.up).normalized;
            var Vback45 = (-transform.forward + transform.up).normalized;
            var Vright45 = (transform.right + transform.up).normalized;
            var Vleft45 = (-transform.right + transform.up).normalized;
            transform.position = goS1[i].transform.position + new Vector3(0.5f, 0.5f, 0.5f);
            Debug.DrawRay(transform.position, transform.forward * 0.51f, Color.blue);
            Debug.DrawRay(transform.position, -transform.forward * 0.51f, Color.blue);
            Debug.DrawRay(transform.position, transform.right * 0.51f, Color.red);
            Debug.DrawRay(transform.position, -transform.right * 0.51f, Color.red);
            Debug.DrawRay(transform.position, transform.up * 0.51f, Color.white);
            Debug.DrawRay(transform.position, -transform.up * 0.51f, Color.white);
            Debug.DrawRay(transform.position, Vforward45 * 0.81f, Color.red);
            Debug.DrawRay(transform.position, Vback45 * 0.81f, Color.red);
            Debug.DrawRay(transform.position, Vright45 * 0.81f, Color.red);
            Debug.DrawRay(transform.position, Vleft45 * 0.81f, Color.red);
        }
        for (int i = 0; i < goS2.Length; i++)
        {
            var Vforward45 = (transform.forward + transform.up).normalized;
            var Vback45 = (-transform.forward + transform.up).normalized;
            var Vright45 = (transform.right + transform.up).normalized;
            var Vleft45 = (-transform.right + transform.up).normalized;
            transform.position = goS2[i].transform.position + new Vector3(0.5f, 0.5f, 0.5f);
            Debug.DrawRay(transform.position, transform.forward * 0.51f, Color.blue);
            Debug.DrawRay(transform.position, -transform.forward * 0.51f, Color.blue);
            Debug.DrawRay(transform.position, transform.right * 0.51f, Color.red);
            Debug.DrawRay(transform.position, -transform.right * 0.51f, Color.red);
            Debug.DrawRay(transform.position, transform.up * 0.51f, Color.white);
            Debug.DrawRay(transform.position, -transform.up * 0.51f, Color.white);
            Debug.DrawRay(transform.position, Vforward45 * 0.81f, Color.red);
            Debug.DrawRay(transform.position, Vback45 * 0.81f, Color.red);
            Debug.DrawRay(transform.position, Vright45 * 0.81f, Color.red);
            Debug.DrawRay(transform.position, Vleft45 * 0.81f, Color.red);
        }
        for (int i = 0; i < goS3.Length; i++)
        {
            var Vforward45 = (transform.forward + transform.up).normalized;
            var Vback45 = (-transform.forward + transform.up).normalized;
            var Vright45 = (transform.right + transform.up).normalized;
            var Vleft45 = (-transform.right + transform.up).normalized;
            transform.position = goS3[i].transform.position + new Vector3(0.5f, 0.5f, 0.5f);
            Debug.DrawRay(transform.position, transform.forward * 0.51f, Color.blue);
            Debug.DrawRay(transform.position, -transform.forward * 0.51f, Color.blue);
            Debug.DrawRay(transform.position, transform.right * 0.51f, Color.red);
            Debug.DrawRay(transform.position, -transform.right * 0.51f, Color.red);
            Debug.DrawRay(transform.position, transform.up * 0.51f, Color.white);
            Debug.DrawRay(transform.position, -transform.up * 0.51f, Color.white);
            Debug.DrawRay(transform.position, Vforward45 * 0.81f, Color.red);
            Debug.DrawRay(transform.position, Vback45 * 0.81f, Color.red);
            Debug.DrawRay(transform.position, Vright45 * 0.81f, Color.red);
            Debug.DrawRay(transform.position, Vleft45 * 0.81f, Color.red);
        }
    }
    void SpawnPoint()
    {
        GameObject[] floor = GameObject.FindGameObjectsWithTag("FloorRoom");
        transform.position = floor[Random.Range(0, floor.Length)].transform.position;
        objToSpawn = new GameObject();
        objToSpawn.tag = "Respawn";
        objToSpawn.transform.position = transform.position;
        Instantiate(Player, objToSpawn.transform.position + new Vector3(0.0f, 0.0f, 0.0f), Quaternion.Euler(0, 0, 0));//-------------------------------------------------------------------
        //camera_pose = GameObject.Find("cam_pos");
        //actually_camera_pose = GameObject.Find("Player_1(Clone)/target_cam_pos");
        //camera_pose.transform.position = actually_camera_pose.transform.position;
        //camera_pose.transform.parent = actually_camera_pose.transform;
    }
    void agent_apear()
    {
        GameObject[] floors = GameObject.FindGameObjectsWithTag("FloorRoom");
        enamy_count = Random.Range(2, 5);
        bool spawned = false;

        for (int i = 0; i < enamy_count; i++)
        {
            spawned = true;
            for (int f = 0; f < floors.Length; f++)
            {
                if (spawned)
                {
                    transform.position = floors[Random.Range(0, floors.Length)].transform.position;
                    Instantiate(Enamy, transform.position + new Vector3(0.0f, 0.0f, 0.0f), Quaternion.Euler(0, 0, 0));
                    //break;
                    Debug.Log(transform.position);
                    spawned = false;
                }
            }
        }
    }
    void agentsi()
    {
        if (GameObject.Find("1Z_enenemi(Clone)") != null)
        {
            Debug.Log("aaa");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene("win");
        }
    }
    void navIt()
    {
        for (int q = 0; q < surfaces.Length; q++)
        {
            surfaces[q].BuildNavMesh();
        }
    }
    void ScaleIt()
    {
        Empty.transform.localScale += new Vector3(100, 100, 100);
    }
    void RoomOrbiter()
    {
        GameObject[] goR = GameObject.FindGameObjectsWithTag("room");
        for (int i = 0; i < goR.Length; i++)
        {
            float scaleX = goR[i].transform.localScale.x;
            float x = 0;
            float scaleY = goR[i].transform.localScale.y;
            float scaleZ = goR[i].transform.localScale.z;
            //Debug.Log(goR[i].transform.position + " X" + (scaleX) + " Y" + (scaleY) + " Z" + (scaleZ));
            while (x < scaleX)
            {
                for (int j = 0; j < scaleZ; j++)
                {
                    for (int k = 0; k < scaleY; k++)
                    {
                        Instantiate(TempRoomPrefab, goR[i].transform.position + new Vector3(0.5f + x, 0.5f + k, 0.5f + j), Quaternion.Euler(0, 0, 0));
                    }
                }
                x++;
            }
        }
        for (int i = 0; i < goR.Length; i++)
        {
            Destroy(goR[i]);
        }
    }
    void RoomWallBilder()
    {
        GameObject[] goR = GameObject.FindGameObjectsWithTag("roomer");
        for (int i = 0; i < goR.Length; i++)
        {
            transform.position = goR[i].transform.position;
            right_check = Physics.Raycast(transform.position, transform.right * 0.71f, out right, 0.71f);
            left_check = Physics.Raycast(transform.position, -transform.right * 0.71f, out left, 0.71f);
            up_check = Physics.Raycast(transform.position, transform.up * 0.71f, out up, 0.71f);
            down_check = Physics.Raycast(transform.position, -transform.up * 0.71f, out down, 0.71f);
            forward_check = Physics.Raycast(transform.position, transform.forward * 0.71f, out forward, 0.71f);
            back_check = Physics.Raycast(transform.position, -transform.forward * 0.71f, out back, 0.71f);
            if (!right_check)
            {
                GameObject myWall = Instantiate(Wall_RoomPrefab, goR[i].transform.position + new Vector3(0.56f, 0.0f, 0.0f), Quaternion.Euler(90, 90, 0)) as GameObject;
                myWall.transform.parent = Empty.transform;
            }
            if (!left_check)
            {
                GameObject myWall = Instantiate(Wall_RoomPrefab, goR[i].transform.position + new Vector3(-0.5f, 0.0f, 0.0f), Quaternion.Euler(90, 90, 0)) as GameObject;
                myWall.transform.parent = Empty.transform;
            }
            if (!forward_check)
            {
                GameObject myWall = Instantiate(Wall_RoomPrefab, goR[i].transform.position + new Vector3(0.0f, 0.0f, 0.56f), Quaternion.Euler(90, 0, 0)) as GameObject;
                myWall.transform.parent = Empty.transform;
            }
            if (!back_check)
            {
                GameObject myWall = Instantiate(Wall_RoomPrefab, goR[i].transform.position + new Vector3(0.0f, 0.0f, -0.5f), Quaternion.Euler(90, 0, 0)) as GameObject;
                myWall.transform.parent = Empty.transform;
            }
            if (!up_check)
            {
                GameObject myWall = Instantiate(Roof_RoomPrefab, goR[i].transform.position + new Vector3(0.0f, 0.56f, 0.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
                myWall.transform.parent = Empty.transform;
            }
            if (!down_check)
            {
                GameObject myWall = Instantiate(Floor_RoomPrefab, goR[i].transform.position + new Vector3(0.0f, -0.50f, 0.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
                myWall.transform.parent = Empty.transform;                                
            }
        }
    }
    void RoomPlusCorridorDestroyer()
    {
        GameObject[] goR = GameObject.FindGameObjectsWithTag("roomer");
        for (int i = 0; i < goR.Length; i++)
        {
            Destroy(goR[i]);
        }
        //del me plz!!!!!!
        GameObject[] goR1 = GameObject.FindGameObjectsWithTag("room");
        for (int i = 0; i < goR1.Length; i++)
        {
            Destroy(goR1[i]);
        }
        //
        GameObject[] goC = GameObject.FindGameObjectsWithTag("corridor");
        for (int i = 0; i < goC.Length; i++)
        {
            Destroy(goC[i]);
        }
    }
    void RoofBuilder()
    {
        GameObject[] goC = GameObject.FindGameObjectsWithTag("corridor");
        for (int i = 0; i < goC.Length; i++)
        {
            Roof_was_build = false;
            transform.position = goC[i].transform.position + new Vector3(0.5f, 0.5f, 0.5f);
            up_check = Physics.Raycast(transform.position, transform.up * 0.75f, out up, 0.75f);
            if (up_check && up.transform.tag == "corridor" && !Roof_was_build)
            {
                if (Random.value > 0.7) //%70 percent chance
                {
                    Roof_was_build = true;
                }
                else
                {
                    GameObject go = Instantiate(RoofPrefab, goC[i].transform.position + new Vector3(0.5f, 1.05f, 0.5f), Quaternion.Euler(0, 0, 0)) as GameObject;
                    go.transform.parent = Empty.transform;
                }
                Roof_was_build = true;
            }
            //if (up_check && up.transform.tag == "stairs" && !Roof_was_build || up_check && up.transform.tag == "stairs1" && !Roof_was_build || up_check && up.transform.tag == "stairs2" && !Roof_was_build || up_check && up.transform.tag == "stairs3" && !Roof_was_build)
            //{
            //    GameObject go = Instantiate(RoofPrefab, goC[i].transform.position + new Vector3(0.5f, 1.05f, 0.5f), Quaternion.Euler(0, 0, 0)) as GameObject;
            //    go.transform.parent = Empty.transform;
            //    Roof_was_build = true;
            //}            
            if (!Roof_was_build && !up_check)
            {
                GameObject go = Instantiate(RoofPrefab, goC[i].transform.position + new Vector3(0.5f, 1.05f, 0.5f), Quaternion.Euler(0, 0, 0)) as GameObject;
                go.transform.parent = Empty.transform;
            }
        }
    }

    void FloorBuilder()
    {
        GameObject[] goC = GameObject.FindGameObjectsWithTag("corridor");
        for (int i = 0; i < goC.Length; i++)
        {
            Roof_was_build = false;
            transform.position = goC[i].transform.position + new Vector3(0.5f, 0.5f, 0.5f);
            down_check = Physics.Raycast(transform.position, -transform.up * 0.51f, out down, 0.51f);
            
            if (!Roof_was_build && !down_check)
            {
                GameObject go = Instantiate(FloorPrefab, goC[i].transform.position + new Vector3(0.5f, 0f, 0.5f), Quaternion.Euler(0, 0, 0)) as GameObject;
                go.transform.parent = Empty.transform;
            }
        }
    }
    void RightWallBuilder()
    {
        GameObject[] goC = GameObject.FindGameObjectsWithTag("corridor");
        for (int i = 0; i < goC.Length; i++)
        {
            Roof_was_build = false;
            transform.position = goC[i].transform.position + new Vector3(0.5f, 0.5f, 0.5f);
            right_check = Physics.Raycast(transform.position, transform.right * 0.71f, out right, 0.71f);
          
            if (!Roof_was_build && !right_check)
            {
                GameObject go = Instantiate(Side_Wll_Cordr_Prefab, goC[i].transform.position + new Vector3(1f, 0.5f, 0.5f), Quaternion.Euler(0, 0, 90)) as GameObject;
                go.transform.parent = Empty.transform;
            }
        }
    }
    void LefttWallBuilder()
    {
        GameObject[] goC = GameObject.FindGameObjectsWithTag("corridor");
        for (int i = 0; i < goC.Length; i++)
        {
            Roof_was_build = false;
            transform.position = goC[i].transform.position + new Vector3(0.5f, 0.5f, 0.5f);
            left_check = Physics.Raycast(transform.position, -transform.right * 0.71f, out left, 0.71f);
            
            if (!Roof_was_build && !left_check)
            {
                GameObject go = Instantiate(Side_Wll_Cordr_Prefab, goC[i].transform.position + new Vector3(-0.05f, 0.5f, 0.5f), Quaternion.Euler(0, 0, 90)) as GameObject;
                go.transform.parent = Empty.transform;
            }
        }
    }
    void BackWallBuilder()
    {
        GameObject[] goC = GameObject.FindGameObjectsWithTag("corridor");
        for (int i = 0; i < goC.Length; i++)
        {
            Roof_was_build = false;
            transform.position = goC[i].transform.position + new Vector3(0.5f, 0.5f, 0.5f);
            back_check = Physics.Raycast(transform.position, -transform.forward * 0.71f, out back, 0.71f);
           
            if (!Roof_was_build && !back_check)
            {
                GameObject go = Instantiate(Side_Wll_Cordr_Prefab, goC[i].transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.Euler(90, 0, 0)) as GameObject;
                go.transform.parent = Empty.transform;
            }
        }
    }
    void ForwardWallBuilder()
    {
        GameObject[] goC = GameObject.FindGameObjectsWithTag("corridor");
        for (int i = 0; i < goC.Length; i++)
        {
            Roof_was_build = false;
            transform.position = goC[i].transform.position + new Vector3(0.5f, 0.5f, 0.5f);
            forward_check = Physics.Raycast(transform.position, transform.forward * 0.71f, out forward, 0.71f);
           
            if (!Roof_was_build && !forward_check)
            {
                GameObject go = Instantiate(Side_Wll_Cordr_Prefab, goC[i].transform.position + new Vector3(0.5f, 0.5f, 1.06f), Quaternion.Euler(90, 0, 0)) as GameObject;
                go.transform.parent = Empty.transform;
            }
        }
    }
    void StairsOrbitor()
    {
        GameObject[] goS = GameObject.FindGameObjectsWithTag("stairs");        
        int q = 0;
        for (int i = 0; i < goS.Length; i++)
        {
            if (q == 4) { q -= 3; }
            else { q += 1; }

            switch (q)
            {
                case 1:
                    goS[i].tag = "stairs";
                    break;
                case 2:
                    goS[i].tag = "stairs1";
                    break;
                case 3:
                    goS[i].tag = "stairs2";
                    break;
                case 4:
                    goS[i].tag = "stairs3";
                    break;
            }
            //Debug.Log(goS[i].transform.position +"  "+ i + "  " + q);
        }
    }
    void StairsBuilder()
    {
        GameObject[] goR = GameObject.FindGameObjectsWithTag("room");
        GameObject[] goC = GameObject.FindGameObjectsWithTag("corridor");
        GameObject[] goS = GameObject.FindGameObjectsWithTag("stairs");
        GameObject[] goS1 = GameObject.FindGameObjectsWithTag("stairs1");
        GameObject[] goS2 = GameObject.FindGameObjectsWithTag("stairs2");
        GameObject[] goS3 = GameObject.FindGameObjectsWithTag("stairs3");       

        for (int i = 0; i < goS.Length; i++)
        {
            transform.position = goS[i].transform.position + new Vector3(0.5f, 0.5f, 0.5f);
            right_check = Physics.Raycast(transform.position, transform.right * 0.71f, out right, 0.71f);
            left_check = Physics.Raycast(transform.position, -transform.right * 0.71f, out left, 0.71f);
            up_check = Physics.Raycast(transform.position, transform.up * 0.71f, out up, 0.71f);
            down_check = Physics.Raycast(transform.position, -transform.up * 0.71f, out down, 0.71f);
            forward_check = Physics.Raycast(transform.position, transform.forward * 0.71f, out forward, 0.71f);
            back_check = Physics.Raycast(transform.position, -transform.forward * 0.71f, out back, 0.71f);
            //-------------------------------
            var Vforward45 = (transform.forward + transform.up).normalized;
            var Vback45 = (-transform.forward + transform.up).normalized;
            var Vright45 = (transform.right + transform.up).normalized;
            var Vleft45 = (-transform.right + transform.up).normalized;
            right45_check = Physics.Raycast(transform.position, Vright45 * 1f, out right45, 1f);
            left45_check = Physics.Raycast(transform.position, Vleft45 * 1f, out left45, 1f);
            forward45_check = Physics.Raycast(transform.position, Vforward45 * 1f, out forward45, 1f);
            back45_check = Physics.Raycast(transform.position, Vback45 * 1f, out back45, 1f);
            //-NEGATIV RAYCAST CHECK OF 45 DEG Angles
            var Vforward45negativ = (transform.forward + -transform.up).normalized;
            var Vback45negativ = (-transform.forward + -transform.up).normalized;
            var Vright45negativ = (transform.right + -transform.up).normalized;
            var Vleft45negativ = (-transform.right + -transform.up).normalized;
            right45negativ_check = Physics.Raycast(transform.position, Vright45negativ * 1f, out right45negativ, 1f);
            left45negativ_check = Physics.Raycast(transform.position, Vleft45negativ * 1f, out left45negativ, 1f);
            forward45negativ_check = Physics.Raycast(transform.position, Vforward45negativ * 1f, out forward45negativ, 1f);
            back45negativ_check = Physics.Raycast(transform.position, Vback45negativ * 1f, out back45negativ, 1f);
            

            if (down_check && down.transform.tag == "stairs2" && forward_check && forward.transform.tag == "corridor" && back_check && back.transform.tag == "stairs1" && back45negativ_check && back45negativ.transform.tag == "stairs3")// -S2 F S2
            {
                //Debug.Log(goS[i].transform.position + "-S2 F S2");
                GameObject go = Instantiate(StairsPrefab, goS3[i].transform.position + new Vector3(0.5f, 2.05f, 1f), Quaternion.Euler(0, 180, 0)) as GameObject;
                go.transform.parent = Empty.transform;
            }

            if (down_check && down.transform.tag == "stairs2" && right_check && right.transform.tag == "corridor" && left_check && left.transform.tag == "stairs1" && left45negativ_check && left45negativ.transform.tag == "stairs3")// -S2 R S2 
            {
                GameObject go = Instantiate(StairsPrefab, goS3[i].transform.position + new Vector3(1.0f, 2.05f, 0.5f), Quaternion.Euler(0, -90, 0)) as GameObject;
                go.transform.parent = Empty.transform;
                //Debug.Log(goS[i].transform.position + "-S2 R S2");
            }

            if (down_check && down.transform.tag == "stairs2" && left_check && left.transform.tag == "corridor" && right_check && right.transform.tag == "stairs1" && right45negativ_check && right45negativ.transform.tag == "stairs3")// -S2 L S2
            {
                GameObject go = Instantiate(StairsPrefab, goS3[i].transform.position + new Vector3(0.0f, 2.05f, 0.5f), Quaternion.Euler(0, 90, 0)) as GameObject;
                go.transform.parent = Empty.transform;
                //Debug.Log(goS[i].transform.position + "-S2 L S2");
            }

            if (down_check && down.transform.tag == "stairs2" && back_check && back.transform.tag == "corridor" && forward_check && forward.transform.tag == "stairs1" && forward45negativ_check && forward45negativ.transform.tag == "stairs3")// -S2 B S2
            {
                GameObject go = Instantiate(StairsPrefab, goS3[i].transform.position + new Vector3(0.5f, 2.05f, 0.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
                go.transform.parent = Empty.transform;
                //Debug.Log(goS[i].transform.position + "-S2 B S2");
            }
            //Another check where S type on top NEGATIVE VARIUCION OF CUBICKS
            //S2 Category S2 Category S2 Category S2 Category S2 Category S2 Category S2 Category S2 Category S2 Category S2 Category S2 Category S2 Category S2 Category S2 Category
            if (up_check && up.transform.tag == "stairs2" && left_check && left.transform.tag == "corridor" && right_check && right.transform.tag == "stairs1" && right45_check && right45.transform.tag == "stairs3")// S2 L S1
            {
                GameObject go = Instantiate(StairsPrefab, goS[i].transform.position + new Vector3(1.0f, 2.05f, 0.5f), Quaternion.Euler(0, -90, 0)) as GameObject;
                go.transform.parent = Empty.transform;

                //Debug.Log(goS[i].transform.position + "S2 L S1");
            }
            if (up_check && up.transform.tag == "stairs2" && back_check && back.transform.tag == "corridor" && forward_check && forward.transform.tag == "stairs1" && forward45_check && forward45.transform.tag == "stairs3")//  S2 B S1
            {
                GameObject go = Instantiate(StairsPrefab, goS[i].transform.position + new Vector3(0.5f, 2.05f, 1.0f), Quaternion.Euler(0, 180, 0)) as GameObject;
                go.transform.parent = Empty.transform;

                //Debug.Log(goS[i].transform.position + "S2 B S1");
            }

            if (up_check && up.transform.tag == "stairs2" && forward_check && forward.transform.tag == "corridor" && back_check && back.transform.tag == "stairs1" && back45_check && back45.transform.tag == "stairs3")// S2 F S1
            {
                GameObject go = Instantiate(StairsPrefab, goS[i].transform.position + new Vector3(0.5f, 2.05f, 0.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
                go.transform.parent = Empty.transform;

                //Debug.Log(goS[i].transform.position + "S2 F S1");
            }

            if (up_check && up.transform.tag == "stairs2" && right_check && right.transform.tag == "corridor" && left_check && left.transform.tag == "stairs1" && left45_check && left45.transform.tag == "stairs3")// S2 R S1 
            {
                GameObject go = Instantiate(StairsPrefab, goS[i].transform.position + new Vector3(0.0f, 2.05f, 0.5f), Quaternion.Euler(0, 90, 0)) as GameObject;
                go.transform.parent = Empty.transform;

                //Debug.Log(goS[i].transform.position + "S2 R S1");
            }
        }
    }
    void StairsDestroier()
    {
        GameObject[] goS = GameObject.FindGameObjectsWithTag("stairs");
        GameObject[] goS1 = GameObject.FindGameObjectsWithTag("stairs1");
        GameObject[] goS2 = GameObject.FindGameObjectsWithTag("stairs2");
        GameObject[] goS3 = GameObject.FindGameObjectsWithTag("stairs3");
        for (int i = 0; i < goS.Length; i++)
        { goS[i].SetActive(false); }
        for (int i = 0; i < goS1.Length; i++)
        { goS1[i].SetActive(false); }
        for (int i = 0; i < goS2.Length; i++)
        { goS2[i].SetActive(false); }
        for (int i = 0; i < goS3.Length; i++)
        { goS3[i].SetActive(false); }
    }
}