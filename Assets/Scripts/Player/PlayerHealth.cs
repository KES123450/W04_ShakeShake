using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth;
    [SerializeField] float invunerableDuration;
    PlayerController player;
    PlayerAnimation playerAnimation;

    int _currentHealth;
    float invunerableTimer;


    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }
    public bool IsAlive => _currentHealth > 0;
    public bool IsInvunerable => invunerableTimer > 0;
    
    void OnGUI()
    {
        var style = new GUIStyle();
        style.fontSize = 100;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(0, Screen.height / 10, Screen.width / 2, 2 * Screen.height / 10), $"Player : {CurrentHealth} / {maxHealth}", style);
    }

    void Awake()
    {
        player = GetComponent<PlayerController>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    void Start()
    {
        CurrentHealth = maxHealth;
    }
    void Update()
    {
        if (IsAlive && IsInvunerable)
        {
            invunerableTimer -= Time.deltaTime;
            if (!IsInvunerable)
            {
                EndInvunerable();
            }
        }
    }
    public void OnDamage(int value = 1)
    {
        if (IsAlive && !IsInvunerable)
        {
            CurrentHealth -= value;
            if (IsAlive)
            {
                StartInvunerable();
            }
        }
    }

    void StartInvunerable()
    {
        invunerableTimer = invunerableDuration;
        playerAnimation.OnStartInvunerable(invunerableDuration);
    }

    void EndInvunerable()
    {
        playerAnimation.OnEndInvunerable();
    }
}
