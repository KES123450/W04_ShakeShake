using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float speed;
    [Header("Roll")]
    [SerializeField] float rollDistance;
    [SerializeField] float rollDuration;

    PlayerController player;
    PlayerAction action;
    Rigidbody2D playerRigidbody;

    Vector2 moveDirection;
    Vector2 rollDirection;
    float rollStartTime;

    public bool CanMove { get; set; }
    public bool IsRollEnded { get; private set; }
    public bool IsMoving => playerRigidbody.velocity.magnitude > 0;
    bool IsRolling { get { return player.CurrentState == PlayerState.Roll; } }


    private void Awake()
    {
        player = GetComponent<PlayerController>();
        action = GetComponent<PlayerAction>();
        playerRigidbody = GetComponent<Rigidbody2D>();    
    }
    private void Start()
    {
        IsRollEnded = false;
    }
    private void FixedUpdate()
    {
        if (!CanMove)
        {
            return;
        }

        switch (player.CurrentState)
        {
            case PlayerState.Idle: FixedUpdateIdle(); break;
            case PlayerState.Roll: FixedUpdateRoll(); break;
            case PlayerState.Action: FixedUpdateAction(); break;
            case PlayerState.Death: FixedUpdateDeath(); break;
        }
    }
    void FixedUpdateIdle()
    {
        playerRigidbody.velocity = moveDirection * speed;
    }
    void FixedUpdateRoll()
    {
        if (Time.time > rollStartTime + rollDuration)
        {
            IsRollEnded = true;
        }
    }
    void FixedUpdateAction()
    {
        var muliplier = action.CurrentAction.SpeedMultiplier;
        playerRigidbody.velocity = moveDirection * speed * muliplier;
    }
    void FixedUpdateDeath()
    {
    }
    public void SetDireciton(Vector2 direction)
    {
        moveDirection = direction;
        if (!direction.Equals(Vector2.zero)) { rollDirection = direction; }
    }

    public void StartRoll(Vector2 _direction)
    {
        rollStartTime = Time.time;
        var direction = (_direction.Equals(Vector2.zero) ? rollDirection : _direction).normalized;
        var rollSpeed = rollDistance / rollDuration;
        playerRigidbody.velocity = direction * (rollSpeed);
    }
    public void EndRoll()
    {
        if (!IsRolling) { return; }
        IsRollEnded = false;
    }

}
