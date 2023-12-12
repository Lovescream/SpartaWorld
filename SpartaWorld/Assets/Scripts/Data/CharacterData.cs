using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[Serializable]
public class CharacterData {
    public string key;
    public string description;
    public float hp;
    public float damage;
    public float defense;
    public float critical;
}

[Serializable]
public class CharacterDataLoader : ILoader<string, CharacterData> {
    public List<CharacterData> characters = new();
    public Dictionary<string, CharacterData> MakeDictionary() {
        Dictionary<string, CharacterData> dictionary = new();
        foreach (CharacterData character in characters) {
            dictionary.Add(character.key, character);
        }
        return dictionary;
    }
}
