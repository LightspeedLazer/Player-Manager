using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    public float HealPercent;

    void OnCollisionEnter(Collision col)
    {
        Character character = col.gameObject.GetComponent<Character>();
        print("help please");
        if(character != null && character.Heal(new Health(character.MaxHealth * HealPercent,Health.HealSource.MedKit,0,false)) > 0) {Destroy(gameObject);}
    }
}
