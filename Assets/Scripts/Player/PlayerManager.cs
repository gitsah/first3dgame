using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private AnimatorStateInfo stateInfo;
    private Vector3 mousePos;
    private float speed;
    private bool itHasHit;
    //temporarily giving it a default Weapon, will later recieve current Weapon info from the WeaponManager
    private Weapon playerWeapon = new Weapon();

    public Animator animator;
    public AnimationState animationState;
    public Camera mainCam;
    public CharacterController player;
    public GameObject target;

    public float hitpoints { get; set; }
    public float lifeRegen { get; set; }
    public bool inStandingAnim { get; set; }
    public float meleeDamage { get; set; }
    public float attackTime { get; set; }
    public float attackRange { get; set; }

    //deals damage to this object when called
    public void DealDamage(int damage)
    {
        hitpoints -= damage;
    }

    // Use this for initialization
    private void Start()
    {   //using a temp filler weapon here, will later add weapon spawning and pickups to determine these stats for the player
        playerWeapon.atkSpeed = 0.39f;
        playerWeapon.critChance = 0.05f;
        playerWeapon.critDamage = 1.5f;
        playerWeapon.damage = 60;


        attackRange = 2.9f;
        attackTime = playerWeapon.atkSpeed;
        meleeDamage = playerWeapon.damage;
        itHasHit = false;
        hitpoints = 1000;
        lifeRegen = 1;
        mousePos = player.transform.position;
        speed = 4.3f;
	}
	
	// Update is called once per frame
	private void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        inStandingAnim = stateInfo.fullPathHash == ASM.great_sword_slash_2;
        moveCheck();
        lookAtCheck();
        hitpoints += lifeRegen * TimerUtility.DeltaTimer();
        // Debug.Log(hitpoints);
    }
    
    //called once per frame but always after all update methods called
    private void LateUpdate()
    {
        AnimationCheck();
    }

    //calls move methods if mouse is clicked
    private void moveCheck()
    {
        if (!inStandingAnim)
        {
            itHasHit = false;

            if (Input.GetMouseButton(0))
            {
                locatePos();
                moveToPos();
            }
            else
            {
                animationState = AnimationState.IDLE;
            }
        }
    }

    //looks at target if in a locked animation
    private void lookAtCheck()
    {
        //if attack anim playing, turn towards dude and deal damage once per anim
        if (inStandingAnim && target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f * TimerUtility.DeltaTimer());

            if(((stateInfo.normalizedTime % 1f) > attackTime) && !itHasHit && target.GetComponent<MobManager>().inRange(attackRange))
            {
                itHasHit = true;
                target.GetComponent<MobManager>().DealDamage((int)meleeDamage);
            }
        }
    }

    //locate and set the mouse position using a raycast
    private void locatePos()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1200))
        {
            if (hit.collider.tag != "Player")
                mousePos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }

    //move to mouse position and rotate towards it
    private void moveToPos()
    {
        if (Vector3.Distance(transform.position, mousePos) > 0.65f)
        {
            animationState = AnimationState.RUNNING;

            Vector3 camFollow = mainCam.transform.position;
            Quaternion newRotation = Quaternion.LookRotation(mousePos - transform.position);

            //only rotate on y axis
            newRotation.x = newRotation.z = 0f;

            //rotate to the click
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.12f * TimerUtility.DeltaTimer());

            //move to it
            player.SimpleMove(transform.forward * speed * TimerUtility.DeltaTimer());
        }
        else
        {
            animationState = AnimationState.IDLE;
        }
    }

    //checks what the animation state is and sets the appropriate animation to play
    public void AnimationCheck()
    {
        animator.SetBool(ASM.isRunning, false);
        //animator.SetBool("isWalking", false);
        animator.SetBool(ASM.isIdle, false);
        animator.SetBool(ASM.isAttacking, false);

        switch (animationState)
        {
            case AnimationState.ATTACKING:
                animator.SetBool(ASM.isAttacking, true);
                break;
            case AnimationState.IDLE:
                animator.SetBool(ASM.isIdle, true);
                break;
            /*case AnimationState.WALKING:
                animator.SetBool(ASM.isWalking, true);
                break;*/
            case AnimationState.RUNNING:
                animator.SetBool(ASM.isRunning, true);
                break;
            default:
                break;
        }
    }
}
