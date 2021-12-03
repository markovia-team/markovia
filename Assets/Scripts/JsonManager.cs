using UnityEngine;
using System.IO;

public static class JsonManager {
    public static bool SaveToJson(string fileName, GameData data) {
        // No tira error el writeAll pero está bueno chequear que no sea read only de todas formas:
        if (File.Exists(Application.dataPath + "/Scripts/" + fileName) && ((File.GetAttributes(Application.dataPath + "/Scripts/" + fileName) & FileAttributes.ReadOnly)) != 0)
            return false;
        
        string textJson = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/Scripts/" + fileName, textJson);
        return true;
    }

    public static bool ReadFromJson(string fileName, out GameData dataBuff) {
        if (!File.Exists(Application.dataPath + "/Scripts/" + fileName)) {
            dataBuff = null;
            return false;
        }

        string textJson = File.ReadAllText(Application.dataPath + "/Scripts/" + fileName);
        dataBuff = JsonUtility.FromJson<GameData>(textJson);
        return true;
    }
}