using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier {

    public StatModifierType Type { get; private set; }
    public float Value { get; private set; }

    
    public StatModifier(StatModifierType type, float value) {
        Type = type;
        Value = value;
    }
}