using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Color rollColor;
    [SerializeField] Color actionColor;
    [SerializeField] Color deathColor;

    PlayerController player;
    SpriteRenderer spriteRenderer;

    PlayerState previousState;
    Color defaultColor;

    void Awake()
    {
        player = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        defaultColor = spriteRenderer.color;
        previousState = player.CurrentState;
    }
    void Update()
    {
        switch (player.CurrentState)
        {
            case PlayerState.Idle: UpdateIdle(); break;
            case PlayerState.Roll: UpdateRoll(); break;
            case PlayerState.Action: UpdateAction(); break;
            case PlayerState.Death: UpdateDeath(); break;
        }
    }


    void UpdateIdle()
    {
        if (previousState != PlayerState.Idle)
        {
            previousState = PlayerState.Idle;

            //temp code
            spriteRenderer.color = defaultColor;
        }
    }
    void UpdateRoll()
    {
        if (previousState != PlayerState.Roll)
        {
            previousState = PlayerState.Roll;

            //temp code
            spriteRenderer.color = rollColor;
        }
    }
    void UpdateAction()
    {
        if (previousState != PlayerState.Action)
        {
            previousState = PlayerState.Action;

            //temp code
            spriteRenderer.color = actionColor;
        }
    }
    void UpdateDeath()
    {
        if (previousState != PlayerState.Death)
        {
            previousState = PlayerState.Death;

            //temp code
            spriteRenderer.color = deathColor;
        }
    }
}
