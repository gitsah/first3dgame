using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//melee fighting stuff
public class CombatManager : MonoBehaviour {
    public GameObject TargetEnemy;
    private PlayerManager playerManager;
    public bool updateTarget;

	// Use this for initialization
	void Start ()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();
        updateTarget = false;
	}
	
	// Update is called once per frame
	void Update () {
        //updates target to be same as attack target
        if (updateTarget)
        {
            playerManager.target = TargetEnemy;
            updateTarget = false;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            playerManager.animationState = AnimationState.ATTACKING;

        }
        else if (Input.GetKeyUp(KeyCode.Space))
            playerManager.animationState = AnimationState.IDLE;
    }
}
