using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState { Idle, Roll, Action, Death }
public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    [SerializeField] float rollInputBuffer;
    [SerializeField] float actionInputBuffer;

    PlayerInputActions inputs;
    PlayerMove playerMove;
    PlayerAction playerAction;

    Vector2 inputDirection;
    bool desiredRoll;
    bool desiredAction;
    float rollInputBufferCounter;
    float actionInputBufferCounter;
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

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            desiredAction = true;
            actionInputBufferCounter = actionInputBuffer;
        }

    }

    void Awake()
    {
        inputs = new();
        inputs.Player.SetCallbacks(this);
        inputs.Enable();

        playerMove = GetComponent<PlayerMove>();
        playerAction = GetComponent<PlayerAction>();
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
        if (desiredAction && actionInputBufferCounter > 0)
        {
            actionInputBufferCounter -= Time.deltaTime;
            if (actionInputBufferCounter < 0)
            {
                desiredAction = false;
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
        if (rollInputBuffer == 0) desiredRoll = false;
        if (actionInputBuffer == 0) desiredAction = false;
    }

    void FixedUpdateIdle()
    {
        playerMove.SetDireciton(inputDirection);

        TryRoll();
        TryAction();
    }
    void FixedUpdateRoll()
    {
        if (playerMove.IsRollEnded)
        {
            playerMove.EndRoll();
            CurrentState = PlayerState.Idle;
        }
        if (playerAction.CanActionCancelRoll)
        {
            playerMove.EndRoll();
            TryAction();
        }
    }
    void FixedUpdateAction()
    {
        if (playerAction.IsActionEnded)
        {
            playerAction.EndAction();
            CurrentState = PlayerState.Idle;
        }
        if (playerAction.CanRollCancelAction)
        {
            playerAction.EndAction();
            TryRoll();
        }
    }

    void FixedUpdateDeath()
    {
        playerMove.CanMove = false;
    }

    bool TryRoll()
    {
        if (desiredRoll)
        {
            playerMove.StartRoll(inputDirection);
            CurrentState = PlayerState.Roll;
            desiredRoll = false;
            return true;
        }
        return false;
    }
    bool TryAction()
    {
        if (desiredAction)
        {
            playerAction.StartAction();
            CurrentState = PlayerState.Action;
            desiredAction = false;
            return true;
        }
        return false;
    }
}
