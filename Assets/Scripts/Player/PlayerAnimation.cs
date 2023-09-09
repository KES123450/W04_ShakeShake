using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Color rollColor;
    [SerializeField] Color actionColor;
    [SerializeField] Color deathColor;
    [Header("Invunerable Color")]
    [SerializeField] Color invunerableColor;
    [SerializeField] int invunerableBlinkNum;
    [SerializeField] Ease invunerableBlinkEaseFunction;

    PlayerController player;
    SpriteRenderer spriteRenderer;
    Animator animator;

    
    PlayerState previousState;
    Color defaultColor;
    bool isInvunerable;
    float invunerableDuration;
    float invunerableTimer;


    void Awake()
    {
        player = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        defaultColor = spriteRenderer.color;
        previousState = player.CurrentState;
        isInvunerable = false;
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
        if (isInvunerable)
        {
            UpdateInvunerableColor();
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

    public void OnStartInvunerable(float effectDuration)
    {
        isInvunerable = true;
        invunerableDuration = effectDuration;
        invunerableTimer = invunerableDuration;
    }
    public void OnEndInvunerable()
    {
        isInvunerable = false;
        spriteRenderer.color = defaultColor;
    }
    public void UpdateInvunerableColor()
    {
        invunerableTimer -= Time.deltaTime;

        var blinkDuration = invunerableDuration / invunerableBlinkNum;
        var interpolatedValue = ((invunerableDuration - invunerableTimer) % blinkDuration) / blinkDuration;
        if (interpolatedValue > 0.5f)
        {
            interpolatedValue = 1 - interpolatedValue;
        }
        var easedValue = DOVirtual.EasedValue(0, 1, interpolatedValue * 2, invunerableBlinkEaseFunction);
        spriteRenderer.color = Color.Lerp(defaultColor, invunerableColor, easedValue);

    }
}
