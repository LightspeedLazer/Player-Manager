using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloon
{
    public enum BloonTypes {Red,Blue,Green,Yellow,Pink,Purple,Black,White,Zebra,Lead,Rainbow,Ceramic,MOAB,BFB,ZOMG,BAD,DDT}
    public List<Bloon> Spawns = new List<Bloon>();
    public List<Bloon> RegrowPath = new List<Bloon>();
    public int MaxHealth;
    public int Health;
    public float speed;
    public bool camo;
    public bool regen;

    public List<Bloon> Damage(int damage)
    {
        List<Bloon> r = null;
        Health -= Mathf.Max(damage,0);
        if (Health <= 0)
        {
            r = new List<Bloon>();
            foreach(Bloon bloon in Spawns)
            {
                
                r.Add(bloon);
                r[r.Count-1].camo = camo;
                r[r.Count-1].regen = regen;
                r[r.Count-1].Damage(-Health);
            }
        }
        return r;
    }
}

public class RedBloon : Bloon
{
    public int MaxHealth = 1;
}
