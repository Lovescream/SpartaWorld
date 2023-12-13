using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public string UserName { get; private set; }
    public CharacterData Data { get; private set; }
    public string ClassName => Data.name;
    public string ClassDescription => Data.description;
    public int Level { get; private set; }
    public float Exp {
        get => _exp;
        set {
            _exp = value;
            int level = Level;
            while (true) {
                if (!Main.Data.Levels.TryGetValue(level + 1, out _)) break;
                if (!Main.Data.Levels.TryGetValue(level, out LevelData currentLevelData)) break;
                if (Exp < currentLevelData.totalExp) break;
                level++;
            }
            if (level != Level) {
                Level = level;
                Main.Data.Levels.TryGetValue(level, out LevelData currentLevelData);
                RequiredTotalExp = currentLevelData.totalExp;
            }

            int prevLevelTotalExp = Main.Data.Levels.TryGetValue(Level - 1, out LevelData prevLevelData) ? prevLevelData.totalExp : 0;
            RequiredExp = RequiredTotalExp - prevLevelTotalExp;
            CurrentExp = _exp - prevLevelTotalExp;

            OnPlayerDataUpdated?.Invoke();
        }
    }
    public float RequiredTotalExp { get; private set; }
    public float RequiredExp { get; private set; }
    public float CurrentExp { get; private set; }
    public float ExpRatio => CurrentExp / RequiredExp;

    public Status Status { get; private set; } = new();
    public Inventory Inventory { get; private set; }

    private float _exp;

    public event Action OnPlayerDataUpdated;

    public Player(string name, CharacterData data) {
        this.UserName = name;
        this.Data = data;
        Level = 1;
        Exp = 10;
        Status = new();
        Status[StatType.Hp].SetValue(data.hp);
        Status[StatType.Damage].SetValue(data.damage);
        Status[StatType.Defense].SetValue(data.defense);
        Status[StatType.Critical].SetValue(data.critical);
        Inventory = new();
    }
}