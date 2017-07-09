using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour {
    
    private Weapon thisWeapon;
    private float swapCooldown;

    public GameObject player;
    bool canSwap;

    // Use this for initialization
    void Start () {
        //create a weapon for this box
        //TODO: randomize values within a range eventually
        thisWeapon = new Weapon(80, 0.5f, 0.15f, 1.8f);
        swapCooldown = 0;
        canSwap = true;

    }
	
	// Update is called once per frame
	void Update () {
        //switches the weapon if in range of the box and button is pressed down (down only to avoid rapid switching)
        if (inPickupRange() )
        {
            if (Input.GetKeyDown(KeyCode.F))
                swapWeapons();
        }
	}

    //checks if box is close enough to the player
    private bool inPickupRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            return true;
        }
        else
            return false;
    }
    
    //swaps the player's currently equiped weapon with the one in the box
    private void swapWeapons()
    {
        Weapon tempWeapon = PlayerManager.playerWeapon;
        PlayerManager.playerWeapon = thisWeapon;
        thisWeapon = tempWeapon;
    }
}
