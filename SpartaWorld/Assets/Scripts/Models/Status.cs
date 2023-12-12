using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status {
    private Dictionary<StatType, Stat> _stats;

    public Stat this[StatType type] => _stats[type];

    public Status() {
        _stats = new();
        for (int i = 0; i < (int)StatType.COUNT; i++) {
            _stats.Add((StatType)i, new Stat((StatType)i));
        }
    }
}

public enum StatType {
    Hp,
    Damage,
    Defense,
    Critical,
    COUNT,
}
public enum StatModifierType {
    Add,
    Multiple,
}