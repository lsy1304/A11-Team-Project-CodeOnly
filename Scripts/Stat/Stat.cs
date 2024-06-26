using System;


[Serializable]
public class Stat
{
    public int MaxHP;
    public int Def;
    public int BaseDamage;

    public Stat DeepCopy()
    {
        Stat newStat = (Stat)Activator.CreateInstance(GetType());

        newStat.MaxHP = this.MaxHP;
        newStat.BaseDamage = this.BaseDamage;
        newStat.Def = this.Def;

        return newStat;
    }
}
