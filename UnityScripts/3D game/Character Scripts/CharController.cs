using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharController : MonoBehaviour
{

    //The character controller is the central part reponsible for setting up a character within the system.
    //It implements a simple finite-state machine that has the character switching bettween a couple states depending on the circumstances.
    //The character can only be in one state at a time.
    //The currently implemented states are: idle and move.
    //The attack state is partially implemented.

    #region fields

    #region component fields
    private Animator anim;
    public Animator Animator
    {
        get { return anim; }
    }
    private NavMeshAgent agent;
    public NavMeshAgent NavMeshAgent
    {
        get { return agent; }
    }

    private CharacterStats stats;
    public AttackDefinition demoAttack;

    #endregion

    #region state fields
    // Character state initialization
    private CharBaseState currentState;
    public CharBaseState CurrentState
    {
        get { return currentState; }
    }

    public readonly CharIdleState IdleState = new CharIdleState();
    public readonly CharAttackState AttackState = new CharAttackState();
    public readonly CharMoveState MoveState = new CharMoveState();
    // Dash state
    // Jumping state
    // Talking state?

    #endregion

    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        TransitionToState(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update(this);
    }


    #region state methods
    public void TransitionToState(CharBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (currentState == AttackState)
        {
            AttackState.OnCollisionStay(this, collision);
        }
    }
    #endregion
}
