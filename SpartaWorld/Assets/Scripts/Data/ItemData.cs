using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData {
    public string key;
    public ItemType type;
    public string name;
    public string description;
    public float cost;
    public List<StatModifier> modifiers;
}

[Serializable]
public class ItemDataLoader:ILoader<string, ItemData> {
    public List<ItemData> items = new();
    public Dictionary<string, ItemData> MakeDictionary() {
        Dictionary<string, ItemData> dictionary = new();
        foreach (ItemData data in items) {
            dictionary.Add(data.key, data);
        }
        return dictionary;
    }
}

public enum ItemType {
    Sword,
    Axe,
    Hammer,
    Shield,
    WEAPON,
    Helmet,
    Armor,
    Boots,
    ARMOR,
}