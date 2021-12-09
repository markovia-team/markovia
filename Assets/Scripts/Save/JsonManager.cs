using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Runtime.Serialization;
using System.IO;
using SFB;

public static class JsonManager {
    public static GameData initializationData = null;

    public static bool SaveToJson(string fileName, GameData data) {
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

    public static void WriteData() {
		int id = 0, count = 0;
        GameData currentData = new GameData();

        var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "json");
        if (string.Compare(path, string.Empty, StringComparison.Ordinal) == 0) {
            //popup.GetComponentInChildren<TMPro.TMP_Text>().text = "You must enter a name";
            //popup.SetActive(true);
            return;
        }
        //popup.GetComponentInChildren<TMPro.TMP_Text>().text = !JsonManager.SaveToJson(path, currentData) ? "File is read-only!" : "File saved successfully";
        if (!path.EndsWith(".json")) {
            File.Move(path, path + ".json");
        }
    //    popup.SetActive(true);
    }
}