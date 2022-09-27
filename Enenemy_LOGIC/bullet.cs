using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject player;
    public Transform object1;
    public GameObject bulletino;
    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(object1.position, transform.forward*3f, Color.green);
        player = GameObject.Find("Player_2(Clone)");
        float singleStep = 20000f * Time.deltaTime;
        Vector3 targetDirection = player.transform.position - object1.transform.position;
        Vector3 newDirection = Vector3.RotateTowards(-transform.up, targetDirection, singleStep, 0.0f);
        var rotation = Quaternion.LookRotation(newDirection);
        object1.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
        object1.position += object1.transform.forward * 0.5f;
        if (Physics.Raycast(object1.position, transform.forward * 3f, out hit,0.5f))
        {
            if (hit.collider.tag == "Player")
            {
                Debug.Log("workBulet");
                Destroy(bulletino);
                player.GetComponent<characterStat>().Damage(30);
            }
            if (hit.collider.tag == "dd" || hit.collider.tag == "FloorCordr" || hit.collider.tag == "RoofCordr" || hit.collider.tag == "WallCordr" || hit.collider.tag == "FloorRoom" || hit.collider.tag == "RoofRoom" || hit.collider.tag == "WallRoom")
            {
                Debug.Log("destroyedBulet");
                Destroy(bulletino);
            }

            if (hit.collider.tag != "Player")
            {
                Debug.Log("dontBulet");
                
            }
        }

    }
}
