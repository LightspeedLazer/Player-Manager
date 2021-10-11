using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public GameObject Projectile;
    public float AttackSpeed;
    public float CooldownTimer;

    public void Cooldown(float deltaTime)
    {
        CooldownTimer += Mathf.Min(Mathf.Pow(AttackSpeed,-1)*deltaTime,1-CooldownTimer);
    }
    
    public void Activate()
    {
        if (CooldownTimer == 1)
        {
            CooldownTimer = 0;
            
        }
    }
}
