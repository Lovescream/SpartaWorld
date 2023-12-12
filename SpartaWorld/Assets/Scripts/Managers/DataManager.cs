using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class DataManager {

    public Dictionary<string, CharacterData> Characters = new();
    public Dictionary<int, LevelData> Levels = new();

    public void Initialize() {
        Characters = LoadJson<CharacterDataLoader, string, CharacterData>("CharacterData").MakeDictionary();
        Levels = LoadJson<LevelDataLoader, int, LevelData>("LevelData").MakeDictionary();
    }


    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value> {
        TextAsset textAsset = Main.Resource.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<Loader>(textAsset.text);
    }

}

public interface ILoader<Key, Value> {
    Dictionary<Key, Value> MakeDictionary();
}