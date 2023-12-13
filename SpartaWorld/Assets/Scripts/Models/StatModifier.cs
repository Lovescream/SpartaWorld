using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier {

    public StatType Stat { get; private set; }
    public StatModifierType Type { get; private set; }
    public float Value { get; private set; }

    
    public StatModifier(StatType stat, StatModifierType type, float value) {
        Stat = stat;
        Type = type;
        Value = value;
    }
}