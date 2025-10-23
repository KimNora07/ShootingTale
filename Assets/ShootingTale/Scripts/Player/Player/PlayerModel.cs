using System;
using UnityEngine;

public class PlayerModel
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    
    public int currentAtk { get; set; }

    public int[] PlayerLowAtk { get; set; }
    public int[] PlayerMediumAtk { get; set; }
    public int[] PlayerHighAtk { get; set; }
    
    public event Action<int> OnHealthChanged;

    public PlayerModel(int maxHealth)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= Mathf.Max(0, Health - damage);
        OnHealthChanged?.Invoke(Health);
    }
}
