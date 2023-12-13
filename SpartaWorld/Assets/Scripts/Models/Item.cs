using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public ItemData Data { get; private set; }
    public string Key => Data.key;
    public ItemType Type => Data.type;
    public string Name => Data.name;
    public string Description => Data.description;
    public float Cost => Data.cost;
    public List<StatModifier> Modifiers { get; private set; }

    public Item(ItemData data) {
        this.Data = data;
        Modifiers = Data.modifiers.ConvertAll(x => x);
    }
}