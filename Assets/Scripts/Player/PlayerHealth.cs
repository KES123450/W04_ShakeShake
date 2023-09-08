using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth;

    PlayerController player;

    int _currentHealth;

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { 
            _currentHealth = value;
            if (_currentHealth < 0) _currentHealth = 0;
            else if (_currentHealth > maxHealth) _currentHealth = maxHealth;
        }
    }
    public bool IsAlive => _currentHealth > 0;

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }
    void Start()
    {
        CurrentHealth = maxHealth;
    }
    public void OnDamage(int value = 1)
    {
        CurrentHealth -= value;
    }
}
