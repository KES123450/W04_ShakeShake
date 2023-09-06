using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState { Idle, Roll, Action, Death }
public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    [SerializeField] float rollInputBuffer;

    PlayerInputActions inputs;
    PlayerMove playerMove;

    Vector2 inputDirection;
    bool desiredRoll;
    float rollInputBufferCounter;
    public PlayerState CurrentState { get; private set; }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            desiredRoll = true;
            rollInputBufferCounter = rollInputBuffer;
        }
    }

    void Awake()
    {
        inputs = new();
        inputs.Player.SetCallbacks(this);
        inputs.Enable();

        playerMove = GetComponent<PlayerMove>();
    }

    void Start()
    {
        playerMove.CanMove = true;
        CurrentState = PlayerState.Idle;
    }

    void Update()
    {
        if (desiredRoll && rollInputBufferCounter > 0)
        {
            rollInputBufferCounter -= Time.deltaTime;
            if (rollInputBufferCounter < 0)
            {
                desiredRoll = false;
            }
        }
    }
    void FixedUpdate()
    {
        switch (CurrentState)
        {
            case PlayerState.Idle: FixedUpdateIdle(); break;
            case PlayerState.Roll: FixedUpdateRoll(); break;
            case PlayerState.Action: FixedUpdateAction(); break;
            case PlayerState.Death: FixedUpdateDeath(); break;
        }
    }

    void FixedUpdateIdle()
    {
        playerMove.SetDireciton(inputDirection);

        if (desiredRoll)
        {
            CurrentState = PlayerState.Roll;
            playerMove.StartRoll();
            desiredRoll = false;
        }
    }
    void FixedUpdateRoll()
    {
        if (playerMove.IsRollEnded)
        {
            CurrentState = PlayerState.Idle;
            playerMove.EndRoll();
        }
    }
    void FixedUpdateAction()
    {

    }

    void FixedUpdateDeath()
    {
        playerMove.CanMove = false;
    }
}
