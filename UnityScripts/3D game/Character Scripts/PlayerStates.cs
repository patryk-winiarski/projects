using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class CharBaseState
{
    public abstract void EnterState(CharController player);
    public abstract void Update(CharController player);
}

public class CharIdleState : CharBaseState
{
    public override void EnterState(CharController player)
    {
        Animator anim = player.Animator;
        NavMeshAgent agent = player.NavMeshAgent;
        agent.isStopped = true;
        anim.SetFloat("blendSpeed", 0);
        anim.SetLayerWeight(1, 0f); // set attack layer to 0 if player is idle
        Debug.Log("Entering idle state");

    }

    public override void Update(CharController player)
    {
        // If a movement key is pressed, transition to move state
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            player.TransitionToState(player.MoveState);
        }

        // If the area is valid and the LMB is clicked, transition to attack state
        // Add the area check later cause idk how to do it rn. Probably set it through the game manager?

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            player.TransitionToState(player.AttackState);
        }
    }
}

public class CharMoveState : CharBaseState
{
    #region locomotion fields
    private float moveSpeed;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    #endregion

    public override void EnterState(CharController player)
    {
        Animator anim = player.Animator;
        anim.SetLayerWeight(1, 0f); // set attack layer to 0 if player is only moving
        Debug.Log("Starting to move");
    }

    public override void Update(CharController player)
    {
        NavMeshAgent agent = player.NavMeshAgent;
        Animator anim = player.Animator;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveSpeed = agent.speed;

        moveInput = new Vector3(horizontal, 0f, vertical);
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;

        Quaternion cameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, cameraForward);
        Vector3 lookToward = cameraRelativeRotation * moveInput;

        if (moveInput.sqrMagnitude > 0)
        {
            Ray lookRay = new Ray(player.transform.position, lookToward);
            player.transform.LookAt(lookRay.GetPoint(1));
        }

        moveVelocity = player.transform.forward * moveSpeed * moveInput.sqrMagnitude;
        agent.velocity = moveVelocity;
        MovementAnimation(anim, agent);

        // If no keys are being pressed, transition to idle
        if (moveVelocity.x == 0 && moveVelocity.y == 0 && moveVelocity.z == 0)
        {
            player.TransitionToState(player.IdleState);
        }

        // If the area is valid and the LMB is clicked, transition to attack state
        // Add the area check later cause idk how to do it rn. Probably set it through the game manager?

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            player.TransitionToState(player.AttackState);
        }

    }

    void MovementAnimation(Animator anim, NavMeshAgent agent)
    {
        anim.SetFloat("blendSpeed", agent.velocity.magnitude);
    }
}

public class CharAttackState : CharBaseState
{
    //Countdown variable for transitioning back to idle/move state after an attack
    int attackCountdown;

    public override void EnterState(CharController player)
    {
        Animator anim = player.Animator;
        NavMeshAgent agent = player.NavMeshAgent;
        Rigidbody rb = player.GetComponent<Rigidbody>();

        rb.sleepThreshold = 0.0f;

        // Stop the character for the duration of the attack and switch to attack animation layer.
        agent.isStopped = true;
        anim.SetLayerWeight(1, 1f);
        anim.SetTrigger("IsAttacking");
        Debug.Log("Entering attack state");

        attackCountdown = 10000; // 10s
    }

    public void OnCollisionStay(CharController player, Collision collision)
    {
        //Check if key pressed and if the colliding object is attackable, fire the attack method
    }

    public override void Update(CharController player)
    {
        NavMeshAgent agent = player.NavMeshAgent;
        Animator anim = player.Animator;
        Rigidbody rb = player.GetComponent<Rigidbody>();

        var moveVelocity = agent.velocity;

        // Remove from countdown each frame.
        attackCountdown--;

        // If the attack animation has finished playing and a key pressed, transition to move state immediately
        if (!anim.GetCurrentAnimatorStateInfo(1).IsName("anim_attack_1"))
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                player.TransitionToState(player.MoveState);
            }
        }

        // Countdown == 0
        if (attackCountdown == 0)
        {
            Debug.LogError("Attack Countdown is 0!");
            // If no keys are being pressed, transition to idle
            if (moveVelocity.x == 0 && moveVelocity.y == 0 && moveVelocity.z == 0)
            {
                player.TransitionToState(player.IdleState);
            }
        }

        // Reset countdown if attack button pressed again
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("IsAttacking");

            if (rb.IsSleeping())
            {
                rb.WakeUp();
            }

            attackCountdown = 10000; // 10s
        }

    }

    public void AttackTarget(GameObject target, CharController player)
    {
        CharacterStats stats = player.GetComponent<CharacterStats>();
        // Variable to hold an example attack scriptable object
        AttackDefinition demoAttack = player.GetComponent<CharController>().demoAttack;

        var attack = demoAttack.CreateAttack(stats, target.GetComponent<CharacterStats>());

        //Get all of the IAttackable scripts attached to the target
        var attackables = target.GetComponentsInChildren(typeof(IAttackable));

        foreach (IAttackable attackable in attackables)
        {
            // Fire an attack for each attackable script
            attackable.OnAttack(player.gameObject, attack);
        }
    }
}