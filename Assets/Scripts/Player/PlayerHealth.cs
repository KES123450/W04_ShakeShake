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
        set { _currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }
    public bool IsAlive => _currentHealth > 0;
    
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
