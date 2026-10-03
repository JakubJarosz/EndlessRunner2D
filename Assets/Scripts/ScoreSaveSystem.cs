using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class ScoreSaveSystem {
    private static string savePath => Application.persistentDataPath + "/scores.json";

    [System.Serializable]
    private class ScoreWrapper {
        public List<int> scores;
    }

    public static void Save(List<int> scores) {
        ScoreWrapper wrapper = new ScoreWrapper();
        wrapper.scores = scores;

        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, json);
    }

    public static List<int> Load () {
        if (!File.Exists(savePath)) {
            return new List<int>();
        }

        string json = File.ReadAllText(savePath);
        ScoreWrapper wrapper = JsonUtility.FromJson<ScoreWrapper>(json);

        return wrapper.scores ?? new List<int>();
    }
}
