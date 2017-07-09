using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {

    private float spawnTime;
    private float counter;

    public GameObject thisMob;

    // Use this for initialization
    void Start () {
        //spawn a unit initially, set counter and spawnTime values
        spawnUnit();
        counter = 0;
        spawnTime = 1050f;
	}
	
	// Update is called once per frame
	void Update () {
        //checks if the counter is past the spawnTime to spawn a unit
		if (counter > spawnTime * TimerUtility.DeltaTimer())
        {
            spawnUnit();
            counter = 0;
        }

        counter++;

	}

    //spawns a unit when called
    private void spawnUnit()
    {
        //was using this to set spwan location, might use later but for now ill just stick to the spawner location
        //float x = gameObject.transform.position.x;
        //float y = gameObject.transform.position.x;
        //float z = gameObject.transform.position.z;
        
        GameObject.Instantiate(thisMob, gameObject.transform.position, transform.rotation);
    }
}
