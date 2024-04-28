using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Profiling;

public class UnitStats : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int maxAP;
    public int currentAP;
    public int carryCapacity;
    [System.NonSerialized] public int salvage;

    public event EventHandler OnTakeDamage;
    public event EventHandler OnDeath;

    PlayerUnit playerUnit;

    private void Start()
    {
        playerUnit = GetComponent<PlayerUnit>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnTakeDamage?.Invoke(this, EventArgs.Empty);
        if (health <= 0) Death();
    }

    public void LoseAP(int cost)
    {
        currentAP -= cost;
        UIManager.instance.UpdateAPUI(playerUnit);
    }

    void Death()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
