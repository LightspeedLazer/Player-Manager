using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Console console;
    public int Health;
    public int MaxHealth;
    public int OverhealAmount;
    public float OverhealDecay = 3;
    private float OverhealDecaycounter;
    public List<Modifier> ModifierList = new List<Modifier>();

    void Start()
    {
        //ModifierList.Add(new DamageModifier(1-0.25f,global::Damage.DamageType.Explosion));
        console.runCommand.AddListener(delegate
        {
            switch (console.command)
            {
                case "damage":
                    console.Log(new LogEntry(Damage(new Damage(100,global::Damage.DamageType.Explosion,false,false)).ToString()));
                break;
                case "jarate":
                    ModifierList.Add(new StatusEffect(StatusEffect.StatusEffectType.Jarate));
                break;
            }
        });
        MaxHealth = 125;
        OverhealAmount = 60;
        Health = MaxHealth;
    }

    void Update()
    {
        if (Health > MaxHealth)
        {
            OverhealDecaycounter += OverhealDecay * Time.deltaTime;
            Health -= Mathf.FloorToInt(OverhealDecaycounter);
            OverhealDecaycounter -= Mathf.Floor(OverhealDecaycounter);
        }
        else 
        {
            OverhealDecaycounter = 0;
        }
    }

    public Damage Damage(Damage damage)
    {
        foreach (Modifier modifier in ModifierList)
        {
            if (modifier is StatusEffect)
            {
                StatusEffect m = modifier as StatusEffect;
                if (m.type == StatusEffect.StatusEffectType.Jarate || m.type == StatusEffect.StatusEffectType.MarkedForDeath)
                {
                    damage.miniCrit = true;
                }
            }
        }
        Health -= EvaluateDamage(damage);
        return damage;
    }
    
    public Damage TestDamage(Damage damage)
    {
        foreach (Modifier modifier in ModifierList)
        {
            if (modifier is StatusEffect)
            {
                StatusEffect m = modifier as StatusEffect;
                if (m.type == StatusEffect.StatusEffectType.Jarate || m.type == StatusEffect.StatusEffectType.MarkedForDeath)
                {
                    damage.miniCrit = true;
                }
            }
        }
        return damage;
    }

    public int EvaluateDamage(Damage damage)
    {
        float modifiedDamage = damage.baseDamage * TotalDamageModifier(damage.damageType);
        Damage.DamageType critType = global::Damage.DamageType.None;
        if (damage.crit)            {critType = global::Damage.DamageType.Crit;}
        else if (damage.miniCrit)   {critType = global::Damage.DamageType.MiniCrit;}
        float critDamage = modifiedDamage * TotalDamageModifier(critType);
        int finalDamage = Mathf.RoundToInt(modifiedDamage+critDamage);
        return finalDamage;
    }
    
    float TotalDamageModifier(Damage.DamageType damageType)
    {
        float r;
        switch (damageType)
        {                                                       //Determines the initial return value based on crit data
            default:
                r = 1f;
            break;
            case global::Damage.DamageType.Crit:
                r = 2f;
            break;
            case global::Damage.DamageType.MiniCrit:
                r = 0.35f;
            break;
            case global::Damage.DamageType.None:
                r = 0;
            break;
        }
        foreach (Modifier modifier in ModifierList)             //Searches for relevent damage types and applies the modifiers to the return value
        {
            if (modifier is DamageModifier)
            {
                DamageModifier m = modifier as DamageModifier;  //Applies the All damage type to all but crits
                if (m.damageType == damageType || (m.damageType == global::Damage.DamageType.All && !(m.damageType == global::Damage.DamageType.Crit || m.damageType == global::Damage.DamageType.MiniCrit)))
                {
                    r *= m.modifier;
                }
            }
        }
        return r;
    }

    public int Heal(Health health)
    {
        float modifiedHeal = health.baseHeal * TotalHealModifier(health.healSource);
        int finalHeal = Mathf.RoundToInt(Mathf.Min(modifiedHeal,(OverhealAmount * health.overhealPercent)+MaxHealth-Health));
        Health += finalHeal;
        return finalHeal;
    }

    public int TestHeal(Health health)
    {
        float modifiedHeal = health.baseHeal * TotalHealModifier(health.healSource);
        int finalHeal = Mathf.RoundToInt(modifiedHeal);
        return finalHeal;
    }

    float TotalHealModifier(Health.HealSource healSource)
    {
        float r = 1;
        foreach (Modifier modifier in ModifierList)
        {
            if (modifier is HealModifier)
            {
                HealModifier m = modifier as HealModifier;
                if (m.healSource == healSource)
                {
                    r *= m.modifier;
                }
            }
        }
        return r;
    }
}

public struct Damage
{
    public int baseDamage;
    public bool miniCrit;
    public bool crit;
    public enum DamageType {Bullet, Explosion, Fire, Melee, MiniCrit, Crit, None, All}
    public DamageType damageType;

    public Damage(int baseDamage, DamageType damageType, bool miniCrit, bool crit)
    {
        this.baseDamage = baseDamage;
        this.damageType = damageType;
        this.miniCrit = miniCrit;
        this.crit = crit;
    }
}

public struct Health
{
    public float baseHeal;
    public float overhealPercent;
    public bool critHeal;
    public enum HealSource {MedKit, Medigun, DispenserBeam, HealingBolt}
    public HealSource healSource;

    public Health(float baseHeal, HealSource healSource, float overhealPercent, bool critHeal)
    {
        this.baseHeal = baseHeal;
        this.healSource = healSource;
        this.overhealPercent = overhealPercent;
        this.critHeal = critHeal;
    }
}

public class Modifier
{

}

public class StatusEffect : Modifier
{
    public enum StatusEffectType {Milk, Bleed, Jarate, MarkedForDeath, MiniCritBoosted, CritBoosted}
    public StatusEffectType type;

    public StatusEffect(StatusEffectType type)
    {
        this.type = type;
    }
}

public class DamageModifier : Modifier
{
    public Damage.DamageType damageType;
    public float modifier;

    public DamageModifier(float modifier, Damage.DamageType damageType)
    {
        this.modifier = modifier;
        this.damageType = damageType;
    }
}

public class HealModifier : Modifier
{
    public Health.HealSource healSource;
    public float modifier;
}

public class MaxHealthModifier : Modifier
{

}
