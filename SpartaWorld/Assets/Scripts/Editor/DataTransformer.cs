using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DataTransformer : EditorWindow {
#if UNITY_EDITOR

    [MenuItem("Tools/ParseExcel")]
    public static void ParseExcel() {
        ParseCharacterData("Character");
        ParseLevelData("Level");
        ParseItemData("Item");
    }

    private static void ParseCharacterData(string fileName) {
        CharacterDataLoader loader = new();

        string[] lines = File.ReadAllText($"{Application.dataPath}/@Resources/Data/Excel/{fileName}Data.csv").Split("\n");

        for (int y= 1; y < lines.Length; y++) {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0 || string.IsNullOrEmpty(row[0])) continue;

            loader.characters.Add(new() {
                key = row[0],
                name = row[1],
                description = row[2],
                hp = ConvertValue<float>(row[3]),
                damage = ConvertValue<float>(row[4]),
                defense = ConvertValue<float>(row[5]),
                critical = ConvertValue<float>(row[6]),
            });
        }

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/@Resources/Data/JsonData/{fileName}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }
    private static void ParseLevelData(string fileName) {
        LevelDataLoader loader = new();

        string[] lines = File.ReadAllText($"{Application.dataPath}/@Resources/Data/Excel/{fileName}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++) {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0 || string.IsNullOrEmpty(row[0])) continue;

            loader.levels.Add(new() {
                level = ConvertValue<int>(row[0]),
                totalExp = ConvertValue<int>(row[1]),
            });
        }

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/@Resources/Data/JsonData/{fileName}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }
    private static void ParseItemData(string fileName) {
        ItemDataLoader loader = new();

        string[] lines = File.ReadAllText($"{Application.dataPath}/@Resources/Data/Excel/{fileName}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++) {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0 || string.IsNullOrEmpty(row[0])) continue;

            List<StatModifier> modifiers = new();
            string[] modifierInfos = row[5].Split('|');
            for (int i=0;i<modifierInfos.Length;i++) {
                string[] modifierInfo = modifierInfos[i].Split('_');
                modifiers.Add(new StatModifier(ConvertValue<StatType>(modifierInfo[0]),
                    ConvertValue<StatModifierType>(modifierInfo[1]),
                    ConvertValue<float>(modifierInfo[2])));
            }

            loader.items.Add(new() {
                key = row[0],
                type = ConvertValue<ItemType>(row[1]),
                name = row[2],
                description = row[3],
                cost = ConvertValue<float>(row[4]),
                modifiers = modifiers,
            });
        }

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/@Resources/Data/JsonData/{fileName}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }




    private static T ConvertValue<T>(string value) {
        if (string.IsNullOrEmpty(value)) return default;
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
        return (T)converter.ConvertFromString(value);
    }

    private static List<T> ConvertList<T>(string value) {
        if (string.IsNullOrEmpty(value)) return new();
        return value.Split('|').Select(x => ConvertValue<T>(x)).ToList();
    }

#endif
}
