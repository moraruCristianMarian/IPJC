using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgressData
{
    public string lastCompletedLevel;
    public List<LevelData> bestTimesPerLevel = new List<LevelData>();
}

[System.Serializable]
public class LevelData {
    public string name;
    public string bestTime;
}