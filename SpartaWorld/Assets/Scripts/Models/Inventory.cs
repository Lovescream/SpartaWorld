using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    #region Properties

    public float Gold {
        get => _gold;
        set {
            _gold = value;
            OnGoldChanged?.Invoke(_gold);
        }
    }
    public int Count => _items.Count;
    public int MaxCount => 99;      // TODO:: NO HARDCODING

    #endregion

    #region Fields

    private float _gold;

    // Collections.
    private List<Item> _items = new();

    // Callbacks.
    public event Action<float> OnGoldChanged;
    public event Action OnChanged;

    #endregion

    public Item this[string key] {
        get {
            for (int i = 0; i < _items.Count; i++) {
                if (_items[i].Key == key) return _items[i];
            }
            return null;
        }
    }
    public Item this[int index] {
        get {
            return _items[index];
        }
    }

    public void Add(Item item) {
        _items.Add(item);
        OnChanged?.Invoke();
    }
    public void Remove(Item item) {
        _items.Remove(item);
        OnChanged?.Invoke();
    }
    

}