using UnityEngine;
using System.IO;

public static class JsonManager {
    public static bool SaveToJson(string fileName, GameData data) {
        // No tira error el writeAll pero está bueno chequear que no sea read only de todas formas:
        if (File.Exists(fileName) && ((File.GetAttributes(fileName) & FileAttributes.ReadOnly)) != 0)
            return false;
        
        string textJson = JsonUtility.ToJson(data);
        File.WriteAllText(fileName, textJson);
        return true;
    }

    public static bool ReadFromJson(string fileName, out GameData dataBuff) {
        if (!File.Exists(fileName)) {
            dataBuff = null;
            return false;
        }

        string textJson = File.ReadAllText(fileName);
        dataBuff = JsonUtility.FromJson<GameData>(textJson);
        return true;
    }
}