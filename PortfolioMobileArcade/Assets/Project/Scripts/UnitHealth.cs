using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth
{
    private int _currentHealth;
    private int _maxHealth;

    //Properties
    public int Health
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
        }
    }
    
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    
    //Constructor
    public UnitHealth(int health, int maxHealth)
    {
        _currentHealth = health;
        _maxHealth = maxHealth;
    }
    
    //Functions
    public void TakeDamage(int damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
        }
    }

    public void Heal(int healAmount)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += healAmount;
        }
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

}