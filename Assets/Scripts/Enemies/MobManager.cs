using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    //Subclass fields
    protected AnimationState animationState;
    protected bool isAttacking;
    protected bool itHasHit;
    protected float lifeRegen;
    protected float meleeAttackHigh;
    protected float meleeAttackLow;
    protected float meleeDamage;
    protected float meleeRange;
    protected float sightRange;
    protected float speed;
    protected AnimatorStateInfo stateInfo;
    public float hitpoints { get; set; }

    public CharacterController controller;
    public Animator animator;
    //protected Transform playerTransform = controller.transform;

    protected PlayerManager player;

    //can be called to deal damage to this unit
    public void DealDamage(int damage)
    {
        hitpoints -= damage;
        if (hitpoints <= 0)
        {
            hitpoints = 0;
            Destroy(gameObject);
        }
    }

    //returns true if player is within range specified
    public bool inRange(float range)
    {
        return Vector3.Distance(transform.position, controller.transform.position) < range;
    }

    protected void regenLife()
    {
        hitpoints += lifeRegen * TimerUtility.DeltaTimer();
    }

    //looks at and moves towards the player if they're within site range specified
    protected void moveTowardsPlayer()
    {
        if (inRange(sightRange))
        {
            Vector3 direction = controller.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.065f * TimerUtility.DeltaTimer());

            if (inRange(sightRange - 2))
            {
                if (!isAttacking)
                {
                    itHasHit = false;
                    animationState = AnimationState.WALKING;
                    transform.Translate(0, 0, speed * TimerUtility.DeltaTimer());
                }
            }
            else
            {
                animationState = AnimationState.IDLE;
            }
        }
        else
        {
            animationState = AnimationState.IDLE;
        }
    }

    //deals damage if in range of player for specified time
    protected void meleeAttack()
    {
        if (inRange(meleeRange))
        {
            animationState = AnimationState.ATTACKING;

            if (((stateInfo.normalizedTime % 1f) > meleeAttackLow) && ((stateInfo.normalizedTime % 1f) < meleeAttackHigh) && !itHasHit)
            {
                itHasHit = true;
                player.DealDamage((int)meleeDamage);
            }
        }
        else if (animationState != AnimationState.IDLE)
        {
            animationState = AnimationState.WALKING;
        }
    }

    //does stuff if mousing over
    protected void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.GetComponent<CombatManager>().TargetEnemy = gameObject;
            player.GetComponent<CombatManager>().updateTarget = true;
        }
    }

    //checks what the animation state is and sets the appropriate animation to play
    protected void AnimationCheck()
    {
        animator.SetBool(ASM.isAttacking, false);
        animator.SetBool(ASM.isWalking, false);
        animator.SetBool(ASM.isIdle, false);
        //animator.SetBool(ASM.isRunning, false);

        switch (animationState)
        {
            case AnimationState.ATTACKING:
                animator.SetBool(ASM.isAttacking, true);
                break;
            case AnimationState.IDLE:
                animator.SetBool(ASM.isIdle, true);
                break;
            case AnimationState.WALKING:
                animator.SetBool(ASM.isWalking, true);
                break;
            case AnimationState.RUNNING:
                animator.SetBool(ASM.isRunning, true);
                break;
            default:
                break;
        }
    }
}
