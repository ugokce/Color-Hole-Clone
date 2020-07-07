using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    Level currentLevel;
    int levelIndex = 0;
    
    void Start()
    {
        levelIndex = PlayerPrefs.GetInt("CurrentLevel", 1);
    }

    void OnLevelCompleted()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel.levelNumber);

        currentLevel = LevelSerializer.DeSerializeLevel(currentLevel.levelNumber + 1);
        currentLevel.initLevel();
    }
}
