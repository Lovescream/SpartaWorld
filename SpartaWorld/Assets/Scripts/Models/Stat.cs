using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat {
    public StatType Type { get; private set; }
    public float Min { get; private set; }
    public float Max { get; private set; }
    public float Value { get; private set; }
    public float OriginValue { get; private set; }

    private List<StatModifier> _modifiers = new();

    public event Action<Stat> OnChanged;

    public Stat(StatType type, float min = 0, float max = 1049, float value = 0) {
        Type = type;
        Min = min;
        Max = max;
        OriginValue = value;
    }

    public void SetValue(float value) {
        OriginValue = value;
        Value = GetModifyValue();
        OnChanged?.Invoke(this);
    }
    public void AddModifier(StatModifier modifier) {
        _modifiers.Add(modifier);
        Value = GetModifyValue();
        OnChanged?.Invoke(this);
    }
    public void RemoveModifier(StatModifier modifier) {
        _modifiers.Remove(modifier);
        Value = GetModifyValue();
        OnChanged?.Invoke(this);
    }
    private float GetModifyValue() {
        float value = OriginValue;
        for (int i = 0; i < _modifiers.Count; i++) {
            if (_modifiers[i].Type == StatModifierType.Add) value += _modifiers[i].Value;
            else if (_modifiers[i].Type == StatModifierType.Multiple) value *= _modifiers[i].Value;
        }
        value = Mathf.Clamp(value, Min, Max);
        return value;
    }
}