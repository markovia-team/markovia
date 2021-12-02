using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using System.Runtime.Serialization;
using System.IO;
using System.Text;

public static class JsonManager{
    public static bool SaveToJson(string fileName, GameData data) {
        //validar que exista
        string textJson = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/Scripts/" + fileName, textJson);
        return true;
    }

    public static bool ReadFromJson(string fileName, out GameData dataBuff){
        //validar que exista
        string textJson = File.ReadAllText(Application.dataPath + "/Scripts/" + fileName);
        dataBuff= JsonUtility.FromJson<GameData>(textJson);
        return true;
    }
}