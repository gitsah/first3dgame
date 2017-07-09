using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used to create weapons, more stats will be added as needed
public class Weapon {
    public float damage;
    public float atkSpeed;
    public float critChance;
    public float critDamage;


    public Weapon(float setDamage, float setAtkSpeed, float setCritChance, float setCritDamage)
    {
        damage = setDamage;
        atkSpeed = setAtkSpeed;
        critChance = setCritChance;
        critDamage = setCritDamage;
    }

}
