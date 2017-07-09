using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : MobManager {

	// Use this for initialization
	private void Start()
    {
        animationState = AnimationState.IDLE;
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        isAttacking = false;
        itHasHit = false;
        maxHP = 1000;
        hitpoints = maxHP;
        lifeRegen = 0.02f;
        sightRange = 11.5f;
        meleeRange = 2.62f;
        speed = 0.042f;
        meleeAttackHigh = 0.392f;
        meleeAttackLow = 0.352f;
        meleeDamage = 60;

        AnimationCheck();
    }
    
    // Update is called once per frame
    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        isAttacking = stateInfo.fullPathHash == ASM.Attack;
        moveTowardsPlayer();
        meleeAttack();
        AnimationCheck();

        if(hitpoints < maxHP)
            regenLife();

        //Debug.Log(hitpoints);
    }
}
