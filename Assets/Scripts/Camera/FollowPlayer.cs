using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make camera follow the player
public class FollowPlayer : MonoBehaviour {
    public GameObject player;
    Vector3 cam;
    float height = 20.87508f;

	// Use this for initialization
	void Start()
    {
        cam = player.transform.position;	
	}
	
	// Update is called once per frame
	void Update()
    {
        cam.x = player.transform.position.x + 16.0746f;
        cam.z = player.transform.position.z - 12.82303f;
        cam.y = height;

        transform.position = cam;
	}
}
