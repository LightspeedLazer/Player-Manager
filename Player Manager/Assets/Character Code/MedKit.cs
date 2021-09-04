using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    public float HealPercent;

    void OnTriggerEnter(Collision col)
    {
        Entity character = col.gameObject.GetComponent<Entity>();
        if(character != null && character.Heal(new Health(character.MaxHealth * HealPercent,Health.HealSource.MedKit,0.5f,false)) > 0) {Destroy(gameObject);}
    }
}
