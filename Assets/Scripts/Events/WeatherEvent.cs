using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherEvent : Events
{
    public enum Severity { Helpful, Minor, Inconvenient, Problematic, Kaju_Controlled};
    public enum Damage { Heal, electrical, acidic}
    public Severity severity;
    public Damage damage;

    void DetermineValues()
    { 
        int rand = Random.Range(0, 100);
        if (rand < 10)
        { severity = Severity.Helpful; damage = Damage.Heal; }
        else if (rand >= 10 && rand < 59)
        {
            severity = Severity.Minor;
            int rand2 = Random.Range(0, 1);
            if (rand2 == 1)
            { damage = Damage.electrical; }
            else
            { damage = Damage.acidic; }
        }
        else if (rand >= 60 && rand < 84)
        { 
            severity = Severity.Inconvenient;
            int rand2 = Random.Range(0, 1);
            if (rand2 == 1)
            { damage = Damage.electrical; }
            else
            { damage = Damage.acidic; }
        }
        else if(rand >=84 && rand < 96)
        {
            severity = Severity.Problematic;
            int rand2 = Random.Range(0, 1);
            if (rand2 == 1)
            { damage = Damage.electrical; }
            else
            { damage = Damage.acidic; }
        }
        else
        {
            severity = Severity.Kaju_Controlled;
            int rand2 = Random.Range(0, 1);
            if (rand2 == 1)
            { damage = Damage.electrical; }
            else
            { damage = Damage.acidic; }
        }
    }
}
