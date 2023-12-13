using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : Inventory {

    private Dictionary<ItemType, Item> _equips = new();

    public event Action<Item> OnEquipChanged;

    public PlayerInventory() {
        _equips.Add(ItemType.WEAPON, null);
        _equips.Add(ItemType.Shield, null);
        _equips.Add(ItemType.Helmet, null);
        _equips.Add(ItemType.Armor, null);
        _equips.Add(ItemType.Boots, null);
    }

    public bool IsEquip(Item item) {
        return _equips.Values.Contains(item);
    }
    public void Equip(Item item) {
        switch (item.Type) {
            case ItemType.Sword:
            case ItemType.Axe:
            case ItemType.Hammer:
                if (_equips[ItemType.WEAPON] != null) UnEquip(ItemType.WEAPON);
                _equips[ItemType.WEAPON] = item;
                break;
            case ItemType.Shield:
                if (_equips[ItemType.Shield] != null) UnEquip(ItemType.Shield);
                _equips[ItemType.Shield] = item;
                break;
            case ItemType.Helmet:
                if (_equips[ItemType.Helmet] != null) UnEquip(ItemType.Helmet);
                _equips[ItemType.Helmet] = item;
                break;
            case ItemType.Armor:
                if (_equips[ItemType.Armor] != null) UnEquip(ItemType.Armor);
                _equips[ItemType.Armor] = item;
                break;
            case ItemType.Boots:
                if (_equips[ItemType.Boots] != null) UnEquip(ItemType.Boots);
                _equips[ItemType.Boots] = item;
                break;
        }

        // TODO:: Find제거
        GameObject.FindObjectOfType<MainScene>().Player.Status.AddModifiers(item.Modifiers);

        OnEquipChanged(item);
    }
    public void UnEquip(ItemType type) {
        if (type == ItemType.Sword || type == ItemType.Axe || type == ItemType.Hammer) type = ItemType.WEAPON;
        if (!_equips.TryGetValue(type, out Item item)) return;
        if (item == null) return;
        _equips[type] = null;

        // TODO:: Find제거
        GameObject.FindObjectOfType<MainScene>().Player.Status.RemoveModifiers(item.Modifiers);

        OnEquipChanged(item);
    }
    
}