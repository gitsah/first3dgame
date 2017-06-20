using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to keep track of current animation state
public enum AnimationState {
    ATTACKING,
    IDLE,
    RUNNING,
    WALKING 
}

//classes for animation state relative stuff
public class ASM
{
    //Animation state hashes
    public static int
        isAttacking = Animator.StringToHash("isAttacking"),
        isRunning = Animator.StringToHash("isRunning"),
        isWalking = Animator.StringToHash("isWalking"),
        isIdle = Animator.StringToHash("isIdle");

    //Attack state hashes
    public static int
        great_sword_slash_2 = Animator.StringToHash("Base.great_sword_slash_2"),
        Attack = Animator.StringToHash("Base.Attack");




//    public static void AnimationCheck(Animator animator, AnimationState animationState)
//    {
//            animator.SetBool("isAttacking", false);
//            animator.SetBool("isWalking", false);
//            animator.SetBool("isIdle", false);
//            animator.SetBool("isRunning", false);

//            switch (animationState)
//            {
//                case AnimationState.ATTACKING:
//                    animator.SetBool("isAttacking", true);
//                    break;
//                case AnimationState.IDLE:
//                    animator.SetBool("isIdle", true);
//                    break;
//                /*case AnimationState.WALKING:
//                    animator.SetBool("isWalking", true);
//                    break;*/
//                case AnimationState.RUNNING:
//                    animator.SetBool("isRunning", true);
//                    break;
//                default:
//                    break;
//            }
//        }
    }
