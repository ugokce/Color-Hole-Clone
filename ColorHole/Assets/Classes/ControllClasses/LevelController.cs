using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    Level currentLevel;
    int levelIndex = 0;
    int collectedObjectCount = 0;
    void Start()
    {
        levelIndex = PlayerPrefs.GetInt("CurrentLevel", 1);

        EventManager.getInstance().playerEvents.onObjectCollected.AddListener(OnObjectCollected);
    }

    void OnLevelCompleted()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel.levelNumber);

        currentLevel = LevelSerializer.DeSerializeLevel(currentLevel.levelNumber + 1);
        currentLevel.initLevel();
    }

    void OnObjectCollected()
    {
        collectedObjectCount++;

        if(collectedObjectCount >= currentLevel.levelObjects.Count)
        {
            EventManager.getInstance().playerEvents.onLevelCompleted.Invoke(currentLevel.levelNumber);
        }
        else if(collectedObjectCount >= currentLevel.subLevelPassCount)
        {
            OnLevelCompleted();
            EventManager.getInstance().playerEvents.onSubLevelCleared.Invoke();
        }
    }
}
