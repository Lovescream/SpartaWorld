using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string UserName { get; private set; }

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
                RequiredExp = currentLevelData.totalExp;
            }
            OnPlayerDataUpdated?.Invoke();
        }
    }
    public float RequiredExp { get; private set; }

    public Status Status { get; private set; } = new();

    private float _exp;

    public event Action OnPlayerDataUpdated;
}