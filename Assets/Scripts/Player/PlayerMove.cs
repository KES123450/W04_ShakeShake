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
    Rigidbody2D playerRigidbody;

    Vector2 moveDirection;
    Vector2 rollDirection;
    float rollStartTime;

    public bool CanMove { get; set; }
    public bool IsRollEnded { get; private set; }
    bool IsRolling { get { return player.CurrentState == PlayerState.Roll; } }


    private void Awake()
    {
        player = GetComponent<PlayerController>();
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
        playerRigidbody.velocity = Vector2.zero;
    }
    void FixedUpdateDeath()
    {
    }

    public void SetDireciton(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void StartRoll()
    {
        rollStartTime = Time.time;
        rollDirection = moveDirection;

        var rollSpeed = rollDistance / rollDuration;
        playerRigidbody.velocity = rollDirection.normalized * (rollSpeed);
    }
    public void EndRoll()
    {
        if (!IsRolling) { return; }
        IsRollEnded = false;
    }

}
