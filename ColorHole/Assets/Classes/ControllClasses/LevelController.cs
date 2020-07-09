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

    public void GenerateLevelObjects()
    {
        foreach (var obj in currentLevel.levelObjects)
        {
            GameObject newObject = new GameObject();
            Instantiate(newObject);
            newObject.AddComponent<LevelObject>();
            newObject.GetComponent<LevelObject>().objectData.CopyValues(obj);
        }
    }

    public void ClearLevel()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("LevelObject"))
        {
            Destroy(obj);
        }
    }

    public void initLevel()
    {
        ClearLevel();
        GenerateLevelObjects();
    }

    void OnLevelCompleted()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel.levelNumber);

        currentLevel = LevelSerializer.DeSerializeLevel(currentLevel.levelNumber + 1);
        initLevel();
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

    public float GetSubLevelProgress()
    {
        if(collectedObjectCount < currentLevel.subLevelPassCount)
        {
            return collectedObjectCount / currentLevel.subLevelPassCount;
        }

        return 1;
    }

    public float GetLevelProgress()
    {
        if(collectedObjectCount < currentLevel.subLevelPassCount)
        {
            return 0;
        }

        return (collectedObjectCount - currentLevel.subLevelPassCount) / (currentLevel.levelObjects.Count - currentLevel.subLevelPassCount);
    }
}
