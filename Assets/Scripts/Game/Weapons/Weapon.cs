using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used to create weapons, more stats will be added as needed
public class Weapon {
    public float damage;
    public float atkSpeed;
    public float critChance;
    public float critDamage;

    //allows easy creation of weapons with unique stats
    public Weapon(float setDamage, float setAtkSpeed, float setCritChance, float setCritDamage)
    {
        damage = setDamage;
        atkSpeed = setAtkSpeed;
        critChance = setCritChance;
        critDamage = setCritDamage;
    }
    
    //deault stats for starter weapon
    public Weapon()
    {
        damage = 60;
        atkSpeed = 0.39f;
        critChance = 0.05f;
        critDamage = 1.5f;
    }

}
